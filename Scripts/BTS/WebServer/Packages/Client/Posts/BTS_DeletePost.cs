using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_DeletePost : BTS_BasePackage<NoDataResponse> {

		public const string PackId = "DeletePost";
        private int m_postId;
		public BTS_DeletePost(int postId) : base (PackId) {
            m_postId = postId;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("post_id", m_postId);
            return requestFields;
		}
        
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}