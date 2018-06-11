using UnityEngine;
using System.Collections;
namespace BTS {
    public class NotificationViewModel {
        public int Id { get; private set; }
        public readonly string Username;
        public readonly Observable<Sprite> UserAvatar = new Observable<Sprite>();
        public readonly int Date;
        public readonly NotificationAction Action;
        public readonly int Subject;
        public readonly int Value;

        public NotificationViewModel(NotificationModel notification) {
            Id = notification.Id;
            Username = notification.User.Name;
            Date = notification.CreatedAt;
            Action = notification.Action;
            Subject = notification.Entity;
            Value = notification.Value;
        }
    }
}