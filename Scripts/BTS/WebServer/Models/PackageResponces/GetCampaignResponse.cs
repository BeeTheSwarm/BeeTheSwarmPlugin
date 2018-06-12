using System.Collections.Generic;

namespace BTS {
    public class GetCampaignResponse : PackageResponse {
        public CampaignModel Campaign { get; private set; }

        public override void Parse(Dictionary<string, object> data) {
            Campaign = new CampaignModel();
            Campaign.ParseJSON((Dictionary<string, object>) data["campaign"]);
        }
    }
}