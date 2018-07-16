using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class UserProfileScreenController : BaseScreenController<IUserProfileScreen>, IUserProfileController, IUserProfileScreenListener {
        [Inject] private INotificationsModel m_notificationsModel;
        [Inject] private ITopPanelController m_topPanelControllerDelegate;
        [Inject] private IPostListControllerDelegate m_postListControllerDelegate;
        [Inject] private IImagesService m_imagesService;
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IFeedsService m_feedsService;
        [Inject] private IFeedsModel m_feedsModel;
        [Inject] private IStartCampaignController m_startCampaignController;
        [Inject] private IEditProfileController m_editProfileController;
        [Inject] private IHiveController m_hiveController;
        [Inject] private IGetHiveService m_getHiveService;
        [Inject] private IInviteFriendsController m_inviteFriendsController;
        [Inject] private IHiveLeaderboardController m_hiveLeaderboardController;
        [Inject] private INotificationsController m_notificationsController;
        [Inject] private IOurGamesController m_ourGamesController;
        [Inject] private IPopupsModel m_popupsModel;
        [Inject] private IViewCampaignController m_viewCampaignController;
        [Inject] private IYesNoPopupController m_yesNoController;

        [Inject] private ISignOutService m_signOutService;
        [Inject] private IDeleteCampaignService m_deleteCampaignService;
        [Inject] private IGetUserCampaignService m_getUserCampaignService;
        [Inject] private IRequestsScreenController m_requestsScreenController;
        [Inject] private IRequestsModel m_requestsModel;
        [Inject] private IEditCampaignController m_editCampaignController;
        [Inject] private IUpdateCampaignController m_updateCampaignController;
        [Inject] private IAddPostController m_addPostController;
        [Inject] private IYesNoPopupController m_yesNoPopup;

        [Inject] private ICampaignToolboxController m_campaignToolbox;
        [Inject] private ILoaderController m_loader;
        [Inject] private ITutorialController m_minigameController;
        
        private UserProfileViewModel m_viewModel = new UserProfileViewModel();

        public override void PostInject() {
            base.PostInject();
            m_topPanelControllerDelegate.SetAvatarEnabled(false);
            m_topPanelControllerDelegate.SetBackButtonEnabled(true);
            m_topPanelControllerDelegate.OnBackBtnPressed += OnBackPressed;
            m_postListControllerDelegate.SetMaxItems(1);
            m_postListControllerDelegate.PostsEditable = true;
            m_postListControllerDelegate.SetItemsSource((offset, limit, callback) => {
                m_loader.Show("Loading user campaign");
                m_getUserCampaignService.Execute(list => {
                    callback.Invoke(list);
                    m_loader.Hide();
                });
            });
            m_viewModel.NotificationsCount.Set(m_notificationsModel.NewNotifications);
            m_notificationsModel.NotificationsCountUpdated += m_viewModel.NotificationsCount.Set;
            m_feedsModel.OnCampaignCreated += CampaignCreatedHandler;
            m_requestsModel.RequestsCountUpdated += m_viewModel.RequestsCount.Set;
            m_userModel.OnUserStateChanged += UserStateChangedHandler;
        }
        
        private void UserStateChangedHandler(UserState state) {
            switch (state) {
                case UserState.LoggedIn:
                    AddListeners();
                    m_userModel.User.OnAvatarChanged += OnAvatarChange;
                    if (m_userModel.User.HiveId == 0) {
                        m_userModel.User.OnHiveIdChanged += HiveIdChangedHandler;
                    } else {
                        m_userModel.OnImpactChanged += ImpactChangedHandler;
                    }
                    break;
                default:
                    LogoutHandler();
                    break;
            }
        }

        private void LogoutHandler() {
            m_feedsModel.UserCampaign.OnInsertedPosts -= OnUpdateUsersPosts;
            m_feedsModel.UserCampaign.OnPostRemoved -= OnUpdateUsersPosts;
            m_postListControllerDelegate.Clear();
        }

        private void AddListeners() {
            m_feedsModel.UserCampaign.OnInsertedPosts += OnUpdateUsersPosts;
            m_feedsModel.UserCampaign.OnPostRemoved += OnUpdateUsersPosts;
        }

        private void ImpactChangedHandler(float obj) {
            m_viewModel.Impact.Set(obj);
        }

        private void OnUpdateUsersPosts() {
            m_postListControllerDelegate.Clear();
            m_postListControllerDelegate.Update();
        }

        private void CampaignCreatedHandler() {
            m_postListControllerDelegate.Update();
        }

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(m_viewModel);
            m_view.SetInitCallback(() => {
                m_topPanelControllerDelegate.SetView(m_view.GetTopPanel());
                m_postListControllerDelegate.SetView(m_view.GetPostlistContainer());
                m_postListControllerDelegate.Update();
            });
        }

        public override void Show() {
            base.Show();
            m_imagesService.GetImage(m_userModel.User.Avatar, m_viewModel.Avatar.Set);
            m_viewModel.RequestsCount.Set(m_requestsModel.NewRequests); 
            if (m_userModel.User.HiveId == 0) {
                m_viewModel.Impact.Set(m_userModel.User.Impact);
            } else {
                m_getHiveService.Execute(m_userModel.User.HiveId, GetHiveHandler, 0 , 1);
            }
            m_postListControllerDelegate.Update();
        }

        private void HiveIdChangedHandler() {
            m_userModel.OnImpactChanged -= ImpactChangedHandler;
            m_getHiveService.Execute(m_userModel.User.HiveId, GetHiveHandler, 0, 1);
        }

        private void GetHiveHandler(List<UserModel> arg1, int arg2, UserModel arg3, int totalHiveImpact) {
            m_viewModel.Impact.Set(totalHiveImpact);
        }

        private void OnAvatarChange() {
            m_imagesService.GetImage(m_userModel.User.Avatar, m_viewModel.Avatar.Set);

        }

        public void OnAboutClick() {
            m_minigameController.Show();
        }
        
        public void OnCreateCampaignClick() {
            m_startCampaignController.Show();
        }

        public void OnDeleteCampaignClick() {
            m_yesNoController.Show("Are you sure you want to delete your campaign?\n\nYou will lose all bees for this campaign", "Delete", "Cancel", responce => {
                if (responce == YesNoPopupResponce.Confirmed) {
                    m_deleteCampaignService.Execute((result => { }));
                    m_postListControllerDelegate.Clear();
                }
            });
            
        }

        public void OnEditProfileClick() {
            m_editProfileController.Show();
        }

        public void OnHiveClick() {
            m_hiveController.Show(m_userModel.User.HiveId);
        }

        public void OnInviteFriendsClick() {
            m_inviteFriendsController.Show();
        }
        
        public void OnNotificationsClick() {
            m_notificationsController.Show();
        }

        public void OnOurGamesClick() {
            m_ourGamesController.Show();
        }
        
        public void OnViewAllPostsClick() {
            m_viewCampaignController.Show(m_feedsModel.UserCampaign.GetPosts()[0].Campaign.Id);
        }

        private void OnPlayerReady() {
        }

        public void OnCampaignToolboxClick() {
            m_campaignToolbox.Show();
        }

        public void OnSignOutClick() {
            m_yesNoPopup.Show("Are you sure you want to logout?", "Logout", "Cancel", result => {
                if (result == YesNoPopupResponce.Confirmed) {
                    m_signOutService.Execute();            
                }
            });
        }

        public void OnRequestsClick() {
            m_requestsScreenController.Show();
        }

        public void OnEditClick(Vector3 position) {
            m_editCampaignController.Show(responce => {
                switch (responce) {
                    case EditMenuResponce.NoAction:

                        break;
                    case EditMenuResponce.DeleteCampaign:
                        OnDeleteCampaignClick();
                        break;
                    case EditMenuResponce.EditCampaign:
                        m_updateCampaignController.Show();
                        break;
                }
            }, position);
        }

        public void OnAddPostClick() {
            m_addPostController.Show();
        }
    }
}