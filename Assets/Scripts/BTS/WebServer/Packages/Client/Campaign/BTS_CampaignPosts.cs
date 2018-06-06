using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_CampaignPosts : BTS_BasePackage<MockPackResponce> {

		public const string PackId = "CampaignPosts";
        private int m_campaignId = 0;
		public BTS_CampaignPosts(int campaignId) : base (PackId) {
            m_campaignId = campaignId;

        }
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("campaign_id", m_campaignId);
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