using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    public interface IInviteFriendsView : IControlledView, ITopPanelContainer {
        void SetViewModel(InviteFriendViewModel m_viewModel);
    }
}