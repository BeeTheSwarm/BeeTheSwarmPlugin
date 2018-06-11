using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

internal class BTS_ConfirmResetPassword : BTS_BasePackage<AuthTokenResponce> {

	private const string PackId = "ConfirmResetPassword";
	private string m_email;
	private string m_password;
	private string m_confirmPassword;
	private int m_code;

		public BTS_ConfirmResetPassword(string email, int code, string password, string confirmPassword) : base (PackId) {
		m_email = email;
		m_password = password;
		m_confirmPassword = confirmPassword;
		m_code = code;
	}

	public override Dictionary<string, object> GenerateData () {
		Dictionary<string, object> fieldsJSON =  new Dictionary<string, object>();
		fieldsJSON.Add("login", m_email);
		fieldsJSON.Add("code", m_code);
		fieldsJSON.Add("password", m_password);
		fieldsJSON.Add("password_confirmation", m_confirmPassword);
		return fieldsJSON;
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