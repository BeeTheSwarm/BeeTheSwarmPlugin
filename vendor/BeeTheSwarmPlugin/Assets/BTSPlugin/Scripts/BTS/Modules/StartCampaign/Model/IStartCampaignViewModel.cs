using System;
using UnityEngine;

namespace BTS {
    public interface IStartCampaignViewModel {
        string CampaignTitle { get; set; }
        string Website { get; set; }
        string PostTitle { get; set; }
        string PostDescription { get; set; }
        Observable<Texture2D> PostImage { get; }
        Observable<string> CategoryName { get; }
        Observable<Sprite> CategoryImage { get; }
        void OnSelectImagePressed();
        void CreateCampaign();
        void CreatePost();
    }
}