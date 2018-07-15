using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class ResendCodeService : BaseNetworkService<AuthTokenResponce>, IResendCodeService {

        [Inject]
        private IUserProfileModel m_userModel;

        [Inject] private ILocalDataModel m_localDataModel;
        [Inject] private IPopupsModel m_popupsModel;

        public ResendCodeService() {
        }

        public void Execute(string email) {
            SendPackage(new BTS_ResendCode(email));
        }
        
        protected override void HandleSuccessResponse(AuthTokenResponce data) {
            m_networkService.AuthToken = data.AuthToken;
            m_localDataModel.SaveToken(data.AuthToken);
            m_popupsModel.AddPopup(new ErrorPopupItemModel("Verification code sent on your email"));
        }

    }
}
