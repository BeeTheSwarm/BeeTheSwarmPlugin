using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Avatar = BTS.Avatar;

public class EditProfileView : TopPanelScreen<IEditProfileViewListener>, IEditProfileView {
    [SerializeField]
    private Avatar m_avatar;
    [SerializeField]
    private InputField m_username;
    [SerializeField]
    private GameObject m_changePasswordBtn;
    [SerializeField]
    private InputField m_oldPassword;
    [SerializeField]
    private InputField m_newPassword;
    [SerializeField]
    private InputField m_confirmNewPassword;
    [SerializeField]
    private GameObject m_passwordFieldsContainer;
    [SerializeField]
    private GameObject m_popupContainer;
    [SerializeField]
    private Text m_popupText;

    private EditProfileViewModel m_viewModel;
    public void SetViewModel(EditProfileViewModel model)
    {
        m_viewModel = model;
        model.Avatar.Subscribe(SetAvatar);
        model.Username.Subscribe(SetUsername);
    }

    public void SetAvatar(Sprite avatar)
    {
        m_avatar.SetAvatar(avatar);
    }
    
    public void SetUsername(string username)
    {
        m_username.text = username;
    }
    
    public void OnChangePasswordClick()
    {
        m_changePasswordBtn.gameObject.SetActive(false);
        m_passwordFieldsContainer.SetActive(true);
    }

    public void OnUsernameEdited(string text) {
        m_viewModel.NewUsername = m_username.text;
    }

    public void OnOldPasswordEdited(string text) {
        m_viewModel.OldPassword = m_oldPassword.text;
    }

    public void OnNewPasswordEdited(string text) {
        m_viewModel.NewPassword = m_newPassword.text;
    }

    public void OnConfirmPasswordEdited(string text) {
        m_viewModel.NewPasswordConfirmation = m_confirmNewPassword.text;
    }

    public void OnSaveChangesClick() {
        m_controller.OnSaveChanges();
    }

    public void OnEditAvatarClick() {
        m_controller.OnEditAvatar();
    }

    public void Reset() {
        m_changePasswordBtn.gameObject.SetActive(true);
        m_passwordFieldsContainer.SetActive(false);
        m_oldPassword.text = string.Empty;
        m_newPassword.text = string.Empty;
        m_confirmNewPassword.text = string.Empty;
}

    public void ShowPopup(string message) {
        if (string.IsNullOrEmpty(message)) {
            m_popupText.text = "User updated";
        } else {
            m_popupText.text = "Failed: \n" + message;
        }
        m_popupContainer.SetActive(true);
    }

    public void OnPopupButtonClick() {
        m_controller.OnPopupButtonClicked();
    }

    public void ClosePopup() {
        m_popupContainer.SetActive(false);
    }
}
