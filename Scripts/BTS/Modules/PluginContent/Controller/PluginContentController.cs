using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluginContentController : BaseScreenController<IPluginContentView>, IPluginContentViewListener, IPluginContentController {
    [Inject]
    private IUserProfileModel m_userProfile;
    [Inject]
    private IFeedsController m_feedsController;
    [Inject]
    private ISignUpController m_signUpController;
    [Inject]
    private IRegistrationController m_registrationController;

    public PluginContentController()
    {
        
    }

    public override void PostInject() {
        base.PostInject();
        m_userProfile.OnUserLoggedOut += LogoutHandler;
    }

    private void LogoutHandler() {
        m_signUpController.Show();
    }

    public override bool StoredInHistory {
        get {
            return false;
        }
    }
    public override void Show() {
        base.Show();
        if (m_userProfile.IsLoggedIn) {
            m_view.Show();
             
        }
        else {
            m_signUpController.Show();
        }
    }
    
    public void OnDrag(float y) {
        if (y > 50) {
            Hide();
        }
    }
}
