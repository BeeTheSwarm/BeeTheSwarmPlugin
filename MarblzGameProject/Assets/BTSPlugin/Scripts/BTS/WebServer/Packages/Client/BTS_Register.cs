using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

internal class BTS_Register : BTS_BasePackage {

	public const string PackId = "Register";
	public string name = String.Empty;
	public string email = String.Empty;
	public string password = String.Empty;
	public UInt64 timeZone_offset = 0;
	public string ref_token;

		public BTS_Register(string name, string email, string password, UInt64 timeZone_offset, string ref_token) : base (PackId) {
		this.name = name;
		this.email = email;
		this.password = password;
		this.timeZone_offset = timeZone_offset;
		this.ref_token = ref_token;
	}

	public override Dictionary<string, object> GenerateData () {
		Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

		OriginalJSON.Add("name", name);
		OriginalJSON.Add("email", email);
		OriginalJSON.Add("password", password);
		OriginalJSON.Add("ref", ref_token);
		OriginalJSON.Add ("timezone_offset", timeZone_offset);

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