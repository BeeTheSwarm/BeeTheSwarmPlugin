using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    public class NotificationWelcomeToHiveView : BaseNotificationView {
        private const string MESSAGE_TEMPLATE = "You accepted <color=#fff32b>{0}'s</color> invite. You are now part of her Hive";
        internal override void SetViewModel(NotificationViewModel obj) {
            base.SetViewModel(obj);
            SetMiddleText(string.Format(MESSAGE_TEMPLATE, obj.Username));
            obj.UserAvatar.Subscribe(SetLeftImage);
        }
    }
}