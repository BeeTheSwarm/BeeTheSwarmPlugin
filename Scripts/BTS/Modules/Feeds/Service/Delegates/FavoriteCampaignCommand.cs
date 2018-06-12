using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;
namespace BTS {
    internal class FavoriteCampaignCommand : BaseNetworkService<NoDataResponse>,IFavoriteCampaignService {
        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IFeedsModel m_feedModel;
        [Inject]
        private IUserProfileService m_userService;
        private CampaignModel campaign;
        public FavoriteCampaignCommand() {
        }

        public void Execute(int postId) {
            var post = m_feedModel.GetPost(postId);
            if (post != null) {
                campaign = post.Campaign;
                if (campaign.IsFavorite) {
                    return;
                }
                SendPackage(new BTS_AddCampaignToFavorite(campaign.Id));
            }
        }

        protected override void HandleSuccessResponse(NoDataResponse data) {
            m_feedModel.SetCampaignFavorite(campaign);
        }

    }
}
