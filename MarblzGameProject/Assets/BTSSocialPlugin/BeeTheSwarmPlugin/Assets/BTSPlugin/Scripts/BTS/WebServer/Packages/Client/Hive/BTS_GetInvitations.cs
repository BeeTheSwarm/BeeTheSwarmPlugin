using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

    internal class BTS_GetInvitations : BTS_BasePackage<GetInvitationsResponse> {
        public const string PackId = "GetInvitations";
        private int m_limit;
        private int m_offset;
        public BTS_GetInvitations(int offset, int limit) : base(PackId) {
            m_offset = offset;
            m_limit = limit;
        }

        public override Dictionary<string, object> GenerateData() {
            Dictionary<string, object> requestFields = new Dictionary<string, object>();
            requestFields.Add("offset", m_offset);
            requestFields.Add("limit", m_limit);
            return requestFields;
        }

        public override Int32 Timeout {
            get {
                return 5;
            }
        }
    }
}