using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class LoadRequestsCommand : BaseNetworkService<GetInvitationsResponse>, ILoadRequestsCommand {

        [Inject] private IRequestsModel m_model;
        private Action<List<InvitationModel>> m_callback;
        
        public void Execute(int offset, int limit, Action<List<InvitationModel>> receiver) {
            m_callback = receiver;
            SendPackage(new BTS_GetInvitations(offset, limit));
        }

        protected override void HandleSuccessResponse(GetInvitationsResponse data) {
            m_model.AddRequests(data.Invitations);
            m_model.SetNewRequestsCount(data.InvitationsCount);
            if (m_callback != null) {
                m_callback.Invoke(data.Invitations);
                m_callback = null;
            } 
        }

    }
}
