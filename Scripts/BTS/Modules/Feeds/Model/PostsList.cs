using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS {
    public class PostsList {
        private List<PostModel> m_data = new List<PostModel>();
        public event Action OnAddedPosts = delegate { };
        public event Action OnInsertedPosts = delegate { };
        public event Action OnPostRemoved = delegate { };

        public void AddPosts(List<PostModel> posts) {
            m_data.AddRange(posts);
            OnAddedPosts.Invoke();
        }

        public void Clear() {
            RemovePosts(GetPosts());
        }
        
        public List<PostModel> GetPosts(int offset, int count) {
            if (offset < m_data.Count) {
                var limit = Math.Min(count, m_data.Count - offset);
                return m_data.GetRange(offset, limit);
            }

            return new List<PostModel>();
        }

        public List<PostModel> GetPosts() {
            return m_data.GetRange(0, m_data.Count);
        }

        public bool HasItemsInRange(int offset, int count) {
            return m_data.Count >= offset + count;
        }

        internal IEnumerable<PostModel> FindPosts(int postId) {
            return m_data.FindAll(post => post.Id == postId);
        }

        public int Count() {
            return m_data.Count;
        }

        internal void RemovePostsByCampaign(int campaignId) {
            var toRemove = m_data.FindAll(post => post.Campaign.Id == campaignId);
            RemovePosts(toRemove);
        }

        private void RemovePosts(List<PostModel> toRemove) {
            if (toRemove.Count > 0) {
                toRemove.ForEach(post => { m_data.Remove(post); });
                OnPostRemoved.Invoke();
            }
        }

        internal void RemovePost(int postId) {
            var toRemove = m_data.FindAll(post => post.Id == postId);
            RemovePosts(toRemove);
        }

        public void InsertPosts(List<PostModel> dataPosts) {
            if (dataPosts.Count > 0) {
                m_data.InsertRange(0, dataPosts);
                OnInsertedPosts.Invoke();
            }
        }
    }
}