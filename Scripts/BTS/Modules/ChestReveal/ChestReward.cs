using System.Collections.Generic;

namespace BTS  {
    public class ChestReward {
        public int Count { get; private set; }
        public void ParseJSON(Dictionary<string, object> data) {
            Count = int.Parse(data["count"].ToString());
        }
    }
}