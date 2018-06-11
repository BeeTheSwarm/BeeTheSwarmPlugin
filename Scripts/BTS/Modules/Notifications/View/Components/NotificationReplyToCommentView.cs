using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    public class NotificationReplyToCommentView : BaseCommentNotificationView {
        public override void SetMiddleText(string text) {
            base.SetMiddleText("Replied to a comment on your campaign: " + text);
        }
    }
}