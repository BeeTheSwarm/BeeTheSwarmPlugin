using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetTopPostsCommand : BaseNetworkService<GetPostsResponse>, IGetTopPostsService {
        [Inject]

        private IFeedsModel m_model;
        [Inject]
        private IUserProfileService m_userService;
        private int m_offset;
        private int m_limit;
        private Action<List<PostModel>> m_callback;

        public GetTopPostsCommand() {
        }

        public void Execute(int offset, int limit, Action<List<PostModel>> callback) {
            if (m_model.TopCampaignsList.HasItemsInRange(offset, limit)) {
                callback.Invoke(m_model.TopCampaignsList.GetPosts(offset, limit));
            }
            else {
                var offsetToLoad = Math.Max(offset, m_model.TopCampaignsList.Count());
                var limitToLoad = limit - offsetToLoad + offset;
                if (limit <= 0) {
                    callback.Invoke(new List<PostModel>());
                    return;
                }
                m_callback = callback;
                m_offset = offset;
                m_limit = limit;
                SendPackage(new BTS_GetTopSwarmedPosts(offsetToLoad, limitToLoad));
            }
            
        }
        
        protected override void HandleSuccessResponse(GetPostsResponse data) {
            m_model.UpdatePosts(data.Posts);
            m_model.TopCampaignsList.AddPosts(data.Posts);
            m_callback.Invoke(m_model.TopCampaignsList.GetPosts(m_offset, m_limit));
        }
    }
}
