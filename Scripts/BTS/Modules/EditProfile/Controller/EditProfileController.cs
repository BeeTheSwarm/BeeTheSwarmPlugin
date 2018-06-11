using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditProfileController : TopPanelScreenController<IEditProfileView>, IEditProfileViewListener, IEditProfileController {
    [Inject]
    private IUserProfileService m_userProfileService;
    [Inject]
    private IUserProfileModel m_userModel;
    [Inject]
    private IImagesService m_imageService;
    [Inject]
    private IPopupsModel m_popupsModel;
    [Inject]
    private IUpdateUserService m_updateUserService;
    [Inject] private ILoaderController m_loader;

    private EditProfileViewModel m_viewModel = new EditProfileViewModel();
    public EditProfileController() {
    }

    protected override bool BackButtonEnabled {
        get {
            return true;
        }
    }

    public override void Show() {
        base.Show();
        m_viewModel.Reset();
        m_imageService.GetImage(m_userModel.User.Avatar, m_viewModel.Avatar.Set);
        m_viewModel.Username.Set(m_userModel.User.Name);
        m_viewModel.NewUsername = m_userModel.User.Name;
        m_view.Reset();
    }

    protected override void PostSetView() {
        base.PostSetView();
        m_view.SetViewModel(m_viewModel);
    }

    private bool m_hasError = false;
    public void OnPopupButtonClicked() {
        if (!m_hasError) {
            base.OnBackPressed();
        }
        m_view.ClosePopup();
    }

    public void OnSaveChanges() {
        if (!string.IsNullOrEmpty(m_viewModel.OldPassword)) {
            if (m_viewModel.NewPassword.Length < 8) {
                m_popupsModel.AddPopup(new ErrorPopupItemModel("Minimal password length is 8 symbols"));
                return;
            }
            if (!m_viewModel.NewPassword.Equals(m_viewModel.NewPasswordConfirmation)) {
                m_popupsModel.AddPopup(new ErrorPopupItemModel("Passwords dont match"));
                return;
            }
        }
        m_loader.Show("Saving...");
        m_updateUserService.Execute(m_viewModel.NewUsername, m_viewModel.NewAvatar, m_viewModel.OldPassword, m_viewModel.NewPassword, m_viewModel.NewPasswordConfirmation,
            (error) => {
                m_loader.Hide();
                m_hasError = !string.IsNullOrEmpty(error);
                m_view.ShowPopup(error);
            }
        );
    }

    public void OnEditAvatar() {
        Platform.Adapter.GetGalleryImage((texture) => {
            m_viewModel.Avatar.Set(texture.ToSprite());
            m_viewModel.NewAvatar = texture;
        }
        );
    }
}
