using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    public class NotificationRequestToHiveView : BaseNotificationView {
        
        internal override void SetViewModel(NotificationViewModel obj) {
            base.SetViewModel(obj);
            SetTitle(obj.Username);
            obj.UserAvatar.Subscribe(SetLeftImage);
        }
    }
}