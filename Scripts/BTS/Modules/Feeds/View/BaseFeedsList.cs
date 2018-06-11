using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BTS
{
    public class BaseFeedsList : MonoBehaviour, IPostlistContainer
    {
        [SerializeField] private Transform m_itemsParent;
        [SerializeField] private FeedsListItem m_feedItemOrigin;
        private List<FeedsListItem> m_items = new List<FeedsListItem>();
        
        private ObservableList<PostViewModel> m_viewModel;
        
        public void SetInitCallback(Action callback) {
            
        }
        
        protected virtual void AddPost(PostViewModel obj) {
            if (!gameObject.activeInHierarchy) {
                gameObject.SetActive(true);
            }
            AddItem(obj);
        }

        protected virtual void RemovePost(PostViewModel obj) {
            FeedsListItem item = m_items.Find(listItem => { return listItem.Model == obj; });
            if (item != null) {
                m_items.Remove(item);
                item.Unsubscribe();
                Destroy(item.gameObject);
            }
        }

        protected virtual FeedsListItem AddItem(PostViewModel postModel) {
            var item = Instantiate(m_feedItemOrigin, m_itemsParent);
            item.SetModel(postModel);
            item.transform.SetAsLastSibling();
            m_items.Add(item);
            return item;
        }

        public void SetViewModel(ObservableList<PostViewModel> viewModel) {
            m_viewModel = viewModel;
            m_viewModel.OnAdd += AddPost;
            m_viewModel.OnRemove += RemovePost;
            m_viewModel.OnClear += ClearPosts;
        }

        protected virtual void ClearPosts() {
            m_items.ForEach(item => {
                item.Unsubscribe();
                Destroy(item.gameObject);
            });
            m_items.Clear();
        }
    }
}