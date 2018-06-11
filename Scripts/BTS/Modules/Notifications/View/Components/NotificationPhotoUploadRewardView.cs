using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    public class NotificationPhotoUploadRewardView : BaseNotificationView {
        [SerializeField]
        private Image m_photoImage;
        internal override void SetViewModel(NotificationViewModel obj) {
            base.SetViewModel(obj);
            obj.UserAvatar.Subscribe(SetLeftImage);
        }
    }
}