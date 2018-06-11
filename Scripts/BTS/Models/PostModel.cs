using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BTS {

    public class PostModel : PostSimpleModel {
        public CampaignModel Campaign { get; set; }
        public List<CommentModel> Comments { get; private set; }
        public int CommentsCount { get; private set; }
        public Action<CommentModel> OnCommentAdded = delegate { };
        public Action<CommentModel> OnCommentInserted = delegate { };
        public event Action OnUpdate = delegate { };
        public override void ParseJSON(Dictionary<string, object> responseData) {
            base.ParseJSON(responseData);
            Campaign = new CampaignModel();
            Campaign.ParseJSON((Dictionary<string, object>)responseData["campaign"]);
            Comments = new List<CommentModel>();
            List<object> commentsSource = (List<object>)responseData["comments"];
            foreach (var commentSource in commentsSource) {
                CommentModel comment = new CommentModel();
                comment.ParseJSON((Dictionary<string, object>)commentSource);
                Comments.Add(comment);
            }
            CommentsCount = int.Parse(responseData["comments_count"].ToString());
        }

        internal void AddNewComment(CommentModel comment) {
            Comments.Insert(0, comment);
            OnCommentInserted.Invoke(comment);
        }

        internal void AddComments(List<CommentModel> comments) {
            comments.ForEach(comment => {
                Comments.Add(comment);
                OnCommentAdded.Invoke(comment);
            });
        }

        internal void UpdateCommentsCount(int commentsCount) {
            CommentsCount = commentsCount;
        }

        public void Update(PostModel source) {
            Title = source.Title;
            Description = source.Description;
            Image = source.Image;
            OnUpdate.Invoke();
        }

    }

}