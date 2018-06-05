using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetUserCampaignCommand : BaseNetworkService<GetUserCampaignsResponce>,IGetUserCampaignService {
        [Inject]
        private IFeedsModel m_feedModel;
        private Action<List<PostModel>> m_callback;

        public GetUserCampaignCommand() {
        }

        public void Execute(Action<List<PostModel>> callback) {
            if (m_feedModel.UserCampaign.Count() > 0) {
                callback.Invoke(m_feedModel.UserCampaign.GetPosts().GetRange(0,1));
                return;
            }
            m_callback = callback;
            SendPackage(new BTS_GetUserCampaign());
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(new List<PostModel>());
        }

        protected override void HandleSuccessResponse(GetUserCampaignsResponce data) {
            m_feedModel.UpdatePosts(data.Posts);
            m_feedModel.AddUserCampaign(data.Posts);
            m_callback.Invoke(m_feedModel.UserCampaign.GetPosts().GetRange(0,Math.Min(1, m_feedModel.UserCampaign.Count() )));
        }
    }
}
