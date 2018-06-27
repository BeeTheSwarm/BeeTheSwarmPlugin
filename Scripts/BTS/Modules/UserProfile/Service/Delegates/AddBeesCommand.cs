using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class AddBeesService : BaseNetworkService<AddBeesResponse>, IAddBeesService {
        [Inject]
        private IUserProfileModel m_userModel;
        
        public void Execute(int count) {
            SendPackage(new BTS_AddBees(count));
        }

        protected override void HandleSuccessResponse(AddBeesResponse data) {
            m_userModel.SetLevel(data.User.Level, data.User.Progress);
            m_userModel.SetBees(data.User.Bees);
        }
    }
}
