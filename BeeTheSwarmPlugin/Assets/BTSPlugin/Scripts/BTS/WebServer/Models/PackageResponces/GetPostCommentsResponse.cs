using System.Collections.Generic;
namespace BTS {
    public class GetPostCommentsResponse : PackageResponse {
        public List<CommentModel> Comments { get; private set; }
        public int CommentsCount { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Comments = new List<CommentModel>();
            List<object> commentsSource = (List<object>)data["comments"];
            foreach (var commentSource in commentsSource) {
                CommentModel comment = new CommentModel();
                comment.ParseJSON((Dictionary<string, object>)commentSource);
                Comments.Add(comment);
            }
            CommentsCount = int.Parse(data["comments_count"].ToString());
        }
    }
}