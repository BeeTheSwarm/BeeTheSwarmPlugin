using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class InviteFriendsPhoneCommand : BaseNetworkService<GetUserResponce>, IInviteFriendsPhoneService {

        [Inject] private IUserProfileModel m_userModel;
        private const string MESSAGE_TEMPLATE = "Help me Fundraise the Fun Way! By Playing Free Video Games! Download Bee The Swarm using this link beetheswarm.app.link and when you signup use my code {0}";

        public void Execute(List<string> phones) {
            Platform.Adapter.SendMessages(string.Format(MESSAGE_TEMPLATE, m_userModel.User.HiveCode), phones,
                success => {
                    if (success)
                        SendPackage(new BTS_InvitePlayers(1, phones));
                });
        }

        protected override void HandleSuccessResponse(GetUserResponce data) {
            m_userModel.SetBees(data.User.Bees);
        }
    }
}
