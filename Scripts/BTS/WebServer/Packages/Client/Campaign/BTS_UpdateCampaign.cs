using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_UpdateCampaign : BTS_BasePackage<GetCampaignResponse> {

		public const string PackId = "UpdateCampaign";

        private string m_title;
        private string m_address;
        private string m_website;
        private int m_category;
        private string packId;

        public BTS_UpdateCampaign(string title, int category, string website) : base (PackId) {
            m_title = title;
            m_website = website;
            m_category = category;
        }

        public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("title", m_title);
            requestFields.Add("address", "some address");
            requestFields.Add("website", m_website);
            requestFields.Add("category", m_category);
            return requestFields;
		}
        
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}