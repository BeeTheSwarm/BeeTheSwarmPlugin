using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using BTS;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {

    internal class MissedHivePlayersView : TopPanelScreen<IMissedHivePlayersViewListener>, IMissedHivePlayersView {
        [SerializeField]
        private Text m_membersCount;
        [SerializeField]
        private Text m_membersRank;
        [SerializeField]
        private Text m_impact;
        [SerializeField]
        private Text m_impactRank;
        [SerializeField]
        private MissedHivePlayersItem m_hiveItem;
        [SerializeField]
        private Transform m_searchResultContainer;
        private List<MissedHivePlayersItem> m_searchResult = new List<MissedHivePlayersItem>();
        [SerializeField]
        private Image m_referrerAvatar;
        [SerializeField]
        private GameObject m_referrerAvatarContainer;
        [SerializeField]
        private GameObject m_addCodeButtonReferrer;

        private MissedHivePlayersViewModel m_viewModel;

        public void SetViewModel(MissedHivePlayersViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.SearchResultList.OnAdd += AddItem;
        }

        private void SetImpact(float value) {
            m_impact.text = value.ToString("C", CultureInfo.GetCultureInfo("en-us"));
        }
        
        private void AddItem(MissedHivePlayersItemViewModel value) {
            MissedHivePlayersItem item = Instantiate(m_hiveItem, m_searchResultContainer);
            item.SetViewModel(value);
            item.transform.SetAsLastSibling();
            m_searchResult.Add(item);
        }
        
    }
}