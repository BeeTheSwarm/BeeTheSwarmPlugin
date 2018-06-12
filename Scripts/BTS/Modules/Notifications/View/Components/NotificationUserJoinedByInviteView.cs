using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    public class NotificationUserJoinedByInviteView : BaseNotificationView {
        internal override void SetViewModel(NotificationViewModel obj) {
            base.SetViewModel(obj);
            SetTitle(obj.Username);
            obj.UserAvatar.Subscribe(SetLeftImage);
        }
    }
}