﻿using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetUserCommand : BaseNetworkService<GetUserResponce>, IGetUserService {

        [Inject] private IUserProfileModel m_userModel;
        private Action<UserModel> m_callback;

        
        public void Execute(Action<UserModel> callback) {
            m_callback = callback;
            SendPackage(new BTS_GetUser());
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            FireErrorEvent(error);
            m_callback.Invoke(null);
        }

        protected override void HandleSuccessResponse(GetUserResponce data) {
            m_callback(data.User);
        }
    }
}
