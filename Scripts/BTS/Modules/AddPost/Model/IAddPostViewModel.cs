using System;
using UnityEngine;

namespace BTS {
    public interface IAddPostViewModel {
        string PostTitle { get; set; }
        string PostDescription { get; set; }
        Observable<Texture2D> PostImage { get; }
        void OnSelectImagePressed();
        void CreatePost();
    }
}