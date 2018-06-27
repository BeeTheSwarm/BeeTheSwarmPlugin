using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class SendEventCommand : BaseNetworkService<EventResponce>, ISendEventService {
        [Inject] private IUserProfileModel m_userModel;
        private Action<int> m_callback;
        public void Execute(string levelId, int score, Action<int> callback) {
            m_callback = callback;
            SendPackage(new BTS_Event(levelId, score));
        }

        protected override void HandleSuccessResponse(EventResponce data) {
            m_userModel.SetLevel(data.User.Level, data.User.Progress);
            m_userModel.SetBees(data.User.Bees);
            m_callback.Invoke(data.Reward);
        }
    }
}