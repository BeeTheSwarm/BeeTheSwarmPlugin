using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetNewCounters : BTS_BasePackage<GetNewCountersResponce> {
		private const string PACK_ID = "GetNewCount";
		public BTS_GetNewCounters() : base (PACK_ID) {

		}
		
		public override Int32 Timeout {
			get {
				return 5;
			}
		}

    }
}