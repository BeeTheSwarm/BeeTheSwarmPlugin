using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    internal class HiveLeaderboardView : TopPanelScreen<IHiveLeaderboardViewListener>, IHiveLeaderboardView {
        [SerializeField]
        private HiveLeaderboardItem m_itemOrigin;
        [SerializeField]
        private Transform m_itemsParent;
        private HiveLeaderboardViewModel m_viewModel;

        public void MissingHivePlayerBtnClickHandler() {
            m_controller.OnMissingHivePlayerClick();
        }

        public void SetViewModel(HiveLeaderboardViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.MembersList.OnAdd += AddItem;
        }

        private void AddItem(HiveLeaderboaderItemViewModel value) {
            HiveLeaderboardItem item = GameObjectInstatiator.InstantiateFromObject(m_itemOrigin);
            item.transform.SetParent(m_itemsParent, false);
            item.SetViewModel(value, m_viewModel.MembersList.Count());
            item.transform.SetAsLastSibling();
        }

        public void ScrolledToTheEnd() {
            m_controller.ScrolledToEnd();
        }
    }

}