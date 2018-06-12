using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {

    public class CommentModel : CommentSimpleModel {
        public UserModel User { get; private set; }

        public override void ParseJSON(Dictionary<string, object> responseData) {
            base.ParseJSON(responseData);
            User = new UserModel();
            User.ParseJSON((Dictionary<string, object>)responseData["user"]);
        }

        public CommentModel() {
        }

        public CommentModel(UserModel user, string text) {
            User = user;
            Text = text;
        }
        
        public CommentModel(UserModel user, CommentSimpleModel comment) {
            User = user;
            Id = comment.Id;
            Text = comment.Text;
            CreatedAt = comment.CreatedAt;
            Reply = comment.Reply;
        }
    }

}