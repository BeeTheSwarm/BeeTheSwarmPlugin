using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetCampaignPosts : BTS_BasePackage<GetPostsResponse> {

		public const string PackId = "GetCampaign";
        private int m_campaignId;
        private int m_offset;
        private int m_limit;

        public BTS_GetCampaignPosts(int campaignId, int offset, int limit) : base (PackId) {
            m_campaignId = campaignId;
            m_offset = offset;
            m_limit = limit;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("campaign_id", m_campaignId);
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