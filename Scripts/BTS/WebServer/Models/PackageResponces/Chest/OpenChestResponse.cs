using System.Collections.Generic;

namespace BTS  {
    public class OpenChestResponse : PackageResponse  {
        public int RewardId { get; private set; }
        public List<ChestReward> Rewards { get; private set; }
        public UserModel User { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            RewardId = int.Parse(data["win"].ToString());
            Rewards = new List<ChestReward>();
            var rewardsSource = (List<object>)data["rewards"];
            foreach (object obj in rewardsSource) {
                ChestReward chestReward = new ChestReward();
                chestReward.ParseJSON((Dictionary<string, object>)obj);
                Rewards.Add(chestReward);
            }
            User = new UserModel();
            User.ParseJSON((Dictionary<string, object>)data["user"]);
        }
    }
}