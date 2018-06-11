using UnityEngine;

namespace BTS {
    public interface IUpdatePostViewModel {
        Observable<Sprite> PostImage { get; }
        Observable<string> Title { get; }
        Observable<string> Description { get; }
        void SaveChanges();
        Observable<Texture2D> NewPostImage { get; }
        string NewTitle { get; set; }
        string NewDescription { get; set; }
        void UpdateImage();
    }
}