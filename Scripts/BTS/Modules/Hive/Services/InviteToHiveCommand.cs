using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class InviteToHiveCommand : BaseNetworkService<NoDataResponse>,IInviteToHiveService {
        [Inject] private IPopupsModel m_popupsModel;
        private Action<List<UserModel>> m_callback;

        public void Execute(int userId) {
            SendPackage(new BTS_RequestInvitation(userId, 1));
        }


        protected override void HandleSuccessResponse(NoDataResponse data) {
             m_popupsModel.AddPopup(new ErrorPopupItemModel("Invitation has been sent"));
        }
    }
}
