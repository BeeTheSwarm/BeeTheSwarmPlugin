using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_FeedCampaign : BTS_BasePackage<FeedCampaignResponce> {

		public const string PackId = "FeedCampaign";
        private int m_postId;
        private int m_count;

        public BTS_FeedCampaign(int postId, int count) : base (PackId) {
            m_postId = postId;
            m_count = count;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("post_id", m_postId);
            requestFields.Add("count", m_count);
			return requestFields;
		}
        
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}