using System;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    public class SearchHiveResponse : PackageResponse {
        public List<UserModel> Users { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Users = new List<UserModel>();
            var hiveSource = (List<object>)data["users"];
            foreach (var hiveItem in hiveSource) {
                    var user = new UserModel();
                    user.ParseJSON((Dictionary<string, object>)hiveItem);
                    Users.Add(user);
            }
        }
    }
}
