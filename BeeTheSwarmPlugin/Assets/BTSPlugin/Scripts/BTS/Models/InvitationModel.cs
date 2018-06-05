using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {

    public class InvitationModel : DataModel {
        public int Id { get; private set; }
        public InvitationType Type { get; private set; }
        public UserModel User { get; private set; }
        public int CreatedAt { get; private set; }

        public override void ParseJSON(Dictionary<string, object> responseData) {
            Id = int.Parse(responseData["id"].ToString());
            Type = (InvitationType)int.Parse(responseData["type"].ToString());
            CreatedAt = int.Parse(responseData["created_ts"].ToString());
            User = new UserModel();
            User.ParseJSON((Dictionary<string, object>)responseData["user"]);
        }
    }

}