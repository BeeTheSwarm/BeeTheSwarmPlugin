using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetEvents : BTS_BasePackage {

		public const string PackId = "GetEvents";

		public BTS_GetEvents() : base (PackId) {
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

			return OriginalJSON;
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