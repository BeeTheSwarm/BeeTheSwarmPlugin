using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetTutorialState : BTS_BasePackage<GetTutorialStateResponce> {
		private const string PACK_ID = "GetTutorialGameState";

		public BTS_GetTutorialState() : base (PACK_ID) {
		}
		
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}