using System;
using System.IO;

namespace BTS {
    public class StartupService : BaseService, IStartupService {
        [Inject] private INetworkService m_networkService;
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private ILocalDataModel m_localData;
        [Inject] private IGetUserService m_getUserService;
        [Inject] private IPopupsModel m_popupsModel;
        [Inject] private ILoadInitDataService m_loadInitDataService;

        public void Execute(Action callback) {
            var token = m_localData.GetToken();
            if (!string.IsNullOrEmpty(token)) {
                m_networkService.AuthToken = token;
                m_getUserService.OnErrorReceived += ErrorHandler;
                m_getUserService.Execute(user => {
                    if (user != null) {
                        m_userModel.SetUserModel(user);
                        m_popupsModel.AddPopup(new UserLoginPopupItemModel(user.Name));
                        m_popupsModel.AddPopup(new UserInfoPopupItemModel(user.Bees, user.Level));
                        m_loadInitDataService.Execute(callback);
                    }
                    else {
                        callback.Invoke();    
                    }
                });
            }
            else {
                callback.Invoke();
            }
        }

        private void ErrorHandler(BTS_Error error) {
            m_getUserService.OnErrorReceived -= ErrorHandler;
            if (error.Code == ApiErrors.USER_IS_UNCONFIRMED) {
                m_userModel.State = UserState.Unconfirmed;
            }
        }
    }
}