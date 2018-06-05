using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace BTS {
    public class ShortFeedSublist : BaseView, IPostlistContainer {
        [SerializeField] private Transform m_itemsParent;
        [SerializeField] private FeedsListItem m_itemsReference;
        public event Action OnClick = delegate { };
        private List<FeedsListItem> m_items = new List<FeedsListItem>();
        private ObservableList<PostViewModel> m_viewModel;

        public void OnButtonClick() {
            OnClick.Invoke();
        }

        internal void AddPost(PostViewModel obj) {
            if (!gameObject.activeInHierarchy) {
                gameObject.SetActive(true);
            }

            AddItem(obj);
        }

        internal void RemovePost(PostViewModel obj) {
            FeedsListItem item = m_items.Find(listItem => { return listItem.Model == obj; });
            if (item != null) {
                item.Unsubscribe();
                m_items.Remove(item);
                Destroy(item.gameObject);
            }
        }

        private void AddItem(PostViewModel postModel) {
            FeedsListItem item = GameObjectInstatiator.InstantiateFromObject(m_itemsReference);
            item.SetModel(postModel);
            item.transform.SetParent(m_itemsParent, false);
            item.transform.SetAsLastSibling();
            m_items.Add(item);
        }


        public void SetViewModel(ObservableList<PostViewModel> viewModel) {
            m_viewModel = viewModel;
            m_viewModel.Get().ForEach(post => { AddPost(post); });
            m_viewModel.OnAdd += AddPost;
            m_viewModel.OnRemove += RemovePost;
            m_viewModel.OnClear += RemovePosts;
            OnCountChanged(m_viewModel.Count());
            m_viewModel.OnCountChanged += OnCountChanged;
        }

        private void RemovePosts() {
            m_items.ForEach(item => {
                item.Unsubscribe();
                Destroy(item.gameObject);
            });
            m_items.Clear();
        }

        private void OnCountChanged(int obj) {
            gameObject.SetActive(obj > 0);
        }
    }
}