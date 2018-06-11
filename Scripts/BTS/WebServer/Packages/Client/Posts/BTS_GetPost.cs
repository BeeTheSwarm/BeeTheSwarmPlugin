using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetPost : BTS_BasePackage<MockPackResponce> {

		public const string PackId = "GetPost";
        private int m_postId;
		public BTS_GetPost(int postId) : base (PackId) {
            m_postId = postId;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("post_id", m_postId);
            return requestFields;
		}

		public override bool AuthenticationRequired {
			get {
				return false;
			}
		}

		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}