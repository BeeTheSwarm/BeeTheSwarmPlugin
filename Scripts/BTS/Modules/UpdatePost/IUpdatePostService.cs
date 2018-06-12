using UnityEngine;

namespace BTS {
    public interface IUpdatePostService: IService {
        void Execute(int postId, string title, string description, Texture2D image);
    }
}