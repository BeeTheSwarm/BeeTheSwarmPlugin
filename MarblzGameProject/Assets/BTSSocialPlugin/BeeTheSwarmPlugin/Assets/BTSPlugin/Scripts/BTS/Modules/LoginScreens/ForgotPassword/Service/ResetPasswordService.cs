using System;

namespace BTS {
    internal class ResetPasswordService: BaseNetworkService<NoDataResponse>, IResetPasswordService {
        private Action<bool> m_callback;
        public void Execute(string login, Action<bool> callback) {
            m_callback = callback;
            SendPackage(new BTS_ResetPassword(login));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(false);
        }

        protected override void HandleSuccessResponse(NoDataResponse data) {
            m_callback.Invoke(true);
        }
    }
}