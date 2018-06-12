using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BTS {
    public class NotificationModel : DataModel {
        public int Id { get; private set; }
        public NotificationAction Action { get; private set; }
        public UserModel User { get; private set; }
        public int Entity { get; private set; }
        public int Value { get; set; }
        public int CreatedAt { get; set; }
        protected object Extra { get; private set;}
        public override void ParseJSON(Dictionary<string, object> responseData) {
            Id = int.Parse(responseData["id"].ToString());
            Action = (NotificationAction)int.Parse(responseData["action"].ToString());
            User = new UserModel();
            if (responseData["user"] != null) {
                User.ParseJSON((Dictionary<string, object>)responseData["user"]);
            }
            Entity = int.Parse(responseData["entity_id"].ToString());
            CreatedAt = int.Parse(responseData["created_ts"].ToString());
            Value = int.Parse(responseData["value"].ToString());
        }
    }
}
