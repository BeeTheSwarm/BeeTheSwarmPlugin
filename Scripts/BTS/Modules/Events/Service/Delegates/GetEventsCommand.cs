using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetEventsCommand : BaseNetworkService<GetEventsResponse>, IGetEventsService {
        [Inject]
        private IEventsModel m_model;

        private Action<List<EventModel>, List<ProgressModel>> m_callback;
        
        public void Execute(Action<List<EventModel>, List<ProgressModel>> callback) {
            m_callback = callback;
            SendPackage(new BTS_GetEvents());
        }

        protected override void HandleSuccessResponse(GetEventsResponse data) {
            m_model.SetEvents(data.Events);
            m_callback.Invoke(data.Events, data.Progress);
        }
    }
}
