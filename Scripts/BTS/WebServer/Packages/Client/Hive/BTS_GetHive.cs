using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

    internal class BTS_GetHive : BTS_BasePackage<GetHiveResponse> {

        public const string PackId = "GetHive";
        public int m_hiveId = 0;
        public int m_offset = 0;
        public int m_limit = 0;

        public BTS_GetHive(int hiveId, int offset, int limit) : base(PackId) {
            m_hiveId = hiveId;
            m_offset = offset;
            m_limit = limit;

        }

        public override Dictionary<string, object> GenerateData() {
            Dictionary<string, object> OriginalJSON = new Dictionary<string, object>();
            OriginalJSON.Add("hive_id", m_hiveId);
            OriginalJSON.Add("offset", m_offset);
            OriginalJSON.Add("limit", m_limit);
            return OriginalJSON;
        }

        public override Int32 Timeout {
            get {
                return 5;
            }
        }
    }
}
