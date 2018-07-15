using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class UpdateUserCommand : BaseNetworkService<GetUserResponce>, IUpdateUserService {
        [Inject]
        private IUserProfileModel m_userModel;
        private Action<string> m_callback;
        private string m_email;
        [Inject]
        private IPopupsModel m_popupsModel;

        public UpdateUserCommand() {

        }

        public void Execute(string name, Texture2D avatar, string password, string newPassword, string confirmNewPassword, Action<string> callback) {
            m_callback = callback;
            SendPackage(new BTS_UpdateUser(name, avatar, password, newPassword, confirmNewPassword));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(error.Description);
        }

        protected override void HandleSuccessResponse(GetUserResponce data) {
            m_userModel.User.UpdateInfo(data.User);
            m_userModel.SetLevel(data.User.Level, data.User.Progress);
            m_userModel.SetBees(data.User.Bees);
            m_callback.Invoke(string.Empty);
        }
    }
}
