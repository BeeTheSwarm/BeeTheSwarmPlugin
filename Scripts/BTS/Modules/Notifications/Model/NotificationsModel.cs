using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class NotificationsModel : INotificationsModel {
        public event Action<int> NotificationsCountUpdated = delegate { };
        public event Action<NotificationModel> NotificationsInserted = delegate { };
        private List<NotificationModel> m_notifications;
        private int m_newNotifications = -1;
        private int m_totalNotifications = -1;

        public int LoadedNotifications {
            get {
                return m_notifications.Count;
            }
        }

        public int TotalNotifications {
            get { return m_totalNotifications; }
        }

        public int NewNotifications {
            get {
                return m_newNotifications;
            }
        }

        public NotificationsModel() {
            m_notifications = new List<NotificationModel>();
        }

        public void SetNotificationsCount(int count) {
            m_totalNotifications = count;
        }

        public void SetNewNotificationsCount(int count) {
            m_newNotifications = count;
            NotificationsCountUpdated.Invoke(m_newNotifications);
        }

        public void AddNotifications(List<NotificationModel> notifications) {
            m_notifications.AddRange(notifications);
        }

        public bool HasLoadAllNotifications() {
            return m_notifications.Count == m_totalNotifications;
        }

        public void InsertNotification(NotificationModel notification) {
            m_notifications.Insert(0, notification);
            NotificationsInserted.Invoke(notification);
        }

        public void SignOut() {
            SetNewNotificationsCount(0);
            SetNotificationsCount(0);
            m_notifications.Clear();
        }

        public List<NotificationModel> GetNotifications(int offset, int limit) {
            if (offset > m_notifications.Count) {
                return new List<NotificationModel>();
            }
            limit = Math.Min(limit, m_notifications.Count - offset);
            return m_notifications.GetRange(offset, limit);
        }

        public void SetTotalNotificationsCount(int count) {
            m_totalNotifications = count;
        }
    }
}