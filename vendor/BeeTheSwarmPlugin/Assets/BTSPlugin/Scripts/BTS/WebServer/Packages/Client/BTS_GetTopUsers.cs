using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetTopUsers : BTS_BasePackage<MockPackResponce> {

		public const string PackId = "GetTopUsers";
        private LeaderboardType m_type;
		public BTS_GetTopUsers(LeaderboardType type) : base (PackId) {
            m_type = type;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("type", (int) m_type);
			return requestFields;
		}
        
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}