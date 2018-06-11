using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignToolboxView : TopPanelScreen<ICampaignToolboxViewListener>, ICampaignToolboxView
{
    public void OnAnnounceFacebookClicked()
    {
        m_controller.OnAnnonceFacebookClick();
    }

    public void OnAnnounceEmailClicked()
    {
        m_controller.OnAnnounceEmailClick();
    }

    public void OnAnnouncePhoneClicked()
    {
        m_controller.OnAnnouncePhoneClick();
    }

    public void OnExplainClicked()
    {
        m_controller.OnExplainClick();
    }

    public void OnInviteClicked()
    {
        m_controller.OnInviteClick();
    }

    public void OnMyCampaignClicked()
    {
        m_controller.OnMyCampaignClick();
    }
}
