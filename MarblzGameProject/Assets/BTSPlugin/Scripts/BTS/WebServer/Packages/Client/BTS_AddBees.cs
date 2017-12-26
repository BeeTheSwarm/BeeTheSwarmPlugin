using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

internal class BTS_AddBees : BTS_BasePackage {

	public const string PackId = "AddBees";
	public int count = 0;
	public int transactionType = 1;

	public BTS_AddBees(int bees) : base (PackId) {
		count = bees;
	}

	public override Dictionary<string, object> GenerateData () {
		Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

		OriginalJSON.Add("count", count);
		OriginalJSON.Add("type", transactionType);

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
