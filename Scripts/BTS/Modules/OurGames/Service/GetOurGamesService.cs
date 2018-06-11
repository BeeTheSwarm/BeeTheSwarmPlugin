using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetOurGamesService: BaseNetworkService<GetGamesResponse>, IGetOurGamesService {

        private Action<List<GameModel>> m_callback;
        protected override void HandleSuccessResponse(GetGamesResponse data) {
            m_callback.Invoke(data.Games);
        }

        public void Execute(Action<List<GameModel>> callback) {
            m_callback = callback;
            SendPackage(new BTS_GetGamesList());
        }
    }
}