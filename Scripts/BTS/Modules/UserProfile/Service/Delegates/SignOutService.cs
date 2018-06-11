namespace BTS {
    public class SignOutService: BaseService, ISignOutService {
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IFeedsModel m_feedsModel;
        [Inject] private ILocalDataModel m_localData;
        [Inject] private INetworkService m_networkService;
        [Inject] private INotificationsModel m_notificationsModel;
        [Inject] private IRequestsModel m_requestsModel;
        [Inject] private IHiveModel m_hiveModel;
        
        public void Execute() {
            m_networkService.SignOut();
            m_localData.SaveToken(string.Empty);
            m_userModel.SetUserModel(null);
            m_feedsModel.SignOut();
            m_notificationsModel.SignOut();
            m_requestsModel.SignOut();
            m_hiveModel.SignOut();
        }
    }
}