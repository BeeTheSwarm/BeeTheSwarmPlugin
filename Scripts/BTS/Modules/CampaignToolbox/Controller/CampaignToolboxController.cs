using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignToolboxController: TopPanelScreenController<ICampaignToolboxView>, ICampaignToolboxViewListener, ICampaignToolboxController 
{
    TopPanelViewModel m_viewmodel;
    [Inject] private IFeedsModel m_feedsModel;
    [Inject] private IViewCampaignController m_viewCampaignController;
    [Inject] private IInviteFriendsController m_inviteFriendsController;
    [Inject] private IPopupsModel m_popupsModel;
    [Inject] private IContactPickerController m_contactPickerController;
    [Inject] private IStartCampaignController m_startCampaignController;
    [Inject] private ITutorialController m_minigameController;


    private const string MESSAGE_TITLE = "Help me Fundraise the Fun Way!";
    private const string SHARE_TEXT = "Help me raise money for charities by playing free video games on your phone! Just use this link beetheswarm.app.link";
    public CampaignToolboxController()
    {
        
    }
    
    protected override bool BackButtonEnabled {
        get {
            return true;
        }
    }

    public void OnAnnonceFacebookClick()
    {
        UM_ShareUtility.FacebookShare(SHARE_TEXT);
    }

    public void OnAnnounceEmailClick()
    {
        m_contactPickerController.ShowEmailPicker(list => {
            if (list.Count > 0) {
                Platform.Adapter.SendEmail(MESSAGE_TITLE, SHARE_TEXT, list, result => {
                    
                });
            }
        }, "Select", "Send");
    }

    public void OnAnnouncePhoneClick()
    {
        m_contactPickerController.ShowPhonesPicker(list => {
            if (list.Count > 0) {
                Platform.Adapter.SendMessages(SHARE_TEXT, list, result => {
                    
                });
            }
        }, "Select", "Send");
    }

    public void OnExplainClick()
    {
        #if UNITY_EDITOR
        m_minigameController.Show();
        #endif
        m_popupsModel.AddPopup(new ErrorPopupItemModel("Not implemented yet"));
    }

    public void OnInviteClick()
    {
        m_inviteFriendsController.Show();
    }

    public void OnMyCampaignClick()
    {
        if (m_feedsModel.UserCampaign.Count() > 0) {
            m_viewCampaignController.Show(m_feedsModel.UserCampaign.GetPosts()[0].Campaign.Id);
        }
        else {
            m_startCampaignController.Show(); 
        }
    }
}
