using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BTS {
    public class FeedCampaignResponce : PackageResponse {
        public UserModel User { get; private set; }
        public PostModel Post { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            User = new UserModel();
            User.ParseJSON((Dictionary<string, object>)data["user"]);
            Post = new PostModel();
            Post.ParseJSON((Dictionary<string, object>)data["post"]);

        }
    }
}