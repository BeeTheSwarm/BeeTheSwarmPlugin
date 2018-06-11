namespace BTS {
    public class UpdateRequestsRealtimeService: BaseNetworkService<GetInvitationsResponse>, IUpdateRequestsRealTimeService {
        [Inject] private IRequestsModel m_model;

        public void Execute() {
            SendPackage(new BTS_GetInvitations(0, 1));
        }

        protected override void HandleSuccessResponse(GetInvitationsResponse data) {
            m_model.SetNewRequestsCount(data.InvitationsCount);
            if (data.Invitations.Count == 0) {
                return;
            }
            var requests = m_model.GetRequests(0, 1);
            if (requests.Count == 0 || requests[0].Id != data.Invitations[0].Id) {
                m_model.InsertRequest(data.Invitations[0]);
            }
        }
    }
}