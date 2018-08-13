using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    internal class HiveLeaderboardController : TopPanelScreenController<IHiveLeaderboardView>, IHiveLeaderboardViewListener, IHiveLeaderboardController {
        [Inject]
        private ISearchMissingHivePlayersController m_missingHivePlayerScreen;
        [Inject]
        private IGetHiveService m_getHiveService;
        private int m_hiveId;
        private bool m_waitUpdate = false;
        private int m_hiveMembers = -1;
        protected override bool BackButtonEnabled {
            get {
                return true;
            }
        }

        private HiveLeaderboardViewModel m_viewModel;

        public HiveLeaderboardController() {
            m_viewModel = new HiveLeaderboardViewModel();
        }
        
        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(m_viewModel);
        }

        public void Show(int hiveId) {
            base.Show();
            m_hiveId = hiveId;
            m_getHiveService.Execute(hiveId, OnHiveReceived);
        }

        private void OnHiveReceived(List<UserModel> list, int members, UserModel parent, float totalImpact) {
            m_hiveMembers = members;
            list.ForEach(item => {
                m_viewModel.MembersList.Add(new HiveLeaderboaderItemViewModel(item.Name, item.Impact));
            });
            m_waitUpdate = false;
        }

        public void OnMissingHivePlayerClick() {
            m_missingHivePlayerScreen.Show();
        }

        public void ScrolledToEnd() {
            if (m_waitUpdate ) {
                return;
            }
            if (m_viewModel.MembersList.Count() == m_hiveMembers) {
                return;
            }
            m_waitUpdate = true;
            m_getHiveService.Execute(m_hiveId, OnHiveReceived, m_viewModel.MembersList.Count());
        }
    }
}