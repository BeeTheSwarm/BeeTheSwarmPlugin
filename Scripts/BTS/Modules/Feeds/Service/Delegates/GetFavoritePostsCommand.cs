using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetFavoritePostsCommand : BaseNetworkService<GetPostsResponse>, IGetFavoritePostsService {

        [Inject]
        private IFeedsModel m_model;

        private int m_offset;
        private int m_limit;
        private Action<List<PostModel>> m_callback;

        public void Execute(int offset, int limit, Action<List<PostModel>> callback) {
            if (m_model.FavoriteCampaignsList.HasItemsInRange(offset, limit)) {
                callback.Invoke(m_model.FavoriteCampaignsList.GetPosts(offset, limit));
            }
            else {
                var offsetToLoad = Math.Max(offset, m_model.FavoriteCampaignsList.Count());
                var limitToLoad = limit - offsetToLoad + offset;
                if (limit <= 0) {
                    callback.Invoke(new List<PostModel>());
                    return;
                }
                m_callback = callback;
                m_offset = offset;
                m_limit = limit;
                SendPackage(new BTS_GetFavouritePosts(offsetToLoad, limitToLoad));
            }
            
        }
        
        protected override void HandleSuccessResponse(GetPostsResponse data) {
            m_model.UpdatePosts(data.Posts);
            m_model.FavoriteCampaignsList.AddPosts(data.Posts);
            m_callback.Invoke(m_model.FavoriteCampaignsList.GetPosts(m_offset, m_limit));
        }
    }
}
