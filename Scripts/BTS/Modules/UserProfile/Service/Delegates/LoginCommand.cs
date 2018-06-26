using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class LoginCommand : BaseNetworkService<LoginResponce>, ILoginService {
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IUserProfileService m_userService;
        [Inject] private IPopupsModel m_popupsModel;
        [Inject] private ILocalDataModel m_localDataModel;
        [Inject] private ILoadInitDataService m_loadInitDataService;
        private Action<bool> m_callback;
        private string m_email;

        public void Execute(string email, string password, Action<bool> callback) {
            m_callback = callback;
            m_email = email;
            SendPackage(new BTS_Login(email, password));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(false);
        }

        protected override void HandleSuccessResponse(LoginResponce data) {
            m_networkService.AuthToken = data.AuthToken;
            m_userModel.SetUserModel(data.User);
            m_popupsModel.AddPopup(new UserLoginPopupItemModel(data.User.Name));
            m_popupsModel.AddPopup(new UserInfoPopupItemModel(data.User.Bees, data.User.Level, data.User.Progress));
            m_localDataModel.SaveToken(data.AuthToken);
            m_localDataModel.SaveUserId(data.User.Id);
            m_loadInitDataService.Execute(() => {
                m_callback.Invoke(true);
            });
            
        }
    }
}