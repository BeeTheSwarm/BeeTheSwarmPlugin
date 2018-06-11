using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS
{

    public class GetUserCampaignsResponce : PackageResponse
    {
        public List<PostModel> Posts { get; private set; }

        public override void Parse(Dictionary<string, object> data)
        {
            Posts = new List<PostModel>();
            List<object> postsSource = (List<object>)data["posts"];
            foreach (var postSource in postsSource)
            {
                PostModel post = new PostModel();
                post.ParseJSON((Dictionary<string, object>)postSource);
                Posts.Add(post);
            }
        }
    }
}
