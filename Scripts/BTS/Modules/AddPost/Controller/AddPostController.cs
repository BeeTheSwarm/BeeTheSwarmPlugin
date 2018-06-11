using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    internal class AddPostController : BaseScreenController<IAddPostView>, IAddPostViewListener, IAddPostController, IAddPostViewModel {
        [Inject] private IFeedsService m_feedService;
        [Inject] private IImagesService m_imagesService;
        [Inject] private ICreatePostService m_addPostService;
        [Inject] private ILoaderController m_loader;

        private const int MINIMAL_POST_DESCRIPTION_LENGTH = 4;
        
        public Observable<Texture2D> PostImage { get; private set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }

        public AddPostController() {
            PostImage = new Observable<Texture2D>();
        }
 
        private int m_categoryId;

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(this);
        }

        public override void Show() {
            base.Show();
            m_view.Clear(); 
        }


        private const int TEXTURE_SIZE = 1000;        
        public void OnSelectImagePressed() {
            Platform.Adapter.GetGalleryImage((texture) => {
                PostImage.Set(texture.FitTextureToRectangle(TEXTURE_SIZE, TEXTURE_SIZE).MakeRectangle());});
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
                m_addPostService.Execute(PostTitle, PostDescription,
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

        public void OnStartPressed() {
            m_historyService.BackPressedItem(this);
        }
    }
}