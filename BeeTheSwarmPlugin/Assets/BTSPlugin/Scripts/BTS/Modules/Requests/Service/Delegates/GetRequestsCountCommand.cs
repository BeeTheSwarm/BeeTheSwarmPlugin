using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetRequestsCountCommand : BaseNetworkService<GetInvitationsResponse>, IGetRequestsCommand {
        [Inject] private IRequestsModel m_model;
        [Inject] private IPopupsModel m_popupsModel;

        public void Execute(int offset, int limit) {
            SendPackage(new BTS_GetInvitations(offset, limit));
        }

        protected override void HandleSuccessResponse(GetInvitationsResponse data) {
            var invitations = m_model.NewRequests;
            m_model.SetNewRequestsCount(data.InvitationsCount);
            m_model.AddRequests(data.Invitations);
            if (invitations == -1 && data.InvitationsCount > 0) {
                m_popupsModel.AddPopup(new NewRequestsItemModel());
            }
        }
    }
}