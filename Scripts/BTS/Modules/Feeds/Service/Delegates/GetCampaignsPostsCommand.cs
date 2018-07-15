using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetCampaignsPostsCommand : BaseNetworkService<GetPostsResponse>, IGetCampaignsPostsService {
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IFeedsModel m_model;


        private Action<List<PostModel>> m_callback;

        public void Execute(int campaignId, int offset, int limit, Action<List<PostModel>> callback) {
            m_callback = callback;
            SendPackage(new BTS_GetCampaignPosts(campaignId, offset, limit));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback(new List<PostModel>());
        }

        protected override void HandleSuccessResponse(GetPostsResponse data) {
            m_model.UpdatePosts(data.Posts);
            m_callback(data.Posts);
        }
    }
}