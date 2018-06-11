using UnityEngine;
using System.Collections;
namespace BTS {
    public class PostNotificationViewModel: NotificationViewModel {
        public readonly Observable<Sprite> PostImage = new Observable<Sprite>();

        public PostNotificationViewModel(NotificationModel notification): base(notification) {

        }
    }
}