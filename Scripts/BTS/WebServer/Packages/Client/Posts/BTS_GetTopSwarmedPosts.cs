using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetTopSwarmedPosts : BTS_BasePackage<GetPostsResponse> {

		public const string PackId = "GetTopSwarmedPosts";
        private int m_limit;
        private int m_offset;
		public BTS_GetTopSwarmedPosts(int offset = 0, int limit = 10) : base (PackId) {
            m_offset = offset;
            m_limit = limit;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> data =  new Dictionary<string, object>();
            data.Add("offset", m_offset);
            data.Add("limit", m_limit);
            return data;
		}

		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}