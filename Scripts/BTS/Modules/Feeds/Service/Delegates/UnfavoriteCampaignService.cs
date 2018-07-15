using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;
namespace BTS {
    internal class UnfavoriteCampaignService : BaseNetworkService<NoDataResponse>,IUnfavoriteCampaignService {
        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IFeedsModel m_feedModel;

        private CampaignModel campaign;
        
        public void Execute(int postId) {
            var post = m_feedModel.GetPost(postId);
            if (post != null) {
                campaign = post.Campaign;
                if (!campaign.IsFavorite) {
                    return;
                }
                SendPackage(new BTS_RemoveCampaignFromFavorite(campaign.Id));
            }
        }

        protected override void HandleSuccessResponse(NoDataResponse data) {
            m_feedModel.RemoveCampaignFromFavorite(campaign);
        }

    }
}
