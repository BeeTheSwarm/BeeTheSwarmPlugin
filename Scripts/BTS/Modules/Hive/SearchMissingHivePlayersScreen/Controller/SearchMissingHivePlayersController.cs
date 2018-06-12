using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    internal class SearchMissingHivePlayersController : BaseSearchHivePlayersController, ISearchMissingHivePlayersController {
        [Inject] private IInviteToHivePopupController m_invitePopup;
        [Inject] private IInviteToHiveService m_inviteToHiveService;
       
        internal override void ItemClickHandler(UserViewModel userViewModel) {
            m_invitePopup.Show(userViewModel, result => {
                if (result) {
                    m_inviteToHiveService.Execute(userViewModel.UserId);
                }
            });
        }
    }
}
