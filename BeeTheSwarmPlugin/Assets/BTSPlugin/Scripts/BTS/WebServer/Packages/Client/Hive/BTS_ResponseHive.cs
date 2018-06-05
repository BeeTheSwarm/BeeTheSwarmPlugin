using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

    internal class BTS_ResponseInvitation : BTS_BasePackage<GetUserResponce> {

        public const string PackId = "ResponseInvitation";
        public int m_invitationId = 0;
        public int m_status = 0;

        public BTS_ResponseInvitation(int invitationId, int status) : base(PackId) {
            m_invitationId = invitationId;
            m_status = status;
        }

        public override Dictionary<string, object> GenerateData() {
            Dictionary<string, object> OriginalJSON = new Dictionary<string, object>();
            OriginalJSON.Add("invitation_id", m_invitationId);
            OriginalJSON.Add("status", m_status);
            return OriginalJSON;
        }

        public override Int32 Timeout {
            get {
                return 5;
            }
        }
    }
}
