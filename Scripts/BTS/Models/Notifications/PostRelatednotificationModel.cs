using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    public class PostRelatednotificationModel : NotificationModel {
        
        public PostSimpleModel Post { get; private set;} 
        public CommentSimpleModel Comment { get; private set; }
        public override void ParseJSON(Dictionary<string, object> responseData) {
            base.ParseJSON(responseData);
            var extra = (Dictionary<string, object>)responseData["extra"];
            Post = new PostSimpleModel(); 
            Post.ParseJSON((Dictionary<string, object>)extra["post"]);
            Comment = new CommentSimpleModel();
            Comment.ParseJSON((Dictionary<string, object>)extra["comment"]);
        }
    }
}
