using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface ILoadNotificationsCommand : IService {
        void Execute(int offset, int limit, Action<List<NotificationModel>> receiver);
    }
}
