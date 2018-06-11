using UnityEngine;
using System.Collections;
namespace BTS {
    public class NotificationsScreenViewModel {
        public readonly ObservableList<NotificationViewModel> Notifications = new ObservableList<NotificationViewModel>();
        public readonly Observable<bool> NotificationsLoaded = new Observable<bool>();
    }
}