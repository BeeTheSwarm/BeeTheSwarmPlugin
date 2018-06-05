using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_UpdateUser : BTS_BasePackage<GetUserResponce> {

		public const string PackId = "UpdateProfile";

        protected string m_name;
        protected Texture2D m_avatar;
        protected string m_oldPassword;
        protected string m_newPassword;
        protected string m_newPasswordConfirm;
        
		public BTS_UpdateUser(string name, Texture2D avatar, string oldPassword, string newPassword, string confirmNewPassword) : base (PackId) {
            m_name = name;
            m_avatar = avatar;
            if (oldPassword == null) {
                oldPassword = string.Empty;
            }
            if (newPassword == null) {
                newPassword = string.Empty;
            }
            if (confirmNewPassword == null) {
                confirmNewPassword = string.Empty;
            }
            m_newPassword = newPassword;
            m_newPasswordConfirm = confirmNewPassword;
            m_oldPassword = oldPassword;
        }

        public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("name", m_name);
            if (m_avatar != null) {
                requestFields.Add("image", "data:image/png;base64," + Convert.ToBase64String(m_avatar.EncodeToPNG()));
            }
            requestFields.Add("password", m_oldPassword);
            requestFields.Add("new_password", m_newPassword);
            requestFields.Add("new_password_confirmation", m_newPasswordConfirm);
            return requestFields;
		}

		public override bool AuthenticationRequired {
			get {
				return true;
			}
		}

		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}