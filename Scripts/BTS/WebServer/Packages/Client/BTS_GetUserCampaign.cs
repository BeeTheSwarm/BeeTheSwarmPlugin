using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetUserCampaign : BTS_BasePackage<GetUserCampaignsResponce> {

		public const string PackId = "GetUserCampaign";
        private int m_count;
        private int m_offset;
		public BTS_GetUserCampaign(int offset = 0, int count = 1) : base (PackId) {
            m_count = count;
            m_offset = offset;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("offset", m_offset);
            requestFields.Add("limit", m_count);
            return requestFields;
		}

		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}