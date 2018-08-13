using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {

    internal class HiveController : TopPanelScreenController<IHiveView>, IHiveViewListener, IHiveController {
        [Inject] private IHiveLeaderboardController m_hiveLeaderboard;
        [Inject] private ISearchMissingHivePlayersController m_searchHivePlayers;
        [Inject] private IGetHiveService m_getHiveService;
        [Inject] private ISearchReffererController m_searchRefferer;
        [Inject] private IAddFriendCodePopupController m_addFriendCode;
        [Inject] private IPopupsModel m_popupsModel;
        [Inject] private IUserProfileModel m_userProfile;
        [Inject] private IHiveModel m_hiveModel;
        [Inject] private IImagesService m_imagesService;
        [Inject] private ILoaderController m_loader;

        private const int USERS_ON_SCREEN = 10;
        private HiveViewModel m_viewModel = new HiveViewModel();
        public HiveController() {
        }
        protected override bool BackButtonEnabled {
            get {
                return true;
            }
        }
        
        private void HiveUpdatedHandler() {
            m_viewModel.Reset();
            LoadHive();
        }

        public override void Hide() {
            base.Hide();
            m_hiveModel.OnHiveUpdated -= HiveUpdatedHandler;
            m_viewModel.Reset();
        }

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(m_viewModel);
        }

        public void Show(int hiveId) {
            base.Show();
            m_hiveModel.OnHiveUpdated += HiveUpdatedHandler;
            m_viewModel.HiveId = hiveId;
            LoadHive();
        }
        
        public override void Show() {
            Show(m_userProfile.User.HiveId);
        }

        private void LoadHive() {
            m_viewModel.HasRefferer.Set(m_userProfile.User.HiveId != 0);
            if (m_userProfile.User.HiveId != 0 && m_viewModel.MembersList.Count() == 0) {
                m_loader.Show("Loading...");
                m_getHiveService.Execute(m_userProfile.User.HiveId, GetHiveHandler, 0, USERS_ON_SCREEN);
            }
        }

        private void GetHiveHandler(List<UserModel> users, int members, UserModel refferer, float totalImpact) {
            m_loader.Hide();
            float topTenImpact = 0;
            users.ForEach(user => {
                HiveMemberViewModel vm = GetViewModel(user);
                vm.Place = m_viewModel.MembersList.Count() + 1;
                topTenImpact += user.HiveImpact;
                m_viewModel.MembersList.Add(vm);
            });
            m_viewModel.Members.Set(members);
            m_viewModel.Impact.Set(totalImpact);
            if (users.Count > 0 ) {
                if (members > USERS_ON_SCREEN) {
                    m_viewModel.MembersList.Add(new HiveMemberViewModel("Others", -1, totalImpact - topTenImpact));
                }
            } 
            if (refferer != null) {
                m_viewModel.HasRefferer.Set(true);
                m_imagesService.GetImage(refferer.Avatar, m_viewModel.ReffererAvatar.Set);
            } else {
                m_viewModel.HasRefferer.Set(false);
            }
            
        }

        private HiveMemberViewModel GetViewModel(UserModel user) {
            var result = new HiveMemberViewModel(user.Name, user.HiveId, user.HiveImpact);
            m_imagesService.GetImage(user.Avatar, result.Avatar.Set);
            return result;
        }

        public void AddToHiveClicked() {
            m_searchHivePlayers.Show();
        }

        public void ViewAllClicked() {
            m_hiveLeaderboard.Show(m_viewModel.HiveId);
        }

        public void RefferedByFriendClicked() {
            m_searchRefferer.Show();
        }
    }
}
