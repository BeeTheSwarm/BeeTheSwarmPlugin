using System.Collections;
using System.Collections.Generic;
using BTS;
using UnityEngine;
namespace BTS {
    internal interface ISearchReffererView : IControlledView, ITopPanelContainer {
        void SetViewModel(SearchHivePlayersViewModel viewModel);
    }
}
