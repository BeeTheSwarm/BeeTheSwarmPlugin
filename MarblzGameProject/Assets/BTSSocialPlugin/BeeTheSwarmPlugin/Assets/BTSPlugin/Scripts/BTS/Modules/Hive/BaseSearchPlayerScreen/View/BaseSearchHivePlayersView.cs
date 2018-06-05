using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    internal class BaseSearchHivePlayersView : TopPanelScreen<ISearchHivePlayersViewListener>, ISearchHivePlayersView {
        [SerializeField]
        private Text m_notFoundText;
        [SerializeField]
        private SearchHiveItemView m_itemOrigin;
        [SerializeField]
        private InputField m_inputField;
        [SerializeField]
        private Transform m_itemsParent;
        private List<SearchHiveItemView> m_items = new List<SearchHiveItemView>();
        private SearchHivePlayersViewModel m_viewModel;

        public void SetViewModel(SearchHivePlayersViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.SearchResult.OnAdd += AddItem;
            m_viewModel.SearchResult.OnClear += ClearItems;
        }

        private void ClearItems() {
            m_items.ForEach(item => {
                Destroy(item.gameObject);
            });
            m_items.Clear();
        }

        private void AddItem(UserViewModel obj) {
            SearchHiveItemView item = GameObjectInstatiator.InstantiateFromObject(m_itemOrigin);
            item.transform.SetParent(m_itemsParent, false);
            item.SetViewModel(obj);
            m_items.Add(item);
        }

        public void OnInputFieldChanged(string text) {
            m_notFoundText.gameObject.SetActive(false);
            m_controller.SearchFieldChanged(text);
        }

        public void OnInputFieldEndEdit(string text) {
            m_notFoundText.gameObject.SetActive(false);
            m_controller.SearchFieldEndEditHandler(text);
        }

        public void OnScrolledToEnd() {
            m_controller.OnScrolledToTheEnd();
        }

        public void ShowNotFoundMessage() {
            m_notFoundText.gameObject.SetActive(true);
        }

        public void ClearInput() {
            m_inputField.text = string.Empty;
        }
    }

}