using System;
using System.Collections.Generic;

namespace BTS {
    internal class ProcessNotificationsRealtimeService : BaseNetworkService<GetNotificationsResponse>, IProcessNotificationsRealtimeService{
        [Inject] private INotificationsModel m_model;
        [Inject] private IFeedsModel m_feedsModel;
        [Inject] private IHiveModel m_hiveModel;

        public void Execute() {
            SendPackage(new BTS_GetNotifications(0, 1));
        }
        
        protected override void HandleSuccessResponse(GetNotificationsResponse data) {
            if (data.Notifications.Count == 0) {
                return;
            }

            var notification = data.Notifications[0];
            m_model.InsertNotification(notification);
            switch (notification.Action) {
                case NotificationAction.CommentToPost:
                    UpdatePost(notification);
                    break;
                case NotificationAction.DonateToCampaign:
                case NotificationAction.CampaignFunded:
                    UpdateCampaign(notification);
                    break;
                case NotificationAction.HiveRequestConfirmed:
                case NotificationAction.UserJoinedByInvite:
                case NotificationAction.WelcomeToHive:
                    m_hiveModel.UpdateHive();
                    break;
            }
        }

        private void UpdateCampaign(NotificationModel notification) {
            var campaignRelated = (CampaignRelatedNotificationModel) notification;
            m_feedsModel.UpdateCampaings(campaignRelated.Campaign);
        }

        private void UpdatePost(NotificationModel notification) {
            var postRelated = (PostRelatednotificationModel) notification;
            m_feedsModel.UpdatePostAddComment(postRelated.Post.Id, new CommentModel(postRelated.User, postRelated.Comment));
        }
    }
}