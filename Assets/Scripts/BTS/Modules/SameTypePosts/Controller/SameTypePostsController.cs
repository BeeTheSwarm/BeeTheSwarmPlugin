using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class SameTypePostsController : TopPanelScreenController<ISameTypePostsView>, ISameTypePostsController, ISameTypePostsViewListener {
        [Inject]
        private IPostListControllerDelegate m_postsControllerDelegate;
        [Inject]
        private IFeedsService m_feedsService;
        [Inject]
        private IGetTopPostsService m_topPostLoaderService;
        [Inject]
        private IGetFavoritePostsService m_favoritePostLoaderService;
        [Inject]
        private IGetPostsService m_postLoaderService;

        protected override bool BackButtonEnabled {
            get {
                return true;
            }
        }

        public override void PostInject() {
            base.PostInject();
            m_postsControllerDelegate.SetView(m_view.GetPostlistContainer());
            m_postsControllerDelegate.SetMaxItems(100);
        }

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetInitCallback(() => {
                m_topPanelControllerDelegate.SetView(m_view.GetTopPanel());
            });
        }

        public void Show(PostTypes type) {
            base.Show();
            m_postsControllerDelegate.Clear();
            switch (type) {
                case PostTypes.Favorite:
                    m_postsControllerDelegate.SetItemsSource(m_favoritePostLoaderService.Execute);
                    break;
                case PostTypes.Hot:
                    m_postsControllerDelegate.SetItemsSource(m_topPostLoaderService.Execute);
                    break;
                case PostTypes.New:
                    m_postsControllerDelegate.SetItemsSource(m_postLoaderService.Execute);
                    break;
            }
            m_postsControllerDelegate.Update();
        }

        public void OnScrolledToEnd() {
            m_postsControllerDelegate.Update();
        }
    }
}