using UnityEngine;
using System.Collections;
namespace BTS
{
    public interface IUserProfileScreenListener : IViewEventListener
    {
        void OnEditProfileClick();
        void OnNotificationsClick();
        void OnLeaderboardClick();
        void OnAboutClick();
        void OnQuestsClick();
        void OnHiveClick();
        void OnBadgesClick();
        void OnOurGamesClick();
        void OnInviteFriendsClick();
        void OnDeleteCampaignClick();
        void OnViewAllPostsClick();
        void OnCreateCampaignClick();
        void OnCampaignToolboxClick();
        void OnSignOutClick();
        void OnRequestsClick();
        void OnEditClick(Vector3 position);
        void OnAddPostClick();
    }
}