using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInController : BaseScreenController<ISignInView>, ISignInViewListener, ISignInController {
    [Inject] private IUserProfileModel m_userModel;
    [Inject] private IPopupsModel m_popupModel;
    [Inject] private IRegistrationController m_registrationController;
    [Inject] private IFeedsController m_feedsController;
    [Inject] private ILoginService m_loginService;
    [Inject] private IForgotPasswordController m_forgotPasswordController;

    [Inject] private ILoadInitDataService m_loadInitDataService;
    [Inject] private ILoaderController m_loaderController;

    public SignInController() {
    }

    public override void PostInject() {
        base.PostInject();
        m_userModel.OnUserStateChanged += UserStateChangedHandler;
    }

    private void UserStateChangedHandler(UserState state) {
        switch (state) {
            case UserState.LoggedIn:
                Hide();
                break;
        }
    }

    public override bool StoredInHistory
    {
        get { return false; }
    }

    public override void Hide() {
        base.Hide();
        m_view.ClearInput();
    }

    public void OnForgotPasswordClick() {
        m_forgotPasswordController.Show();
    }

    public void OnRegisterClick() {
        m_registrationController.ShowRegistrationPage(string.Empty);
    }

    public void OnSignInClick(string login, string password) {
        if (string.IsNullOrEmpty(login)) {
            m_popupModel.AddPopup(new ErrorPopupItemModel("Login could not be empty"));
            return;
        }

        if (string.IsNullOrEmpty(password)) {
            m_popupModel.AddPopup(new ErrorPopupItemModel("Password could not be empty"));
            return;
        }

        m_loaderController.Show("Loading");
        m_loginService.Execute(login, password, result => {
            m_loaderController.Hide();
            if (result) {
                m_loaderController.Hide();
                m_feedsController.Show();
            }
            else {
                if (m_userModel.State == UserState.Unconfirmed) {
                    m_registrationController.ShowConfirmationPage(login);
                }
            }
        });
    }
}