using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetEvents : BTS_BasePackage<GetEventsResponse> {

		public const string PackId = "GetEvents";

		public BTS_GetEvents() : base (PackId) {
		}
		
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}