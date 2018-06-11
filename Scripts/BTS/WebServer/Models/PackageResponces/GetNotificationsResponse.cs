using System.Collections.Generic;

namespace BTS {
    public class GetNotificationsResponse : PackageResponse {
        public List<NotificationModel> Notifications { get; private set; }
        public int NotificationsCount { get; private set; }

        public override void Parse(Dictionary<string, object> data) {
            Notifications = new List<NotificationModel>();
            var notificationsSource = (List<object>)data["notifications"];
            
            foreach (object obj in notificationsSource) {
                Notifications.Add(NotificationsFactory.ReadNotification((Dictionary<string, object>)obj));
            }
            NotificationsCount = int.Parse(data["notifications_count"].ToString());
        }
    }

}