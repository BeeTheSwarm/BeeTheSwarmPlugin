using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS {
    public class UserModel : DataModel {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public ImageUrlsModel Avatar { get; private set; }
        public int Bees { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public int Progress { get; set; }
        public float Impact { get; set; }
        public string HiveCode { get; private set; }
        public string HiveParent { get; set; }
        public float HiveImpact { get; private set; }
        public int HiveId { get; private set; }

        public event Action OnUserNameChanged = delegate {  };
        public event Action OnAvatarChanged = delegate {  };
        public event Action OnHiveIdChanged = delegate {  };
        public event Action OnLevelChanged = delegate {  };
        
        public override void ParseJSON(Dictionary<string, object> responseData) {
            Id = int.Parse(responseData["id"].ToString());
            Name = responseData["name"].ToString();
            Email = responseData["email"].ToString();
            Avatar = new ImageUrlsModel();
            Avatar.ImageUrl = responseData["image"].ToString();
            Avatar.ThumbnailUrl = responseData["image_thumbnail"].ToString();
            Bees = int.Parse(responseData["bees"].ToString());
            Points = int.Parse(responseData["points"].ToString());
            Level = int.Parse(responseData["level"].ToString());
            Progress = int.Parse(responseData["progress"].ToString());
            Impact = float.Parse(responseData["impact"].ToString());
            if (responseData["hive_code"] != null) {
                HiveCode = responseData["hive_code"].ToString();
            } else {
                HiveCode = string.Empty;
            }
            if (responseData["hive_parent"] != null) {
                HiveParent = responseData["hive_parent"].ToString();
            }
            HiveId = int.Parse(responseData["hive_id"].ToString());
            HiveImpact = float.Parse(responseData["hive_impact"].ToString());
        }

        internal void UpdateInfo(UserModel user) {
            if (!Name.Equals(user.Name)) {
                Name = user.Name;
                OnUserNameChanged.Invoke();
            }
            if (!Avatar.ImageUrl.Equals(user.Avatar.ImageUrl)) {
                Avatar = user.Avatar;
                OnAvatarChanged.Invoke();
            }
            if (HiveId != user.HiveId) {
                HiveId = user.HiveId;
                HiveParent = user.HiveParent;
                OnHiveIdChanged.Invoke();
            }     
            
        }
    }
}