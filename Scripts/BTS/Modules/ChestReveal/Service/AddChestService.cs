using System;

namespace BTS  {
    public class AddChestService: BaseNetworkService<ChestCountResponse>, IAddChestService {
        private Action<int> m_callback;
        public void Execute(Action<int> callback) {
            m_callback = callback;
            SendPackage(new BTS_AddChest());
        }

        protected override void HandleSuccessResponse(ChestCountResponse data) {
            m_callback.Invoke(data.ChestCount);
        }
    }
}