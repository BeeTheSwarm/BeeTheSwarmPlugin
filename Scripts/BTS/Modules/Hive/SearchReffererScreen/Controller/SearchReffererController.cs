using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    internal class SearchReffererController : BaseSearchHivePlayersController, ISearchReffererController {
        [Inject]
        private IConfirmJoinHivePopupController m_confirmJoinHivePopup;
        [Inject]
        private IJoinHiveService m_joinHiveService;

        
        internal override void ItemClickHandler(UserViewModel userViewModel) {
            m_confirmJoinHivePopup.Show(userViewModel, (result) => {
                if (result) {
                    m_joinHiveService.Execute(userViewModel.UserId);
                }
                BackButtonPressedHandler();
            });
        }
    }
}
