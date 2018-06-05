using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace BTS {
    internal class SearchHiveItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        [SerializeField]
        private Avatar m_avatar;
        [SerializeField]
        private Text m_username;
        private UserViewModel m_viewModel;
        private ScrollRect m_parentScroll;
        private bool m_wasDragged;
        public void SetViewModel(UserViewModel viewModel) {
            m_viewModel = viewModel;
            m_username.text = viewModel.Username;
            m_viewModel.Avatar.Subscribe(m_avatar.SetAvatar);
        }

        public void ClickHandler() {
            if (!m_wasDragged) {
                m_viewModel.OnItemClicked();
            }
        }

        private ScrollRect GetParentScroll() {
            if (m_parentScroll == null) {
                m_parentScroll = gameObject.GetComponentInParent<ScrollRect>();
            }
            return m_parentScroll;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            m_wasDragged = true;
            GetParentScroll().OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData) {
            GetParentScroll().OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData) {
            m_wasDragged = false;
            GetParentScroll().OnEndDrag(eventData);
        }
        
    }
}
