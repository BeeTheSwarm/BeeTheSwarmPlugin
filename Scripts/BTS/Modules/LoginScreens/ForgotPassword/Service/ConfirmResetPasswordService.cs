namespace BTS {
    internal class ConfirmResetPasswordService: BaseNetworkService<AuthTokenResponce>, IConfirmResetPassword {
        [Inject] private ILoadInitDataService m_loadInitDataService;
        [Inject] private IGetUserService m_getUserService;
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IPopupsModel m_popupsModel;
        
        protected override void HandleSuccessResponse(AuthTokenResponce data) {
            m_networkService.AuthToken = data.AuthToken;
            m_getUserService.Execute(user => {
                if (user != null) {
                    m_userModel.SetUserModel(user);
                    m_popupsModel.AddPopup(new UserLoginPopupItemModel(user.Name));
                    m_popupsModel.AddPopup(new UserInfoPopupItemModel(user.Bees, user.Level));
                    m_loadInitDataService.Execute(FireSuccessFinishEvent);
                }
            });
        }

        
        public void Execute(string login, int code, string password, string confirmPassword) {
            SendPackage(new BTS_ConfirmResetPassword(login, code, password, confirmPassword));
        }
    }
}