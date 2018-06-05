using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetPostComments : BTS_BasePackage<GetPostCommentsResponse> {

		public const string PackId = "GetPostComments";
        private int m_postId;
        private int m_offset;
        private int m_limit;
        public BTS_GetPostComments(int postId, int offset, int limit) : base (PackId) {
            m_postId = postId;
            m_offset = offset;
            m_limit = limit;
        }
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("post_id", m_postId);
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