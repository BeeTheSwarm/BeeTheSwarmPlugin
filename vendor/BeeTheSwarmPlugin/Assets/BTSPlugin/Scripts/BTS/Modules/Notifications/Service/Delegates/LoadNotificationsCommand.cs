using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class LoadNotificationsCommand : BaseNetworkService<GetNotificationsResponse>,ILoadNotificationsCommand {
        [Inject] private INotificationsModel m_model;
        private Action<List<NotificationModel>> m_callback;
        public LoadNotificationsCommand() {
        }

        public void Execute(int offset, int limit, Action<List<NotificationModel>> receiver) {
            m_callback = receiver;
            SendPackage(new BTS_GetNotifications(offset, limit));
        }

        protected override void HandleSuccessResponse(GetNotificationsResponse data) {
            m_model.AddNotifications(data.Notifications);
            m_model.SetTotalNotificationsCount(data.NotificationsCount);
            m_callback.Invoke(data.Notifications);
        }

    }
}
