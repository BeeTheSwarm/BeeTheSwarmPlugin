using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class CampaignLevelModel : DataModel {
        public int Level { get; private set; }
        public int Amount { get; private set; }

        public override void ParseJSON(Dictionary<string, object> responseData) {
            Level = int.Parse(responseData["level"].ToString());
            Amount = int.Parse(responseData["amount"].ToString());
        }
    }
}