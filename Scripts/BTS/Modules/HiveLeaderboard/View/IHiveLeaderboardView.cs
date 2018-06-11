using System.Collections;
using System.Collections.Generic;
using BTS;
using UnityEngine;
namespace BTS {
    internal interface IHiveLeaderboardView : IControlledView, ITopPanelContainer {
        void SetViewModel(HiveLeaderboardViewModel m_viewModel);
    }
}
