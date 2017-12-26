using System;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
	
	internal class BTS_ReassignEmail : BTS_BasePackage {

		public const string PackId = "ReassignEmail";
		public int code = 0;
		public string email = "";

		public BTS_ReassignEmail(int code, string email) : base (PackId) {
			this.code = code;
			this.email = email;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

			OriginalJSON.Add("code", code);
			OriginalJSON.Add("email", email);

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