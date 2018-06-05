using UnityEngine;
using System.Collections;
using System;
namespace BTS {
    public class FeedListItemModel {
        public PostModel Model { get; set; }
        public Func<ImagePreloader> CommentLoaderFabric { get; set; }
        public Action<PostModel> OnEditClicked { get; set; }
        public Action<PostModel> OnFavoriteClicked { get; set; }
        public Action<PostModel, string> OnEditTitleClicked { get; set; }
        public Action<PostModel> OnCommentClicked { get; set; }
        public CategoryModel Category { get; set; }
        public int Raised { get; internal set; }
        public int CurrentTarget { get; internal set; }
    }
}