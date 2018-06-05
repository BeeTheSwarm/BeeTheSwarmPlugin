using UnityEngine;
using System.Collections;
using System;

namespace BTS {
    public interface IUserProfileScreen : IControlledView, ITopPanelContainer {
        void SetViewModel(UserProfileViewModel m_viewModel);
        IPostlistContainer GetPostlistContainer();
    }
}
