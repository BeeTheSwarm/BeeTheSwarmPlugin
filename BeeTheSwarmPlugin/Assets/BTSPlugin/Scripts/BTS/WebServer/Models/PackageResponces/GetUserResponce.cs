using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BTS {
    public class GetUserResponce : PackageResponse {
        public UserModel User { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            User = new UserModel();
            User.ParseJSON((Dictionary<string, object>)data["user"]);
        }
    }
}