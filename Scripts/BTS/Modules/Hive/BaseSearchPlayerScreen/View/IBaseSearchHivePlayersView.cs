using System.Collections;
using System.Collections.Generic;
using BTS;
using UnityEngine;
namespace BTS {
    internal interface ISearchHivePlayersView : IControlledView, ITopPanelContainer {
        void SetViewModel(SearchHivePlayersViewModel viewModel);
        void ShowNotFoundMessage();
        void ClearInput();
    }
}
