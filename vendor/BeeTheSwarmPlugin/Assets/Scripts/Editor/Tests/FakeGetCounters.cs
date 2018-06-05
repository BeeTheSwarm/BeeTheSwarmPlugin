namespace BTS.Testing {
    public class FakeGetCounters : BaseService, IGetCountersService {
        [Inject] private INotificationsModel m_notificationsModel;
        [Inject] private IRequestsModel m_requestsModel;

        private int m_result;

        public FakeGetCounters(int result) {
            m_result = result;
        }

        public void Execute() {
            switch (m_result) {
                case 1:
                    m_notificationsModel.SetNewNotificationsCount(5);
                    break;
                case 2:
                    m_requestsModel.SetNewRequestsCount(5);
                    break;
                case 3:
                    m_notificationsModel.SetNewNotificationsCount(5);
                    m_requestsModel.SetNewRequestsCount(5);
                    break;
            }
            FireSuccessFinishEvent();
        }
    }
}