using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetUser : BTS_BasePackage<GetUserResponce> {

		public const string PackId = "GetUser";

		public BTS_GetUser():base(PackId) {
            
		}
        
		public override Int32 Timeout {
			get {
				return 30;
			}
		}
	}
}

