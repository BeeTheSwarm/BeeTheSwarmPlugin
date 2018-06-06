using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class NotificationsService : BaseService, INotificationsService {
        [Inject]
        private INetworkService m_networkService;
        [Inject]
        private INotificationsModel m_model;
        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IGetCountersService m_getCountersService;
        [Inject]
        private ILoadNotificationsCommand m_loadNotificationsService;


        public override void PostInject() {
        //    m_userModel.OnUserLoggedIn += OnUserLoggedIn;
        }

        private void OnUserLoggedIn() {
            m_userModel.OnUserLoggedIn -= OnUserLoggedIn;
            m_getCountersService.Execute();
        }

        public void GetNotifications(int offset, int limit, Action<List<NotificationModel>> receiver) {
            if (m_model.HasLoadAllNotifications()) {
                receiver.Invoke(new List<NotificationModel>());
                return;
            }
            if (offset + limit > m_model.LoadedNotifications) {
                m_loadNotificationsService.Execute(m_model.LoadedNotifications, offset + limit - m_model.LoadedNotifications, receiver);
            }
        }

    }
}