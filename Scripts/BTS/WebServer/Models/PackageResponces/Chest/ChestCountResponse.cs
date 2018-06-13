using System.Collections.Generic;

namespace BTS  {
    public class ChestCountResponse : PackageResponse  {
        public int ChestCount { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            ChestCount = int.Parse(data["count"].ToString());
        }
    }
}