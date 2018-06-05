using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class InviteFriendsEmailCommand : BaseNetworkService<GetUserResponce>, IInviteFriendsEmailService {
        [Inject] private IUserProfileModel m_userModel;


        private const string MESSAGE_TITLE_TEMPLATE = "Help me Fundraise the Fun Way!";

        private const string MESSAGE_TEMPLATE =
            "Help me Fundraise the Fun Way! By Playing Free Video Games! Download Bee The Swarm using this link beetheswarm.app.link and when you signup use my code {0}";

        public void Execute(List<string> mails) {
            Platform.Adapter.SendEmail(MESSAGE_TITLE_TEMPLATE,
                string.Format(MESSAGE_TEMPLATE, m_userModel.User.HiveCode), mails,
                success => {
                    if (success)
                        SendPackage(new BTS_InvitePlayers(2, mails));
                });
        }

        protected override void HandleSuccessResponse(GetUserResponce data) {
            m_userModel.SetBees(data.User.Bees);
        }
    }
}