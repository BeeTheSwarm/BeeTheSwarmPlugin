using UnityEngine;
using System.Collections;
using System;
namespace BTS {
    public interface IImagesService : IService {
        void GetImage(ImageUrlsModel urls, Action<Sprite> callback);
        void GetImage(string url, Action<Sprite> callback);
        Sprite GetImage(string url);
    }
}