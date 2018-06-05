﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace BTS {
    public class GreetingView : BaseControlledView<IGreetingViewListener>, IGreetingView, IDragHandler, IBeginDragHandler, IEndDragHandler {
        [SerializeField]
        private Animator m_animator;
        [SerializeField]
        private ErrorPopupView m_errorSubview;
        [SerializeField]
        private UserLoginPopupView m_userLoginSubview;
        [SerializeField]
        private UserInfoPopupView m_userInfoSubview;
        [SerializeField]
        private GameObject m_newRequestsSubview;
        private RectTransform m_rectTransform;
        private Vector3 m_dragBeginPoint;
        private PopupState m_popupState;
        private bool m_isDragging;

        private void Awake() {
            m_animator.enabled = false;
            m_rectTransform = GetComponent<RectTransform>();
        }

        public void ShowPopup(PopupItemModel item) {
            HideAll();
            switch (item.Type) {
                case PopupTypes.Error:
                    m_errorSubview.gameObject.SetActive(true);
                    m_errorSubview.Setup((ErrorPopupItemModel)item);
                    break;
                case PopupTypes.UserInfo:
                    m_userInfoSubview.gameObject.SetActive(true);
                    m_userInfoSubview.Setup((UserInfoPopupItemModel)item);
                    break;
                case PopupTypes.UserLogin:
                    m_userLoginSubview.gameObject.SetActive(true);
                    m_userLoginSubview.Setup((UserLoginPopupItemModel)item);
                    break;
                case PopupTypes.NewRequests:
                    m_newRequestsSubview.SetActive(true);
                    break;
                default:
                    return;
            }
            m_animator.enabled = true;
            m_animator.SetTrigger("Open");
            m_popupState = PopupState.Opening;
        }

        
        
        private void HideAll() {
            m_errorSubview.gameObject.SetActive(false);
            m_userLoginSubview.gameObject.SetActive(false);
            m_userInfoSubview.gameObject.SetActive(false);
            m_newRequestsSubview.SetActive(false);
        }

        private void OnPopupShown() {
            m_popupState = PopupState.Opened;
            StartCoroutine(HidePopup());
        }

        private void OnPopupHidden() {
            m_popupState = PopupState.Hidden;
            m_controller.OnPopupShown();
        }
        
        private IEnumerator HidePopup() {
            yield return new WaitForSecondsRealtime(1f);
            yield return new WaitUntil(() => !m_isDragging);
            if (m_popupState == PopupState.Opened) {
                m_animator.SetTrigger("Hide");
            }
        }

        public void OnDrag(PointerEventData eventData) {
            m_rectTransform.anchoredPosition = new Vector2(eventData.position.x - m_dragBeginPoint.x, m_rectTransform.anchoredPosition.y);
        }

        public void OnBeginDrag(PointerEventData eventData) {
            m_isDragging = true;
            m_dragBeginPoint = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (m_dragBeginPoint.x - eventData.position.x > 100) {
                m_popupState = PopupState.Hiding;
                m_animator.SetTrigger("SlideLeft");
            } else if (m_dragBeginPoint.x - eventData.position.x < -100) {
                m_popupState = PopupState.Hiding;
                m_animator.SetTrigger("SlideRight");
            }
            m_isDragging = false;
        }
        
        private enum PopupState {
            Hidden,
            Opening,
            Opened,
            Hiding
        }
    }

    
}