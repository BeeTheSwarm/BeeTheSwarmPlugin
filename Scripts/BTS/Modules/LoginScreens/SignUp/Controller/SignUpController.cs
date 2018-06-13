using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUpController : BaseScreenController<ISignUpView>, ISignUpViewListener, ISignUpController {
    [Inject]
    private IUserProfileModel m_userModel;
    [Inject]
    private ISignInController m_signInController;
    [Inject]
    private IPluginContentController m_pluginController;

    [Inject]
    private IRegistrationController m_registrationController;

    private bool m_isStandalone;

    public SignUpController(bool IsStandalone) {
        m_isStandalone = IsStandalone;
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
    
    public override void Hide() {
        base.Hide();
        m_view.ClearInput();
    }

    public override bool StoredInHistory {
        get {
            return false;
        }
    }

    public override void Show() {
        base.Show();
        m_view.Setup(m_isStandalone);
    }

    public void OnSignInClick() {
        m_signInController.Show();
        Hide();
    }

    public void OnSignUpClick(string email) {
        m_registrationController.ShowRegistrationPage(email);
    }

    public void BackClicked() {
        m_pluginController.Hide();
    }
}
