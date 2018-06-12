using System.Collections.Generic;

namespace BTS {
    public class GetPostResponse : PackageResponse {
        public PostModel Post { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Post = new PostModel();
            Post.ParseJSON((Dictionary<string, object>) data["post"]);
        }
    }
}