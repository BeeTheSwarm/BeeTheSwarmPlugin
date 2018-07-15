using System.Collections.Generic;

namespace BTS {
    public class GetHiveResponse : PackageResponse {
        public List<UserModel> Users { get; private set; }
        public int MembersCount { get; private set; }
        public UserModel Parent { get; private set; }
        public int TotalImpact { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Users = new List<UserModel>();
            var hiveSource = (List<object>)data["hive"];
            foreach (var hiveItem in hiveSource) {
                    UserModel user = new UserModel();
                    user.ParseJSON((Dictionary<string, object>)hiveItem);
                    Users.Add(user);
            }
            if (data.ContainsKey("hive_parent") && data["hive_parent"]!=null) {
                Parent = new UserModel();
                Parent.ParseJSON((Dictionary<string, object>)data["hive_parent"]);
            }
            MembersCount = int.Parse(data["hive_count"].ToString());
            TotalImpact = int.Parse(data["hive_total"].ToString());
        }
    }

}
