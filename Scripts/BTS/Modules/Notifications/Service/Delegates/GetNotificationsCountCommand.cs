using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetCounters : BaseNetworkService<GetNewCountersResponce>, IGetCountersService {
        [Inject] private INotificationsModel m_notificationsModel;
        [Inject] private IRequestsModel m_requestsModel;

        public void Execute() {
            BTS_GetNewCounters pack = new BTS_GetNewCounters();
            pack.Handler = this;
            m_networkService.SendPackage(pack);
        }

        protected override void HandleSuccessResponse(GetNewCountersResponce data) {
            m_notificationsModel.SetNewNotificationsCount(data.NotificationsCount);
            m_requestsModel.SetNewRequestsCount(data.RequestsCount);
            FireSuccessFinishEvent();
        }
    }
}