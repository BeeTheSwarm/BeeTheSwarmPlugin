using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {
	
	internal class BTS_Event : BTS_BasePackage {
	
		public const string PackId = "Event";
		public int score = 0;
		public string events = "";

		public BTS_Event(string level, int scoreCount) : base (PackId) {
			events = level;
			score = scoreCount;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();

			OriginalJSON.Add("score", score);
			OriginalJSON.Add("event", events);

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