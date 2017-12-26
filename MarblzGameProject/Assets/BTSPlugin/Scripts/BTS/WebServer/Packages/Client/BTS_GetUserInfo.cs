using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetUserInfo : BTS_BasePackage {

		public const string PackId = "GetUserInfo";
		public int userID;

		public BTS_GetUserInfo(int userID):base(PackId) {
			this.userID = userID;
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

		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

			OriginalJSON.Add("user_id", userID);

			return OriginalJSON;
		}
	}
}

