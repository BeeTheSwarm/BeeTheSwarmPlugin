using System;
using System.Collections;
using System.Collections.Generic;
using BTS.OurGames.Controller;
using BTS.OurGames.View;
using UnityEngine;
using UnityEngine.UI;

public class OurGamesView : TopPanelScreen<IOurGamesViewListener>, IOurGamesView {
    [SerializeField] private Transform m_itemsParent;
    [SerializeField] private OurGamesItemView m_itemOrigin;
    public void SetViewModel(ObservableList<OurGamesItemViewModel> viewModel) {
        viewModel.OnAdd += AddItem;
    }

    private void AddItem(OurGamesItemViewModel obj) {
        var item = Instantiate(m_itemOrigin, m_itemsParent, false);
        item.SetViewModel(obj);
        
    }
}
