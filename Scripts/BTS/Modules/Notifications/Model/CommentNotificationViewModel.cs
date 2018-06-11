using UnityEngine;
using System.Collections;
namespace BTS {
    public class CommentNotificationViewModel: PostNotificationViewModel {
        public readonly string Comment;

        public CommentNotificationViewModel(NotificationModel notification): base(notification) {
            Comment = ((PostRelatednotificationModel) notification).Comment.Text;
        }
    }
}