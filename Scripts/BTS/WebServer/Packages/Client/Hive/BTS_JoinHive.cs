using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

    internal class BTS_JoinHive : BTS_BasePackage<GetUserResponce> {

        public const string PackId = "JoinHive";
        private int m_userId = 0;

        public BTS_JoinHive(int userId) : base(PackId) {
            m_userId = userId;
        }

        public override Dictionary<string, object> GenerateData() {
            Dictionary<string, object> OriginalJSON = new Dictionary<string, object>();
            OriginalJSON.Add("user_id", m_userId);
            return OriginalJSON;
        }

        public override Int32 Timeout {
            get {
                return 5;
            }
        }
    }
}
