using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class SendEventCommand : BaseNetworkService<EventResponce>, ISendEventService {
        [Inject] private IUserProfileModel m_userModel;

        public void Execute(string levelId, int score) {
            m_networkService.SendPackage(new BTS_Event(levelId, score));
        }

        protected override void HandleSuccessResponse(EventResponce data) {
            m_userModel.SetBees(data.User.Bees);
        }
    }
}