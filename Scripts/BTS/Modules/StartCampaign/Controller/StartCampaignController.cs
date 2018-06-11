using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    internal class StartCampaignController : BaseScreenController<IStartCampaignView>, IStartCampaignViewListener,
        IStartCampaignController, IStartCampaignViewModel {
        [Inject] private IFeedsService m_feedService;
        [Inject] private ICampaignCategoriesModel m_categoryModel;
        [Inject] private IImagesService m_imagesService;
        [Inject] private ICreateCampaignService m_createCampaignService;
        [Inject] private ICampaignCategoriesController m_campaignCategoriesController;
        [Inject] private ILoaderController m_loader;

        public StartCampaignController() {
            PostImage = new Observable<Texture2D>();
            CategoryName = new Observable<string>();
            CategoryImage = new Observable<Sprite>();
        }

        private void ResetVars() {
            PostImage.Set(null);
            SetCategory(m_categoryModel.GetCategories()[0].Id);
            CampaignTitle = string.Empty;
            Website = string.Empty;
            PostDescription = string.Empty;
            PostTitle = string.Empty;
        }

        private int m_categoryId;

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(this);
        }

        public override void Show() {
            base.Show();
            ResetVars();
            m_view.Clear();
        }

        public string CampaignTitle { get; set; }
        public string Website { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }

        public Observable<Texture2D> PostImage { get; private set; }
        public Observable<string> CategoryName { get; private set; }
        public Observable<Sprite> CategoryImage { get; private set; }

        private const int MINIMAL_POST_DESCRIPTION_LENGTH = 4;
        private const int TEXTURE_SIZE = 1000;

        public void OnSelectImagePressed() {
            Platform.Adapter.GetGalleryImage((texture) => {
                PostImage.Set(texture.FitTextureToRectangle(TEXTURE_SIZE, TEXTURE_SIZE).MakeRectangle());
            });
        }
        
        public void CreateCampaign() {
            var error = string.Empty;
            if (string.IsNullOrEmpty(CampaignTitle)) {
                error = "Set campaign title";
            }
            else if (CampaignTitle.Trim().Length < 4) {
                error = "Title is too short";
            }

            if (string.IsNullOrEmpty(error)) {
                m_view.ShowPostView();
            }
            else {
                m_view.ShowError(error);
            }
        }

        public void CreatePost() {
            string error = string.Empty;
            if (PostImage.Get() == null) {
                error = "Image not set";
            }
            else if (string.IsNullOrEmpty(PostTitle)) {
                error = "Set post title";
            }
            else if (PostTitle.Trim().Length < 4) {
                error = "Title is too short";
            }

            if (string.IsNullOrEmpty(PostDescription)) {
                error = "Set post description";
            }
            else {
                if (PostDescription.Trim().Length < MINIMAL_POST_DESCRIPTION_LENGTH) {
                    error = "Campaign description is too short";
                }
            }

            if (string.IsNullOrEmpty(error)) {
                m_loader.Show("Saving...");
                m_createCampaignService.Execute(CampaignTitle, m_categoryId, Website, PostTitle, PostDescription,
                    PostImage.Get(),
                    (result) => {
                        m_loader.Hide();
                        if (result) {
                            m_historyService.BackPressedItem(this);
                        }
                    });
            }
            else {
                m_view.ShowError(error);
            }
        }

        
        public void OnSelectCategoryClicked() {
            m_campaignCategoriesController.Show(SetCategory);
        }

        private void SetCategory(int obj) {
            m_categoryId = obj;
            var category = m_categoryModel.GetCategory(obj);
            CategoryName.Set(category.Title);
            m_imagesService.GetImage(category.Image, CategoryImage.Set);
        }


        public void OnStartPressed() {
            m_historyService.BackPressedItem(this);
        }
    }
}