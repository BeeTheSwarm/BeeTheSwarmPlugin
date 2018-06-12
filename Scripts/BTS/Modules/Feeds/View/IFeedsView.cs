using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS
{
    public interface IFeedsView : IControlledView, ITopPanelContainer
    { 
        IPostlistContainer GetNewPostsContainer();
        IPostlistContainer GetFavoritePostsContainer();
        IPostlistContainer GetHotPostsContainer();
        void SetViewModel(FeedListViewModel m_viewModel);
    }
}