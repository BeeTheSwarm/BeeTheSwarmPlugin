using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace BTS {
    public class NotificationsView : BaseControlledView<INotificationsViewListener>, INotificationsView {
        [SerializeField]
        private Transform m_notificationParent;
        [SerializeField]
        private ScrollRect m_notificationScroll;
        [SerializeField]
        private GameObject m_noNotificationsText;
        [Header("Item origins")]
        [SerializeField]
        private NotificationCampaignFundedView m_campaignFunded;
        [SerializeField]
        private NotificationCommentToPostView m_commentToPost;
        [SerializeField]
        private NotificationDonateToCampaignView m_donateToCampaign;
        [SerializeField]
        private NotificationFriendRequestView m_friendRequest;
        [SerializeField]
        private NotificationHiveRequestConfirmedView m_hiveRequestConfirmed;
        [SerializeField]
        private NotificationPhotoUploadRewardView m_photoUploadReward;
        [SerializeField]
        private NotificationReplyToCommentView m_replyToComment;
        [SerializeField]
        private NotificationRequestToHiveView m_requestToHive;
        [SerializeField]
        private NotificationUserJoinedByInviteView m_userJoinedByInvite;
        [SerializeField]
        private NotificationWelcomeToHiveView m_welcomeToHive;


        private NotificationsScreenViewModel m_viewModel;
        private List<BaseNotificationView> m_items = new List<BaseNotificationView>();
        private Dictionary<NotificationAction, BaseNotificationView> m_itemOrigins = new Dictionary<NotificationAction, BaseNotificationView>();
        private void Awake() {
            m_itemOrigins.Add(NotificationAction.CampaignFunded, m_campaignFunded);
            m_itemOrigins.Add(NotificationAction.CommentToPost, m_commentToPost);
            m_itemOrigins.Add(NotificationAction.DonateToCampaign, m_donateToCampaign);
            m_itemOrigins.Add(NotificationAction.FriendRequest, m_friendRequest);
            m_itemOrigins.Add(NotificationAction.HiveRequestConfirmed, m_hiveRequestConfirmed);
            m_itemOrigins.Add(NotificationAction.PhotoUploadRewarded, m_photoUploadReward);
            m_itemOrigins.Add(NotificationAction.ReplyToComment, m_replyToComment);
            m_itemOrigins.Add(NotificationAction.RequestAddToHive, m_requestToHive);
            m_itemOrigins.Add(NotificationAction.UserJoinedByInvite, m_userJoinedByInvite);
            m_itemOrigins.Add(NotificationAction.WelcomeToHive, m_welcomeToHive);
        }

        public void OnBackPressed() {
            m_controller.OnBackPressed();
        }

        
        private void NotificationsCountChangeHandler(int count) {
            m_noNotificationsText.gameObject.SetActive(count == 0 && m_viewModel.NotificationsLoaded.Get());
            m_notificationScroll.gameObject.SetActive(count > 0);
        }

        
        public void SetViewModel(NotificationsScreenViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.Notifications.OnAdd += OnAddNotification;
            m_viewModel.Notifications.OnCountChanged += NotificationsCountChangeHandler;
            m_viewModel.Notifications.OnClear += NotificationsClearHandler;
            m_viewModel.NotificationsLoaded.Subscribe(NotificationLoadedChangeHandler);
        }

        private void NotificationsClearHandler() {
            m_items.ForEach(item => {
                Destroy(item.gameObject);
            });
            m_items.Clear();
        }

        private void NotificationLoadedChangeHandler(bool loaded) {
            m_noNotificationsText.gameObject.SetActive(m_viewModel.Notifications.Count() == 0 && loaded);
        }

        private void OnAddNotification(NotificationViewModel obj) {
            BaseNotificationView item;
            if (m_itemOrigins.ContainsKey(obj.Action)) {
                item = GameObjectInstatiator.InstantiateFromObject(m_itemOrigins[obj.Action]);
                item.transform.SetParent(m_notificationParent, false);
                item.gameObject.SetActive(true);
                item.transform.SetAsLastSibling();
                item.SetViewModel(obj);
                m_items.Add(item);
            }
        }

        public void OnScrolledToEnd() {
            m_controller.OnScrolledToEnd();
        }
    }
}