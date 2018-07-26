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

    public event Action OnHideStarted = delegate { };
    public event Action OnHideFinished = delegate { };
    public event Action OnShown = delegate { };

    public PluginContentController(bool standalone) {
        IsStandalone = standalone;
    }

    public override void PostInject() {
        base.PostInject();
        m_userProfile.OnUserLoggedOut += LogoutHandler;
    }

    private void LogoutHandler() {
        m_signUpController.Show();
    }

    protected override void PostSetView() {
        base.PostSetView();
        m_view.Setup(!IsStandalone);
        m_view.OnHideStarted += () => { OnHideStarted.Invoke(); };
        m_view.OnHideFinished += () => { OnHideFinished.Invoke(); };
        m_view.OnShown += () => { OnShown.Invoke(); };
    }

    public override bool StoredInHistory {
        get {
            return false;
        }
    }

    private bool IsStandalone { get; set; }

    public override void Show() {
        base.Show();
        if (!m_userProfile.IsLoggedIn) {
            m_signUpController.Show();
        }
    }
    
    public void OnDrag(float y) {
        if (y > 50) {
            Hide();
        }
    }
}
