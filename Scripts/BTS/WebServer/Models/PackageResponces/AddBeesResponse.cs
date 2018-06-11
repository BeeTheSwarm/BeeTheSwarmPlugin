using System.Collections.Generic;
namespace BTS {
    public class AddBeesResponse : PackageResponse {
        public UserModel User { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            User = new UserModel();
            Dictionary<string, object> userSource = (Dictionary<string, object>)data["user"];
            User.ParseJSON(userSource);
        }
    }
}