using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
	
	internal class BTS_ResetAuth : BTS_BasePackage {

		public const string PackId = "ResetAuth";
		public string curAuthToken;

		public BTS_ResetAuth(string cur_auth_token):base(PackId) {
			this.curAuthToken = cur_auth_token;
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

			OriginalJSON.Add("auth_token", curAuthToken);

			return OriginalJSON;
		}
	}
}
