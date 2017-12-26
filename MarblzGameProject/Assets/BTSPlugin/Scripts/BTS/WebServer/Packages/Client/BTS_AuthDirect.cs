using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_AuthDirect : BTS_BasePackage {

		public const string PackId = "AuthDirect";
		public string login = String.Empty;
		public string password = String.Empty;

		public BTS_AuthDirect(string login, string password):base(PackId) {
			this.login = login;
			this.password = password;
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

			OriginalJSON.Add("login", login);
			OriginalJSON.Add("password", password);

			return OriginalJSON;
		}
	}
}

