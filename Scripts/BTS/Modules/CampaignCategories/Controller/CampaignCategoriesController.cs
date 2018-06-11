using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class CampaignCategoriesController: BasePopupController<ICampaignCategoriesView>, ICampaignCategoriesViewListener, ICampaignCategoriesController {

    [Inject] private ICampaignCategoriesModel m_categoriesModel;
    [Inject] private IImagesService m_imagesService;
    public CampaignCategoriesController()
    {
    }

    public override void PostInject() {
        base.PostInject();
        m_categoriesModel.OnCategoriesLoaded += OnCategoriesLoadedHandler;
    }

    private void OnCategoriesLoadedHandler() {
        List<CategoryItemViewModel> categoriesViewModels = new List<CategoryItemViewModel>();
        var list = m_categoriesModel.GetCategories();
        list.ForEach(category => {
            CategoryItemViewModel viewModel = new CategoryItemViewModel();
            viewModel.Id = category.Id;
            viewModel.Title = category.Title;
            m_imagesService.GetImage(category.Image, viewModel.Image.Set);
            viewModel.OnClick += OnCategorClickedHandler;
            categoriesViewModels.Add(viewModel);
        });
        m_view.SetCategories(categoriesViewModels);
    }

    private void OnCategorClickedHandler(int categoryId) {
        m_callback.Invoke(categoryId);
        Hide();
    }

    private Action<int> m_callback;
    public void Show(Action<int> callback) {
        m_callback = callback;
        base.Show();
    }
}
