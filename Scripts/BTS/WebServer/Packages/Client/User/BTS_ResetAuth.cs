using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
	
	internal class BTS_ResetAuth : BTS_BasePackage<ResetAuthResponce> {

		public const string PackId = "ResetAuth";
		public string m_authToken;

		public BTS_ResetAuth(string authToken):base(PackId) {
            m_authToken = authToken;
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
            OriginalJSON.Add("auth_token", m_authToken);
            return OriginalJSON;
		}
	}
}
