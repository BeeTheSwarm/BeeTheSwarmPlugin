using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using BTS.OurGames.Controller; 
using UnityEngine;

public class OurGamesController : TopPanelScreenController<IOurGamesView>, IOurGamesViewListener, IOurGamesController {
    [Inject] private IGetOurGamesService m_getGamesService;
    [Inject] private IImagesService m_imagesService;
    private readonly ObservableList<OurGamesItemViewModel> m_games = new ObservableList<OurGamesItemViewModel>();
    [Inject] private ILoaderController m_loader;

    public OurGamesController() {
    }


    protected override bool BackButtonEnabled
    {
        get { return true; }
    }

    public override void Show() {
        base.Show();
        if (m_games.Count() == 0) {
            m_loader.Show("Loading...");
            m_getGamesService.Execute(GameReceivedHandler);
        }
    }

    private void GameReceivedHandler(List<GameModel> obj) {
        m_loader.Hide();
        obj.ForEach(game => {
            OurGamesItemViewModel viewModel = new OurGamesItemViewModel();
            viewModel.GameName.Set(game.Title);
            viewModel.OnClick += () => { OpenGameUrl(game.Url);};
            m_imagesService.GetImage(game.Image, viewModel.GameIcon.Set);
            m_games.Add(viewModel);
        });
    }

    private void OpenGameUrl(string gameUrl) {
        Application.OpenURL(gameUrl);
    }

    protected override void PostSetView() {
        base.PostSetView();
        m_view.SetViewModel(m_games);
    }
}