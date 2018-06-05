using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_InvitePlayers : BTS_BasePackage<GetUserResponce> {

		public const string PackId = "InviteFriends";
        private int m_type;
        private List<string> m_values;
		public BTS_InvitePlayers(int type, List<string> values) : base (PackId) {
            m_type = type;
            m_values = values;
		}
		
		public override Int32 Timeout {
			get {
				return 5;
			}
		}

        public override Dictionary<string, object> GenerateData()
        {
            var data = new Dictionary<string, object>();
            data.Add("type", m_type);
            data.Add("values", m_values);
            return data;
        }
    }
}