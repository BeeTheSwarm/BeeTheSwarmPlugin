using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    public class CampaignRelatedNotificationModel : NotificationModel {
        public CampaignModel Campaign { get; private set;} 
        public override void ParseJSON(Dictionary<string, object> responseData) {
            base.ParseJSON(responseData);
            var extra = (Dictionary<string, object>)responseData["extra"];
            Campaign = new CampaignModel();
            Campaign.ParseJSON((Dictionary<string, object>)extra["campaign"]);

        }
    }
}
