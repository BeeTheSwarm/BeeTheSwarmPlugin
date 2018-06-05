using System.Collections;
using System.Collections.Generic;
using BTS.OurGames.Controller;
using UnityEngine;

public interface IOurGamesView : ITopPanelContainer {
    void SetViewModel(ObservableList<OurGamesItemViewModel> viewModel);
}
