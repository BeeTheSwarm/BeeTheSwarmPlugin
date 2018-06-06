using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    public class GetNewCountersResponce : PackageResponse
    {
        public int NotificationsCount { get; private set; }
        public int RequestsCount { get; private set; }
        public override void Parse(Dictionary<string, object> data)
        {
            NotificationsCount = int.Parse(data["notifications_count"].ToString());
            RequestsCount = int.Parse(data["invitations_count"].ToString());
        }
    }
}
