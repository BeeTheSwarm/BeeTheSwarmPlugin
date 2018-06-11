using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

    internal class BTS_RequestInvitation : BTS_BasePackage<NoDataResponse> {

        public const string PackId = "RequestInvitation";
        public int m_userId = 0;
        public int m_type = 1;

        public BTS_RequestInvitation(int userId, int type) : base(PackId) {
            m_userId = userId;
            m_type = type;
        }

        public override Dictionary<string, object> GenerateData() {
            Dictionary<string, object> OriginalJSON = new Dictionary<string, object>();
            OriginalJSON.Add("user_id", m_userId);
            OriginalJSON.Add("type", m_type);
            return OriginalJSON;
        }

        public override Int32 Timeout {
            get {
                return 5;
            }
        }
    }
}
