using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

internal class BTS_AuthCode : BTS_BasePackage {

	public const string PackId = "AuthCode";
	public int smsCode = 0000;
	public int userID = 0;

	public BTS_AuthCode(int user_id, int code) : base (PackId) {
		smsCode = code;
		userID = user_id;
	}

	public override Dictionary<string, object> GenerateData () {
		Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

		OriginalJSON.Add("user_id", userID);
		OriginalJSON.Add("code", smsCode);

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
