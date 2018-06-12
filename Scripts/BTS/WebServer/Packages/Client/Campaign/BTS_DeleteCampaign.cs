using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_DeleteCampaign : BTS_BasePackage<NoDataResponse> {

		public const string PackId = "DeleteCampaign";
        private int m_campaignId;
		public BTS_DeleteCampaign() : base (PackId) {
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            return requestFields;
		}

		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}