using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class FeedCampaignCommand : BaseNetworkService<FeedCampaignResponce>, IFeedCampaignService {
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IFeedsModel m_model;
        [Inject] private IPopupsModel m_popupsModel;

        public void Execute(int campaingId, int count) {
            if (m_userModel.User.Bees < count) {
                m_popupsModel.AddPopup(new ErrorPopupItemModel("Not enough bees"));
                return;
            }
            SendPackage(new BTS_FeedCampaign(campaingId, count));
        }

        protected override void HandleSuccessResponse(FeedCampaignResponce data) {
            m_model.UpdatePostUserFed(data.Post);
            m_userModel.SetLevel(data.User.Level, data.User.Progress);
            m_userModel.SetBees(data.User.Bees);
            m_userModel.SetImpact(data.User.Impact);
        }
    }
}