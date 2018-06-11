using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    public class NotificationCommentToPostView : BaseCommentNotificationView {

        public override void SetMiddleText(string text) {
            if (text.Length > 58) {
                var lastSpace = text.Substring(0, 55).LastIndexOf(" ");
                if (lastSpace != -1) {
                    text = text.Substring(0, lastSpace);
                } else {
                    text = text.Substring(0, 55);
                }
                text += "...";
            }
            base.SetMiddleText("Commented on your campaign: " + text);
        }
    }
}