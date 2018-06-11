using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace BTS {
    public class RequestsScreenView : BaseControlledView<IRequestsScreenViewListener>, IRequestsScreenView {
        [SerializeField]
        private Transform m_itemsParent;
        [SerializeField]
        private GameObject m_noRequestText;
        [SerializeField]
        private ScrollRect m_requestsScroll;
        [Header("Item origins")]
        [SerializeField]
        private RequestItemView m_requestToHive;
        [SerializeField]
        private RequestItemView m_requestToFriends;

        private RequestsScreenViewModel m_viewModel;
        private List<RequestItemView> m_items = new List<RequestItemView>();
        private Dictionary<InvitationType, RequestItemView> m_itemOrigins = new Dictionary<InvitationType, RequestItemView>();
        private void Awake() {
            m_itemOrigins.Add(InvitationType.HiveInvite, m_requestToHive);
            m_itemOrigins.Add(InvitationType.FriendInvite, m_requestToFriends);
        }

        public void OnBackPressed() {
            m_controller.OnBackPressed();
        }

        public void SetViewModel(RequestsScreenViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.Requests.OnAdd += OnAddRequest;
            m_viewModel.Requests.OnRemove += OnRemoveRequest;
            viewModel.Requests.OnCountChanged += SetupButtons;
        }

        private void OnRemoveRequest(RequestItemViewModel obj) {
            var view = m_items.Find(v => v.Id == obj.Id);
            if (view != null) {
                m_items.Remove(view);
                Destroy(view.gameObject);
            }
        }

        private void SetupButtons(int obj) {
            m_noRequestText.gameObject.SetActive(obj == 0);
            m_requestsScroll.gameObject.SetActive(obj != 0);
        }

        private void OnAddRequest(RequestItemViewModel obj) {
            RequestItemView item;
            if (m_itemOrigins.ContainsKey(obj.RequestType)) {
                item = GameObjectInstatiator.InstantiateFromObject(m_itemOrigins[obj.RequestType]);
                item.transform.SetParent(m_itemsParent, false);
                item.gameObject.SetActive(true);
                item.transform.SetAsLastSibling();
                item.SetViewModel(obj);
                m_items.Add(item);
            }
        }

        public void OnScrolledToEnd() {
            m_controller.OnScrolledToEnd();
        }
        
    }
}