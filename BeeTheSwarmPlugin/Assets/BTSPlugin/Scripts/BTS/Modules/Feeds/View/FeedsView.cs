using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Globalization;

namespace BTS {
    public class FeedsView : TopPanelScreen<IFeedsViewListener>, IFeedsView, ITopPanelContainer {
        [SerializeField]
        private Text m_impact;
        [SerializeField]
        private ShortFeedSublist m_newCampaigns;
        [SerializeField]
        private ShortFeedSublist m_hotCampaigns;
        [SerializeField]
        private ShortFeedSublist m_favouriteCampaigns;

        private FeedListViewModel m_viewModel;

        public void SetViewModel(FeedListViewModel model) {
            m_viewModel = model;
            m_viewModel.Impact.Subscribe(SetImpact);
        }

        public void SetImpact(float impact) {
            CultureInfo ci = new CultureInfo("en-us");
            m_impact.text = impact.ToString("C", ci);
        }

        public void OnCommentCampaignClick() {

        }

        public void OnInviteClick() {
            m_controller.OnInviteClick();
        }

        public void OnShowAllCampaignsClick() {
            m_controller.OnShowAllCampaigns();
        }

        public void OnShowFavouriteCampaignsClick() {
            m_controller.OnShowFavouriteCampaigns();
        }

        public void OnShowRecentCampaignsClick() {
            m_controller.OnShowRecentCampaigns();
        }

        public void OnShowTopCampaignsClick() {
            m_controller.OnShowTopCampaigns();
        }

        public IPostlistContainer GetNewPostsContainer() {
            return m_newCampaigns;
        }

        public IPostlistContainer GetFavoritePostsContainer() {
            return m_favouriteCampaigns;
        }

        public IPostlistContainer GetHotPostsContainer() {
            return m_hotCampaigns;
        }
    }
}