using System;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
	
	internal class BTS_ResetPassword : BTS_BasePackage {

		public const string PackId = "ResetPassword";
		public string login = "";
		
		public BTS_ResetPassword(string login) : base (PackId) {
			this.login = login;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

			OriginalJSON.Add("login", login);

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
