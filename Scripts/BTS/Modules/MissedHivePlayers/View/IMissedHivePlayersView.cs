using System.Collections;
using System.Collections.Generic;
using BTS;
using UnityEngine;

namespace BTS {
    internal interface IMissedHivePlayersView : IControlledView {
        void SetViewModel(MissedHivePlayersViewModel m_viewModel);
    }
}
