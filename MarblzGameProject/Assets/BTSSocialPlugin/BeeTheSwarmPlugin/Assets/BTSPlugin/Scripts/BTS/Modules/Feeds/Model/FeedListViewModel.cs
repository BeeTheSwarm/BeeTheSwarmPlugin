using UnityEngine;
using System.Collections;
namespace BTS
{
    public class FeedListViewModel
    {
        public readonly ObservableList<PostViewModel> NewPosts = new ObservableList<PostViewModel>();
        public readonly ObservableList<PostViewModel> TopPosts = new ObservableList<PostViewModel>();
        public readonly ObservableList<PostViewModel> RecentPosts = new ObservableList<PostViewModel>();
        public readonly ObservableList<PostViewModel> FavoritePosts = new ObservableList<PostViewModel>();
        public Observable<float> Impact = new Observable<float>();
    }
}