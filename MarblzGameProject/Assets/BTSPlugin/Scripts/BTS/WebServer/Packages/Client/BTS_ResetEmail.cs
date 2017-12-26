using System;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
	
	internal class BTS_ResetEmail : BTS_BasePackage {

		public const string PackId = "ResetEmail";
		public UInt64 phone;

		public BTS_ResetEmail(UInt64 phoneN) : base (PackId) {
			phone = phoneN;
		}

		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

			OriginalJSON.Add("phone", phone);

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
