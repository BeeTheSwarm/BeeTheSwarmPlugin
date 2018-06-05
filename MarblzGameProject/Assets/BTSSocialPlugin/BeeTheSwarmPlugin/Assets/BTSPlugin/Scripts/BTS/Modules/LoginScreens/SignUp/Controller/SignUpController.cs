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
    private IRegistrationController m_registrationController;
    public SignUpController()
    {
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
    
    public void OnSignInClick() {
        m_signInController.Show();
        Hide();
    }

    public void OnSignUpClick(string email) {
        m_registrationController.ShowRegistrationPage(email);
    }
}
