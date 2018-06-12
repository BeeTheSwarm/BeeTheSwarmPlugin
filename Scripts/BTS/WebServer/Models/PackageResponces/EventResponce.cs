using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BTS {
    public class EventResponce : PackageResponse {
        public UserModel User { get; private set; }
        public float Reward { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            User = new UserModel();
            User.ParseJSON((Dictionary<string, object>)data["user"]);
            Reward = int.Parse(data["reward"].ToString());
        }
    }
}