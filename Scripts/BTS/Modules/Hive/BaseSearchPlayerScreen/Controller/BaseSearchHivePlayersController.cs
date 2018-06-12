using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {

    internal abstract class BaseSearchHivePlayersController : TopPanelScreenController<ISearchHivePlayersView>, ISearchHivePlayersViewListener, IBaseSearchHivePlayersController {

        [Inject]
        protected ISearchPlayerService m_searchPlayerService;
        [Inject]
        protected IImagesService m_imagesService;


        private SearchHivePlayersViewModel m_viewModel = new SearchHivePlayersViewModel();
        private const int MIN_SEARCH_LENGTH = 0;
        private string m_searchKeyword = string.Empty;

        public BaseSearchHivePlayersController() {

        }

        protected override bool BackButtonEnabled {
            get {
                return true;
            }
        }

        public override void Hide() {
            base.Hide();
            m_viewModel.SearchResult.Clear();
            m_view.ClearInput();
        }

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(m_viewModel);
        }

        public void OnScrolledToTheEnd() {
            if (!string.IsNullOrEmpty(m_searchKeyword)) {
                m_searchPlayerService.Execute(m_searchKeyword, m_viewModel.SearchResult.Count(), 10, SearchResultHandler);
            }
        }

        public void SearchFieldChanged(string text) {
            if (text.Length > MIN_SEARCH_LENGTH) {
                m_searchKeyword = text;
                m_viewModel.SearchResult.Clear();
                m_searchPlayerService.Execute(m_searchKeyword, 0, 10, SearchResultHandler);
            }
        }
        
        private void SearchResultHandler(List<UserModel> list) {
            list.ForEach(user => {
                UserViewModel item = new UserViewModel(user.Id, user.Name);
                item.OnClick += ItemClickHandler;
                m_imagesService.GetImage(user.Avatar, item.Avatar.Set);
                m_viewModel.SearchResult.Add(item);
            });
            if (m_viewModel.SearchResult.Count() == 0) {
                m_view.ShowNotFoundMessage();
            }
        }

        internal abstract void ItemClickHandler(UserViewModel userViewModel);

        public void SearchFieldEndEditHandler(string text) {
            SearchFieldChanged(text);
        }
    }
}
