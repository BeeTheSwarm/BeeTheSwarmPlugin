using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_FinishTutorial : BTS_BasePackage<GetUserResponce> {
		private const string PACK_ID = "FinishTutorialGame";

		private int m_score;
		public BTS_FinishTutorial(int score) : base (PACK_ID) {
			m_score = score;
		}

		public override Dictionary<string, object> GenerateData() {
			var fields = base.GenerateData();
			fields.Add("count", m_score);
			return fields;
		}

		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}