using UnityEngine;

namespace BTS {
    public interface IUpdateCampaignViewModel {
        Observable<string> CampaignTitle { get; }
        Observable<string> Website { get; }
        Observable<string> CategoryName { get; }
        Observable<Sprite> CategoryImage { get; }
        int NewCategory { get; set; }
        string NewTitle { get; set; }
        string NewWebsite { get; set; }
        void SaveChanges();
        void SelectCategory();
    }
}