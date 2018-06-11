using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    public class InviteFriendsController : TopPanelScreenController<IInviteFriendsView>, IInviteFriendsViewListener,
        IInviteFriendsController {
        [Inject] private IUserProfileModel m_userProfileModel;
        private InviteFriendViewModel m_viewModel = new InviteFriendViewModel();
        [Inject] private IContactPickerController m_contactPickerController;
        [Inject] private IInviteFriendsPhoneService m_invitePhonesService;
        [Inject] private IInviteFriendsEmailService m_inviteMailsService;
        [Inject] private ISettingsModel m_settingsModel;

        public void CopyCode() {

            var code = m_userProfileModel.User.HiveCode;
            Platform.Adapter.CopyToClipboard(code);
                            
        }

        protected override bool BackButtonEnabled
        {
            get { return true; }
        }

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(m_viewModel);
        }

        public void InviteFriendsByMail() {
            m_contactPickerController.ShowEmailPicker(OnEmailPicked, "Invite friends", "Send");
        }

        private void OnEmailPicked(List<string> list) {
            List<string> emails = list;
            if (emails.Count > 0) {
                m_inviteMailsService.Execute(emails);
            }
        }

        public void InviteFriendsByPhone() {
            m_contactPickerController.ShowPhonesPicker(OnPhonesPicked, "Invite friends", "Send");
        }

        private void OnPhonesPicked(List<string> list) {
            List<string> phones = list;
            if (phones.Count > 0) {
                m_invitePhonesService.Execute(phones);
            }
        }

        public override void Show() {
            base.Show();
            m_viewModel.RewardForInvite.Set(m_settingsModel.GetSettingValue(SettingType.InvitedUser));
            m_viewModel.ReferalCode.Set(m_userProfileModel.User.HiveCode);
            m_view.Show();
        }
    }
}