using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {

    internal class MissedHivePlayersController : BaseScreenController<IMissedHivePlayersView>, IMissedHivePlayersViewListener, IMissedHivePlayersController {
        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IHiveLeaderboardController m_hiveLeaderboard;
        [Inject]
        private IPopupsModel m_popupsModel;

        private MissedHivePlayersViewModel m_viewModel = new MissedHivePlayersViewModel();
        public MissedHivePlayersController() {
        }

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(m_viewModel);
        }

        public void Show(int userId) {

        }

        public void AddToHiveClicked() {

        }

        public void ViewAllClicked() {
            m_hiveLeaderboard.Show();
        }

        public void RefferedByFriendClicked() {

        }
    }
}
