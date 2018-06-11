using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

internal class BTS_AuthCode : BTS_BasePackage<AuthCodeResponce> {

	public const string PackId = "AuthCode";
	public int smsCode = 0000;

	public BTS_AuthCode(int code) : base (PackId) {
		smsCode = code;
	}

	public override Dictionary<string, object> GenerateData () {
		Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();
		OriginalJSON.Add("code", smsCode);
		return OriginalJSON;
	}

	public override Int32 Timeout {
		get {
			return 5;
		}
	}
}
}
