using UnityEngine;
using System.Collections;
namespace BTS
{
    public interface IFeedsViewListener: IViewEventListener
    {
        void OnShowTopCampaigns();
        void OnShowAllCampaigns();
        void OnShowRecentCampaigns();
        void OnShowFavouriteCampaigns(); 
        void OnInviteClick();
    }
}