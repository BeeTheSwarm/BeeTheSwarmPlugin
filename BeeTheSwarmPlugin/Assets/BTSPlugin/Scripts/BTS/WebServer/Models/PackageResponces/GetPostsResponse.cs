using System.Collections.Generic;
namespace BTS {
    public class GetPostsResponse : PackageResponse {
        public List<PostModel> Posts { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Posts = new List<PostModel>();
            var campaignsSource = (List<object>)data["posts"];
            foreach (object obj in campaignsSource) {
                PostModel post = new PostModel();
                post.ParseJSON((Dictionary<string, object>)obj);
                Posts.Add(post);
            }
        }
    }
}