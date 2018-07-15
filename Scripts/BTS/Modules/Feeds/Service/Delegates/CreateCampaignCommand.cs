using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class CreateCampaignCommand : BaseNetworkService<GetPostsResponse>, ICreateCampaignService {
        [Inject] private IFeedsModel m_feedModel;
        [Inject] private IUserProfileModel m_userModel;

        private Action<bool> m_callback;
        
        public void Execute(string campaignTitle, int category, string website, string postTitle, string postDescription, Texture2D image, Action<bool> callback) {
            m_callback = callback;
            SendPackage(new BTS_CreateCampaign(campaignTitle, category, website, postTitle, postDescription, image));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(false);
        }

        protected override void HandleSuccessResponse(GetPostsResponse data) {
            m_feedModel.AddNewCampaign(data.Posts);
            m_callback.Invoke(true);
        }
    }
}