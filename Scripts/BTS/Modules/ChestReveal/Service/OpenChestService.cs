using System;
using System.Collections.Generic;

namespace BTS  {
    public class OpenChestService: BaseNetworkService<OpenChestResponse>, IOpenChestService {
        [Inject] private IUserProfileModel m_userProfileModel;
        private Action<List<ChestReward>, int> m_callback;
        public void Execute(Action<List<ChestReward>, int> callback) {
            m_callback = callback;
            SendPackage(new BTS_OpenChest());
        }

        protected override void HandleSuccessResponse(OpenChestResponse data) {
            m_callback.Invoke(data.Rewards, data.RewardId);
            m_userProfileModel.SetBees(data.User.Bees);
            m_userProfileModel.User.UpdateInfo(data.User);
        }
    }
}