using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    public class BasePostNotificationView : BaseNotificationView {
        [SerializeField]
        private Image m_postImage;
        protected PostNotificationViewModel m_viewModel;
        
        internal override void SetViewModel(NotificationViewModel obj) {
            base.SetViewModel(obj);
            m_viewModel =(PostNotificationViewModel)obj;
            m_viewModel.PostImage.Subscribe(SetPostImage);
        }
        
        private void SetPostImage(Sprite obj) {
            m_postImage.overrideSprite = obj;
        }
    }
}