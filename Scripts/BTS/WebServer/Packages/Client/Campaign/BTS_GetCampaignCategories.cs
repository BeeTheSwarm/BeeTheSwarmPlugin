using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetCampaignCategories : BTS_BasePackage<GetCampaignCategoriesResponse> {

		public const string PackId = "GetCampaignCategories";
        
		public BTS_GetCampaignCategories() : base (PackId) {
		}
		
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}