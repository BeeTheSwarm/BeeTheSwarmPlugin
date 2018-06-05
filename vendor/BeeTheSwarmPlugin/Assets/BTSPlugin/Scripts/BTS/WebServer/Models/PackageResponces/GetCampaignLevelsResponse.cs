using System.Collections.Generic;
namespace BTS {
    public class GetCampaignLevelsResponce : PackageResponse {
        public List<CampaignLevelModel> Levels { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Levels = new List<CampaignLevelModel>();
            List<object> commentsSource = (List<object>)data["levels"];
            foreach (var commentSource in commentsSource) {
                CampaignLevelModel level = new CampaignLevelModel();
                level.ParseJSON((Dictionary<string, object>)commentSource);
                Levels.Add(level);
            }
        }
    }
}