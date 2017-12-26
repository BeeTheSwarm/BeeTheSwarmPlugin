using System;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
	
	internal class BTS_ReassignPassword : BTS_BasePackage {

		public const string PackId = "ReassignPassword";
		public int code = 0;
		public string password = "";
		
		public BTS_ReassignPassword(int code, string password) : base (PackId) {
			this.code = code;
			this.password = password;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

			OriginalJSON.Add("code", code);
			OriginalJSON.Add("password", password);

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