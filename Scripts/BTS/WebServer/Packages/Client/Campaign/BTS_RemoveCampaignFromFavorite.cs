using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_RemoveCampaignFromFavorite: BTS_BasePackage<NoDataResponse> {

		public const string PackId = "UnsetCampaignFromFavourite";
        private int m_campaignId;
		public BTS_RemoveCampaignFromFavorite(int campaignId) : base (PackId) {
            m_campaignId = campaignId;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("campaign_id", m_campaignId);
            return requestFields;
		}
        
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}