using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

    internal class BTS_SearchHive : BTS_BasePackage<SearchHiveResponse> {

        public const string PackId = "SearchHive";
        public string m_keyword;
        public int m_offset = 0;
        public int m_limit = 0;

        public BTS_SearchHive(string keyword, int offset, int limit) : base(PackId) {
            m_keyword = keyword;
            m_offset = offset;
            m_limit = limit;

        }

        public override Dictionary<string, object> GenerateData() {
            Dictionary<string, object> OriginalJSON = new Dictionary<string, object>();
            OriginalJSON.Add("search", m_keyword);
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
