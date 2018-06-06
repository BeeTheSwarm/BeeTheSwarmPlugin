using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BTS {
    public class CommentSimpleModel : DataModel {
        public int Id { get; protected set; }
        public string Text { get; protected set; }
        public int CreatedAt { get; protected set; }
        public int Reply { get; protected set; }

        public override void ParseJSON(Dictionary<string, object> responseData) {
            Id = int.Parse(responseData["id"].ToString());
            Text = responseData["text"].ToString();
            CreatedAt = int.Parse(responseData["created_ts"].ToString());
            Reply = int.Parse(responseData["reply"].ToString());
        }

    }
}