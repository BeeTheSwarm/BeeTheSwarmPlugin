using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetPostsCommand : BaseNetworkService<GetPostsResponse>, IGetPostsService {
        [Inject]
        private IFeedsModel m_model;
        private Action<List<PostModel>> m_callback;
        private int m_offset;
        private int m_limit;

        private bool m_isLoading = false;
        private Queue<RequestData> m_queue = new Queue<RequestData>();
        public void Execute(int offset, int limit, Action<List<PostModel>> callback) {
            if (m_isLoading) {
                m_queue.Enqueue(new RequestData(offset, limit, callback));
                return;
            }

            if (m_model.CampaignsList.HasItemsInRange(offset, limit)) {
                callback.Invoke(m_model.CampaignsList.GetPosts(offset, limit));
            }
            else {
                var offsetToLoad = Math.Max(offset, m_model.CampaignsList.Count());
                var limitToLoad = limit - offsetToLoad + offset;
                if (limit <= 0) {
                    callback.Invoke(new List<PostModel>());
                    return;
                }
                m_callback = callback;
                m_offset = offset;
                m_limit = limit;
                m_isLoading = true;
                SendPackage(new BTS_GetPosts(offsetToLoad, limitToLoad));
            }
            
        }
        
        protected override void HandleSuccessResponse(GetPostsResponse data) {
            m_model.UpdatePosts(data.Posts);
            m_model.CampaignsList.AddPosts(data.Posts);
            m_callback.Invoke(m_model.CampaignsList.GetPosts(m_offset, m_limit));
            m_isLoading = false;
            if (m_queue.Count > 0) {
                var r = m_queue.Dequeue();
                Execute(r.Offset, r.Limit, r.Callback);
            }
        }

        private class RequestData {
            public int Limit { get; private set; }
            public int Offset{ get; private set; }
            public Action<List<PostModel>> Callback{ get; private set; }

            public RequestData(int offset, int limit, Action<List<PostModel>> callback) {
                Limit = limit;
                Offset = offset;
                Callback = callback;
            }
        }
    }
}
