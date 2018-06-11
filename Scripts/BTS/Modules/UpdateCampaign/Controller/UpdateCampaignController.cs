using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCampaignController : BaseScreenController<IUpdateCampaignView>, IUpdateCampaignViewListener,
    IUpdateCampaignController, IUpdateCampaignViewModel {
    [Inject] private IFeedsService m_feedService;
    [Inject] private IFeedsModel m_feedModel;
    [Inject] private IImagesService m_imageService;
    [Inject] private ICreatePostService m_createPostService;
    [Inject] private ICampaignCategoriesModel m_categoriesModel;
    [Inject] private IUpdateCampaignService m_updateCampaignService;
    [Inject] private ICampaignCategoriesController m_campaignCategoriesController;
    [Inject] private IPopupsModel m_popupsModel;

    public Observable<string> CampaignTitle { get; private set; }
    public Observable<string> Website { get; private set; }
    public Observable<string> CategoryName { get; private set; }
    public Observable<Sprite> CategoryImage { get; private set; }
    public string NewTitle { get; set; }
    public string NewWebsite { get; set; }

    private int m_newCategory;
    public override void PostInject() {
        base.PostInject();
        m_feedModel.OnCampaignsUpdated += () => {
            m_historyService.BackPressedItem(this);
        };
    }

    public int NewCategory
    {
        get { return m_newCategory; }
        set
        {
            m_newCategory = value;
            var category = m_categoriesModel.GetCategory(value);
            CategoryName.Set(category.Title);
            m_imageService.GetImage(category.Image, CategoryImage.Set);
        }
    }

    public void SelectCategory() {
        m_campaignCategoriesController.Show(category => {
            NewCategory = category;
        });
    }

    public void SaveChanges() {
        if (NewTitle.Trim().Length < 4) {
            m_popupsModel.AddPopup(new ErrorPopupItemModel("Title too short"));
            return;
        }
        m_updateCampaignService.Execute(NewTitle, NewCategory, NewWebsite);
    }

    public UpdateCampaignController() {
        CampaignTitle = new Observable<string>();
        Website = new Observable<string>();
        CategoryName = new Observable<string>();
        CategoryImage = new Observable<Sprite>();
    }

    protected override void PostSetView() {
        base.PostSetView();
        m_view.SetViewModel(this);
    }

    public override void Show() {
        var post = m_feedModel.UserCampaign.GetPosts()[0];
        CampaignTitle.Set(post.Campaign.Title);
        Website.Set(post.Campaign.Website);
        NewCategory = post.Campaign.Category;
        NewWebsite = post.Campaign.Website;
        NewTitle = post.Campaign.Title;
        base.Show();
    }
}