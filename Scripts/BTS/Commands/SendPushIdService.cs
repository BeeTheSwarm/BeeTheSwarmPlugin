using System;
using UnityEngine;
using System.Collections;
using BTS;

namespace BTS {

    internal class SendPushIdService : BaseNetworkService<NoDataResponse>, ISendPushIdService {

        public void Execute(string userId) {
            Debug.Log("Send notification ID");
            if (!String.IsNullOrEmpty(userId)) {
                SendPackage(new SetupPushId(userId));
            }
        }

        protected override void HandleSuccessResponse(NoDataResponse data) {
            Debug.Log("User push ID sent");
        }
    }
}
