using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS
{
    public interface INotificationsModel : IModel {
        int LoadedNotifications { get; }
        int TotalNotifications { get; }
        int NewNotifications { get; }

        void SetNewNotificationsCount(int count);
        event Action<int> NotificationsCountUpdated;
        event Action<NotificationModel> NotificationsInserted;
        void AddNotifications(List<NotificationModel> notifications);
        void SetTotalNotificationsCount(int count);
        bool HasLoadAllNotifications();
        void InsertNotification(NotificationModel notification);
        void SignOut();
    }
}