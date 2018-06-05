using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class DeleteCampaignCommand : BaseNetworkService<NoDataResponse>,IDeleteCampaignService {

        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IFeedsModel m_feedModel;
        [Inject]
        private IUserProfileService m_userService;
        private Action<bool> m_callback; 

        public DeleteCampaignCommand() {
            
        }

        public void Execute(Action<bool> callback) {
            m_callback = callback;
            SendPackage(new BTS_DeleteCampaign());
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(false);
        }

        protected override void HandleSuccessResponse(NoDataResponse data) {
            m_feedModel.DeleteUserCampaign();
            m_callback.Invoke(true);
        }

    }
}
