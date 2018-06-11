using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS
{
    public interface INotificationsService : IService {
        void GetNotifications(int offset, int limit, Action<List<NotificationModel>> receiver) ;
    }
}