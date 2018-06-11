using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    public static class NotificationsFactory {
        public static NotificationModel ReadNotification(Dictionary<string, object> notificationData) {
            NotificationModel result;
            var action = (NotificationAction)int.Parse(notificationData["action"].ToString());
            switch (action) {
                case NotificationAction.CampaignFunded:
                case NotificationAction.DonateToCampaign:
                    result = new CampaignRelatedNotificationModel();
                    break;
                case NotificationAction.CommentToPost:
                case NotificationAction.ReplyToComment:
                    result = new PostRelatednotificationModel();
                    break;
                default:
                    result = new NotificationModel();
                    break;
                  
            }
            result.ParseJSON(notificationData);
            return result;
        }
    }
}
