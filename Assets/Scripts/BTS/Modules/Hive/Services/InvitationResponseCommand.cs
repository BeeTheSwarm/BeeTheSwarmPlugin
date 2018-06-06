using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class InvitationResponseCommand : BaseNetworkService<GetUserResponce>, IInvitationResponseService {
        [Inject] private IPopupsModel m_popupsModel;
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IRequestsModel m_requestsModel;
        [Inject] private IGetUserService m_getUserService;
        [Inject] private IUpdateHiveService m_updateHiveService;
        private Action<bool> m_callback;
        private InvitationModel m_invitation;
        private int m_status;

        public void Execute(int requestId, int status) {
            m_invitation = m_requestsModel.GetRequest(requestId);
            m_status = status;
            if (m_invitation != null) {
                SendPackage(new BTS_ResponseInvitation(requestId, status));
            }
        }

        protected override void HandleSuccessResponse(GetUserResponce data) {
            m_requestsModel.RemoveRequest(m_invitation);
            if (m_status == 1) {
                if (data.User != null) {
                    m_userModel.User.UpdateInfo(data.User);
                    if (data.User.HiveId != 0) {
                        m_popupsModel.AddPopup(new ErrorPopupItemModel("Welcome to hive"));
                        m_updateHiveService.Execute();
                    }
                }
            }
        }
    }
}