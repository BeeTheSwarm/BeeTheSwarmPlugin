using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class FeedsController : TopPanelScreenController<IFeedsView>, IFeedsController, IFeedsViewListener {
        [Inject] private IPostListControllerDelegate m_topPostsControllerDelegate;
        [Inject] private IPostListControllerDelegate m_favoritePostsControllerDelegate;
        [Inject] private IPostListControllerDelegate m_newPostsControllerDelegate;
        [Inject] private IFeedsService m_feedService;
        [Inject] private IFeedsModel m_feedModel;
        [Inject] private IImagesService m_imagesService;
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IPopupsModel m_popupsModel;
        [Inject] private IUserProfileController m_userScreen;
        [Inject] private ICampaignCategoriesModel m_categoriesModel;
        [Inject] private IViewCampaignController m_viewCampaignController;
        [Inject] private IInviteFriendsController m_inviteFriendsController;
        [Inject] private ISameTypePostsController m_sameTypePostsController;
        [Inject] private IGetTopPostsService m_topPostLoaderService;
        [Inject] private IGetFavoritePostsService m_favoritePostLoaderService;
        [Inject] private IGetPostsService m_postLoaderService;
        [Inject] private ILoadInitDataService m_loadInitDataService;
        [Inject] private IGetImpactService m_getImpactService;
        private FeedListViewModel m_viewModel = new FeedListViewModel();
        
        public override void PostInject() {
            base.PostInject();
            m_feedModel.OnImpactUpdated += m_viewModel.Impact.Set;
            m_topPostsControllerDelegate.SetMaxItems(3);
            m_topPostsControllerDelegate.SetItemsSource(m_topPostLoaderService.Execute);
            m_favoritePostsControllerDelegate.SetMaxItems(3);
            m_favoritePostsControllerDelegate.SetItemsSource(m_favoritePostLoaderService.Execute);
            m_newPostsControllerDelegate.SetMaxItems(3);
            m_newPostsControllerDelegate.SetItemsSource(m_postLoaderService.Execute);
            m_userModel.OnUserStateChanged += UserStateChangedHandler;
            m_getImpactService.OnSuccessFinish += UpdateImpact;
        }

        private void UpdateImpact() {
            m_viewModel.Impact.Set(m_feedModel.Impact);
        }

        private void UserStateChangedHandler(UserState state) {
            switch (state) {
                case UserState.LoggedIn:
                    AddListeners();
                    Show();
                    break;
                default:
                    LogoutHandler();
                    break;
            }
        }

        private void LogoutHandler() {
            m_feedModel.CampaignsList.OnInsertedPosts -= UpdateNewCampaign;
            m_feedModel.CampaignsList.OnPostRemoved -= UpdateNewCampaign;
            m_feedModel.TopCampaignsList.OnInsertedPosts -= UpdateTopCampaigns;
            m_feedModel.TopCampaignsList.OnPostRemoved -= UpdateTopCampaigns;
            m_feedModel.FavoriteCampaignsList.OnInsertedPosts -= UpdateFavoriteCampaign;
            m_feedModel.FavoriteCampaignsList.OnPostRemoved -= UpdateFavoriteCampaign;
            m_favoritePostsControllerDelegate.Clear();
        }

        private void AddListeners() {
            m_feedModel.CampaignsList.OnInsertedPosts += UpdateNewCampaign;
            m_feedModel.CampaignsList.OnPostRemoved += UpdateNewCampaign;
            m_feedModel.TopCampaignsList.OnInsertedPosts += UpdateTopCampaigns;
            m_feedModel.TopCampaignsList.OnPostRemoved += UpdateTopCampaigns;
            m_feedModel.FavoriteCampaignsList.OnInsertedPosts += UpdateFavoriteCampaign;
            m_feedModel.FavoriteCampaignsList.OnPostRemoved += UpdateFavoriteCampaign;
        }

        private void UpdateTopCampaigns() {
            m_topPostsControllerDelegate.Clear();
            m_topPostsControllerDelegate.Update();
        }

        private void UpdateFavoriteCampaign() {
            m_favoritePostsControllerDelegate.Clear();
            m_favoritePostsControllerDelegate.Update();
        }

        private void UpdateNewCampaign() {
            m_newPostsControllerDelegate.Clear();
            m_newPostsControllerDelegate.Update();
        }

        protected override bool AvatarEnabled {
            get {
                return true;
            }
        }

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetInitCallback(() => {
                m_topPanelControllerDelegate.SetView(m_view.GetTopPanel());
                m_topPostsControllerDelegate.SetView(m_view.GetHotPostsContainer());
                m_favoritePostsControllerDelegate.SetView(m_view.GetFavoritePostsContainer());
                m_newPostsControllerDelegate.SetView(m_view.GetNewPostsContainer());
            });
            m_view.SetViewModel(m_viewModel);
        }

        public override void Show() {
            base.Show();
            m_view.Show();
            if (m_loadInitDataService.Loaded) {
                UpdateFeed();
            }
            else {
                m_loadInitDataService.OnLoad += InitDataLoadedHandler;
            }
        }

        private void InitDataLoadedHandler() {
            m_loadInitDataService.OnLoad -= InitDataLoadedHandler;
            UpdateFeed();
        }

        private void UpdateFeed() {
            m_topPostsControllerDelegate.Update();
            m_favoritePostsControllerDelegate.Update();
            m_newPostsControllerDelegate.Update();
            m_getImpactService.Execute();
        }

        public void OnShowAllCampaigns() {

        }

        public void OnShowFavouriteCampaigns() {
            m_sameTypePostsController.Show(PostTypes.Favorite);
        }

        public void OnShowRecentCampaigns() {
            m_sameTypePostsController.Show(PostTypes.New);

        }

        public void OnShowTopCampaigns() {
            m_sameTypePostsController.Show(PostTypes.Hot);
        }

        public void OnInviteClick() {
            m_inviteFriendsController.Show();
        }

    }
}