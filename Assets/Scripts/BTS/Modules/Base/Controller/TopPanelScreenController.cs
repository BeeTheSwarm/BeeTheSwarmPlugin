using UnityEngine;
using System.Collections;
using System;

public abstract class TopPanelScreenController<View> : BaseScreenController<View> where View: ITopPanelContainer {
    [Inject]
    protected ITopPanelController m_topPanelControllerDelegate;
    [Inject]
    protected IUserProfileController m_userProfileScreen;

    public override void PostInject() {
        base.PostInject();
        m_topPanelControllerDelegate.SetAvatarEnabled(AvatarEnabled);
        if (AvatarEnabled) {
            m_topPanelControllerDelegate.OnAvatarPressed += AvatarPressedHandler;
        }
        m_topPanelControllerDelegate.SetBackButtonEnabled(BackButtonEnabled);
        if (BackButtonEnabled) {
            m_topPanelControllerDelegate.OnBackBtnPressed += BackButtonPressedHandler;
        }
    }

    protected virtual void AvatarPressedHandler() {
        m_userProfileScreen.Show();
    }

    protected virtual void BackButtonPressedHandler() {
        OnBackPressed();
    }

    protected override void PostSetView() {
        base.PostSetView();
        m_view.SetInitCallback(() => {
                m_topPanelControllerDelegate.SetView(m_view.GetTopPanel());
            });
    }

    protected virtual bool BackButtonEnabled {
        get {
            return false;
        }
    }

    protected virtual bool AvatarEnabled {
        get {
            return false;
        }
    }
}
