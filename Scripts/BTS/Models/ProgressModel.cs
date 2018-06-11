using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    public class ProgressModel : DataModel {
        public int Score { get; private set; }
        public int Reward { get; set; }
        public string Title { get; set; }

        public override void ParseJSON(Dictionary<string, object> responseData) {
            Score = int.Parse(responseData["score"].ToString());
            Reward = int.Parse(responseData["reward"].ToString());
            Title = responseData["title"].ToString();
        }
    }
}
