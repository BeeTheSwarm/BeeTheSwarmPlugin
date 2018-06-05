using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_ResendCode : BTS_BasePackage<AuthTokenResponce> {

		public const string PackId = "ResendCode";
		public string m_login;

		public BTS_ResendCode(string login) : base (PackId) {
			m_login = login;
		}

		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();
            OriginalJSON.Add("login", m_login);
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
