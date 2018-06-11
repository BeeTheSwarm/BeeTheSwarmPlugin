using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_Login : BTS_BasePackage<LoginResponce> {

		public const string PackId = "Login";
		private string m_login;
        private string m_password;

		public BTS_Login(string login, string password):base(PackId) {
			m_login = login;
			m_password = password;
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

		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();
            OriginalJSON.Add("login", m_login);
			OriginalJSON.Add("password", m_password);
            return OriginalJSON;
		}
	}
}

