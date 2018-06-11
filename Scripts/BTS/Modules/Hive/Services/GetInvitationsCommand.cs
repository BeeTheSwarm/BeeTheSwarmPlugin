using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetInvitationsCommand : BaseNetworkService<GetInvitationsResponse> {
        private Action<List<InvitationModel>, int> m_callback;
        public GetInvitationsCommand() {
        }

        public void Execute(int offset, int limit, Action<List<InvitationModel>, int> callback) {
            m_callback = callback;
            SendPackage(new BTS_GetInvitations(offset, limit));
        }
        
        protected override void HandleSuccessResponse(GetInvitationsResponse data) {
            m_callback.Invoke(data.Invitations, data.InvitationsCount);
        }
    }
}
