using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class RegisterService : BaseNetworkService<AuthTokenResponce>, IRegisterService {
        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IUserProfileService m_userService;
        [Inject]
        private ILocalDataModel m_localDataModel;
        private Action<bool> m_callback;
        [Inject]
        private IPopupsModel m_popupsModel;

        public RegisterService() {

        }

        public void Execute(string name, string email, string password, string referalCode, Action<bool> callback) {
            m_callback = callback;
            ulong offset = TimeDateUtils.GetUTCOffset();
            SendPackage(new BTS_Register(name, email, password, offset, referalCode));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(false);
        }

        protected override void HandleSuccessResponse(AuthTokenResponce data) {
            m_networkService.AuthToken = data.AuthToken;
            m_localDataModel.SaveToken(data.AuthToken);
            m_callback.Invoke(true);
        }
    }
}
