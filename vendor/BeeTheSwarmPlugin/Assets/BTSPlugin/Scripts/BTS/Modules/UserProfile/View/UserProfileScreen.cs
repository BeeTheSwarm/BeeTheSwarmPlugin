using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Globalization;

namespace BTS {
    public class UserProfileScreen : TopPanelScreen<IUserProfileScreenListener>, IUserProfileScreen {

        [SerializeField]
        private Avatar m_avatar;
        [SerializeField]
        private Text m_impact;
        [SerializeField]
        private Text m_rank;
        [SerializeField]
        private Text m_notificationsCount;
        [SerializeField]
        private GameObject m_notificationsCounter;
        [SerializeField]
        private Text m_requestsCount;
        [SerializeField]
        private GameObject m_requestsCounter;
        [SerializeField]
        private Button m_editButton;
        [SerializeField]
        private UserCampaignContainer m_userCampaignContainer;
        private UserProfileViewModel m_viewModel;

        public void OnEditProfileClick() {
            m_controller.OnEditProfileClick();
        }

        public void OnNotificationsClick() {
            m_controller.OnNotificationsClick();
        }

        public void OnSignOutClick() {
            m_controller.OnSignOutClick();
        }

        public void OnRequestsClick() {
            m_controller.OnRequestsClick();
        }

        public void OnLeaderboardClick() {
            m_controller.OnLeaderboardClick();
        }

        public void OnAboutClick() {
            m_controller.OnAboutClick();
        }

        public void OnQuestsClick() {
            m_controller.OnQuestsClick();
        }
        public void OnHiveClick() {
            m_controller.OnHiveClick();
        }
        public void OnBadgesClick() {
            m_controller.OnBadgesClick();
        }
        public void OnOurGamesClick() {
            m_controller.OnOurGamesClick();
        }
        public void OnInviteFriendsClick() {
            m_controller.OnInviteFriendsClick();
        }

        public void OnCampaignToolboxClick() {
            m_controller.OnCampaignToolboxClick();
        }

        public void OnViewAllPostsClick() {
            m_controller.OnViewAllPostsClick();
        }

        public void OnCreateCampaignClick() {
            m_controller.OnCreateCampaignClick();
        }

        public void OnDeleteCampaignClick() {
            m_controller.OnDeleteCampaignClick();
        }

        public void SetViewModel(UserProfileViewModel viewModel) {
            Unsubscribe();
            m_viewModel = viewModel;
        }

        public override void Show() {
            base.Show();
            Subscribe();
        }

        public override void Hide() {
            base.Hide();
            Unsubscribe();
        }

        private void Subscribe() {
            if (m_viewModel != null) {
                m_viewModel.Avatar.Subscribe(SetAvatar);
                m_viewModel.Rank.Subscribe(SetRank);
                m_viewModel.Impact.Subscribe(SetImpact);
                m_viewModel.NotificationsCount.Subscribe(SetNotificationsCount);
                m_viewModel.RequestsCount.Subscribe(SetRequestsCount);
            }
        }

        private void SetRequestsCount(int value) {
            m_requestsCount.text = value.ToString();
            m_requestsCounter.SetActive(value > 0);
        }

        private void SetNotificationsCount(int value) {
            m_notificationsCount.text = value.ToString();
            m_notificationsCounter.SetActive(value > 0);
        }

        private void Unsubscribe() {
            if (m_viewModel != null) {
                m_viewModel.Avatar.Unsubscribe(SetAvatar);
                m_viewModel.Rank.Unsubscribe(SetRank);
                m_viewModel.Impact.Unsubscribe(SetImpact);
                m_viewModel.NotificationsCount.Unsubscribe(SetNotificationsCount);
                m_viewModel.RequestsCount.Unsubscribe(SetRequestsCount);
            }
        }

        private void SetAvatar(Sprite sprite) {
            m_avatar.SetAvatar(sprite);
        }

        private void SetRank(int rank) {
            m_rank.text = "RANK #" + rank.ToString();
        }

        private void SetImpact(float impact) {
            CultureInfo ci = new CultureInfo("en-us");
            m_impact.text = impact.ToString("C", ci);
        }

        public IPostlistContainer GetPostlistContainer() {
            return m_userCampaignContainer;
        }
        
        public void OnEditCampaignClick() {
            m_controller.OnEditClick(m_editButton.transform.position);
        }

        public void OnAddPost() {
            m_controller.OnAddPostClick();
        }
    }
}
