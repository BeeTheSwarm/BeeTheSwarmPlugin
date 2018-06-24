using UnityEngine;
using System.Collections;
namespace BTS
{
    public interface IUserProfileScreenListener : IViewEventListener
    {
        void OnEditProfileClick();
        void OnNotificationsClick();
        void OnAboutClick();
        void OnHiveClick();
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