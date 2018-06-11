using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetCampaignLevels : BTS_BasePackage<GetCampaignLevelsResponce> {

		public const string PackId = "GetCampaignLevels";
        
		public BTS_GetCampaignLevels() : base (PackId) {
		}
		
        public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}