using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetTopFeed : BTS_BasePackage<MockPackResponce> {

		public const string PackId = "GetTopFeed";

		public BTS_GetTopFeed() : base (PackId) {
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
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