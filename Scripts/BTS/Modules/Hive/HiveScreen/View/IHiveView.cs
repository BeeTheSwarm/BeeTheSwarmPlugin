using System.Collections;
using System.Collections.Generic;
using BTS;
using UnityEngine;

namespace BTS {
    internal interface IHiveView : IControlledView, ITopPanelContainer {
        void SetViewModel(HiveViewModel m_viewModel);
    }
}
