using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BTS {
    public class LoginResponce : PackageResponse {
        public string AuthToken { get; private set; }
        public UserModel User { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            User = new UserModel();
            User.ParseJSON((Dictionary<string, object>)data["user"]);
            AuthToken = data["auth_token"].ToString();
        }
    }
}