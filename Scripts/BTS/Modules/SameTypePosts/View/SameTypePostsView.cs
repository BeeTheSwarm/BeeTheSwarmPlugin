using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace BTS {
    public class SameTypePostsView : TopPanelScreen<ISameTypePostsViewListener>, ISameTypePostsView {

        public IPostlistContainer GetPostlistContainer() {
            return m_feedContatiner;
        }

        [SerializeField] private SameTypePostsList m_feedContatiner;
        [SerializeField] private ScrollRectObserver m_scrollRectObserver;

        private void Awake() {
            m_scrollRectObserver.ScrolledToEnd.AddListener(OnScrolledToEnd);
        }
        
        public void OnScrolledToEnd() {
            m_controller.OnScrolledToEnd();
        }
    }
}