namespace BTS {
    public class UpdateHiveService: BaseService, IUpdateHiveService {
        [Inject] private IGetUserService m_getUserService;
        [Inject] private IHiveModel m_hiveModel;
        [Inject] private IUserProfileModel m_userModel;
        public void Execute() {
            if (m_userModel.User.HiveId != 0) {
                m_getUserService.Execute(UserReceivedHandler);
            }
            else {
                m_hiveModel.UpdateHive();
            } 
        }

        private void UserReceivedHandler(UserModel user) {
            if (user == null) {
                return;
            }
            m_userModel.User.UpdateInfo(user);
            m_hiveModel.UpdateHive();
        }
    }
}