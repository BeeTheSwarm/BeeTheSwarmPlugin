using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class RenewAuthTokenCommand : BaseNetworkService<ResetAuthResponce>, ICommand {
        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private ILocalDataModel m_localDataModel;

        public RenewAuthTokenCommand() {

        }

        public void Execute() {
            SendPackage(new BTS_ResetAuth(m_networkService.AuthToken));
        }

        protected override void HandleSuccessResponse(ResetAuthResponce data) {
            m_networkService.AuthToken = data.AuthToken;
            m_localDataModel.SaveToken(data.AuthToken);
        }
    }
}
