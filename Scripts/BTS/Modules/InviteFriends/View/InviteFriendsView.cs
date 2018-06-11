using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    public class InviteFriendsView : TopPanelScreen<IInviteFriendsViewListener>, IInviteFriendsView, ITopPanelContainer {

        [SerializeField] private Text m_referalCode;
        [SerializeField] private Text m_beesCount;
        public void SetViewModel(InviteFriendViewModel m_viewModel) {

            m_viewModel.ReferalCode.Subscribe(SetReferalCode);
            m_viewModel.RewardForInvite.Subscribe(SetRewardCount);
        }

        private void SetRewardCount(int reward) {
            m_beesCount.text = string.Format("{0} BEES", reward);
        }

        private void SetReferalCode(string code) {
            m_referalCode.text = string.Format("* {0} *", code);
        }

        public void OnInviteByMail() {
            m_controller.InviteFriendsByMail();
        }

        public void OnCopyCode() {
            m_controller.CopyCode();
        }

        public void OnInviteByPhone() {
            m_controller.InviteFriendsByPhone();
        }
    }
}