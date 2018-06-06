using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    public class BaseCommentNotificationView : BasePostNotificationView {
        protected CommentNotificationViewModel m_commentViewModel;
        
        internal override void SetViewModel(NotificationViewModel obj) {
            base.SetViewModel(obj);
            m_commentViewModel = (CommentNotificationViewModel)obj;
            SetTitle(obj.Username);
            obj.UserAvatar.Subscribe(SetLeftImage);
            CommentNotificationViewModel viewModel = (CommentNotificationViewModel)obj;
            SetMiddleText(viewModel.Comment);
        }
        
    }
}