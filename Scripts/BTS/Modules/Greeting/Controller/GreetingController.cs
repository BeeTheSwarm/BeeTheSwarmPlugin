using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetingController: BaseScreenController<IGreetingView>, IGreetingViewListener, IGreetingController  
{

    [Inject]
    private IUserProfileModel m_userModel;
    [Inject]
    private IPopupsModel m_popupsModel;
    private bool m_waitingAnimation;
    private PopupItemModel m_currentItem;
    public GreetingController()
    {
        
    }

    public override void PostInject() {
        base.PostInject();
        m_popupsModel.PopupAdded += PopupAddedHandler;
        m_userModel.OnUserLoggedIn += AddBeeListener;
        m_userModel.OnUserLoggedOut += RemoveBeeListener;
    }

    private void AddBeeListener() {
        m_userModel.OnBeesCountUpdated += AddBeesPopup;
    }

    private void AddBeesPopup(int obj, int diff) {
        if (diff > 0) {
            m_popupsModel.AddPopup(new UserInfoPopupItemModel(m_userModel.User.Bees, m_userModel.User.Level, m_userModel.User.Progress));
        }
    }

    private void RemoveBeeListener() {
        m_userModel.OnBeesCountUpdated -= AddBeesPopup;
        m_view.ClearUserStat();
    }

    private void PopupAddedHandler() {
        if (m_waitingAnimation) {
            return;
        }
        ShowNext();
    }

    private void ShowNext() {
        m_currentItem = m_popupsModel.GetNextPopup();
        if (m_currentItem != null) {
            m_waitingAnimation = true;
            m_view.ShowPopup(m_currentItem);
        } 
    }

    public void OnPopupShown() {
        m_popupsModel.PopupShown(m_currentItem);
        m_waitingAnimation = false;
        ShowNext();
    }
    
}
