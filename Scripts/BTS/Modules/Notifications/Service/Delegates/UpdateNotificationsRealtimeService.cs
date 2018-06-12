namespace BTS {
    internal class UpdateNotificationsRealtimeService : BaseNetworkService<GetNewCountersResponce>, IUpdateNotificationsRealtime {
        
        [Inject] private INotificationsModel m_model; 
        [Inject] private IRequestsModel m_requestsModel; 
        [Inject] private IProcessNotificationsRealtimeService m_processNotificationsService; 
        [Inject] private IUpdateRequestsRealTimeService m_updateRequestsRealTimeService; 

        public void Execute() {
            m_networkService.SendPackage(new BTS_GetNewCounters());
        }

        protected override void HandleSuccessResponse(GetNewCountersResponce data) {
            var newNotifications = m_model.NewNotifications;
            if (newNotifications != data.NotificationsCount) {
                m_model.SetNewNotificationsCount(data.NotificationsCount);
                m_processNotificationsService.Execute();
            }

            var newRequst = m_requestsModel.NewRequests;
            if (newRequst != data.RequestsCount) {
                m_updateRequestsRealTimeService.Execute();
            }
        }
    }
}