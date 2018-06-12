using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class ConfirmRegistrationCommand : BaseNetworkService<AuthCodeResponce>, IConfirmRegistrationService {
        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private ILocalDataModel m_localDataModel;
        private Action<bool> m_callback;

        public ConfirmRegistrationCommand() {
        }

        public void Execute(int verificationCode, Action<bool> callback) {
            m_callback = callback;
            SendPackage(new BTS_AuthCode(verificationCode));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(false);
        }

        protected override void HandleSuccessResponse(AuthCodeResponce data) {
            m_userModel.SetUserModel(data.User);
            m_localDataModel.SaveUserId(data.User.Id);
            m_callback.Invoke(true);
        }

    }
}
