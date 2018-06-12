using UnityEngine;
using System.Collections;
using System;

namespace BTS {
    public class PostViewModel {
        public int PostId { get; private set; }
        public int CampaignId { get; private set; }
        public Action<int, Vector3> EditCallback;

        public event Action<int, int, PostViewModel> OnFeed = delegate { };
        public Action<int, string> OnCommentCallback;
        public event Action<int, bool> OnFavoriteClickCallback = delegate { };
        public Action<int, int> OnLoadMoreCommentsCallback;
        public Action<int> OnAvatarClickCallback;
        public event Action OnStartDonate = delegate { };
        public Action<int> OnPostClickCallback;
        public event Action<int> OnAddPostClicked = delegate { };

        public readonly Observable<bool> Editable = new Observable<bool>();

        public readonly Observable<float> Raised = new Observable<float>();
        public readonly Observable<int> CurrentTarget = new Observable<int>();
        public readonly Observable<Sprite> UserAvatar = new Observable<Sprite>();
        public readonly Observable<int> Date = new Observable<int>();
        public readonly Observable<int> MoneyEarned = new Observable<int>();
        public readonly Observable<string> CampaignTitle = new Observable<string>();
        public readonly Observable<string> PostDescription = new Observable<string>();
        public readonly Observable<string> PostTitle = new Observable<string>();
        public readonly Observable<Sprite> PostImage = new Observable<Sprite>();
        public readonly Observable<int> Progress = new Observable<int>();
        public readonly Observable<Sprite> CategoryImage = new Observable<Sprite>();
        public readonly Observable<int> CommentsCount = new Observable<int>();
        public readonly Observable<bool> IsFavorite = new Observable<bool>();
        public readonly ObservableList<CommentViewModel> Comments = new ObservableList<CommentViewModel>();
        public string CommentText = string.Empty;

        public PostViewModel(int postId, int campaingId) {
            PostId = postId;
            CampaignId = campaingId;
        }

        public void AddPost() {
            OnAddPostClicked.Invoke(CampaignId);
        }

        public void Donate(int count) {
            OnFeed.Invoke(PostId, count, this);
        }

        public void ToggleFavorite() {
            OnFavoriteClickCallback.Invoke(PostId, !IsFavorite.Get());
        }

        public void IsSwarming() {
            OnStartDonate.Invoke();
        }
    }
}