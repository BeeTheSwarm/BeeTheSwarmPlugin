using System; 

namespace BTS {
    public class GetChestService: BaseNetworkService<ChestCountResponse>, IGetChestService {
        private Action<int> m_callback;
        public void Execute(Action<int> callback) {
            m_callback = callback;
            SendPackage(new BTS_GetChest());
        }

        protected override void HandleSuccessResponse(ChestCountResponse data) {
            m_callback.Invoke(data.ChestCount);
        }
    }
}