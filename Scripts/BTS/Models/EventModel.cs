using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    public class EventModel : DataModel {
        public string Title { get; private set; }
        public List<RewardModel> Rewards { get; private set; }
        public int Refresh { get; private set; }

        public override void ParseJSON(Dictionary<string, object> responseData) {
            Title = responseData["title"].ToString();
            Rewards = new List<RewardModel>();
            List<object> rewardsSource = (List<object>)responseData["rewards"];
            rewardsSource.ForEach(r => {
                var reward = new RewardModel();
                reward.ParseJSON((Dictionary<string, object>)r);
                Rewards.Add(reward);
            });
            Refresh = int.Parse(responseData["refresh"].ToString());
        }
    }
}