using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICampaignToolboxViewListener : IViewEventListener
{
    void OnAnnonceFacebookClick();
    void OnAnnounceEmailClick();
    void OnAnnouncePhoneClick();
    void OnExplainClick();
    void OnInviteClick();
    void OnMyCampaignClick();
}
