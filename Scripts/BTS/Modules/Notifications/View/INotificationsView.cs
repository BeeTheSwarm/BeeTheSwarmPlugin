using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    public interface INotificationsView : IControlledView {
        void SetViewModel(NotificationsScreenViewModel m_viewModel);
    }
}