using System;
using UnityEngine;

namespace BTS  {
    public class UpdatePostController:BaseScreenController<IUpdatePostView>, IUpdatePostController,IUpdatePostViewListener, IUpdatePostViewModel {
        [Inject] private IImagesService m_imagesService;
        [Inject] private IUpdatePostService m_updatePostService;
        [Inject] private IFeedsModel m_feedsModel;
        [Inject] private IPopupsModel m_popupsModel;

        public Observable<Sprite> PostImage { get; private set; }
        public Observable<string> Title { get; private set; }
        public Observable<string> Description { get; private set; }

        public Observable<Texture2D> NewPostImage { get; private set; }
        public string NewTitle { get; set; }
        public string NewDescription { get; set; }

        private const int TEXTURE_SIZE = 1000;

        public UpdatePostController() {
            PostImage = new Observable<Sprite>();
            Title = new Observable<string>();
            Description = new Observable<string>();
            NewPostImage = new Observable<Texture2D>();
        }

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(this);
        }

        private int m_postId;
        public void Show(int postId) {
            m_postId = postId;
            m_post = m_feedsModel.GetPost(m_postId);
            Title.Set(m_post.Title);
            Description.Set(m_post.Description);
            m_imagesService.GetImage(m_post.Image, PostImage.Set);
            NewTitle = m_post.Title;
            NewDescription = m_post.Description;
            base.Show();
        }

        private PostModel m_post;
        public void SaveChanges() {
            if (NewDescription.Trim().Length < 4) {
                m_popupsModel.AddPopup(new ErrorPopupItemModel("Description too short"));
                return;
            }
            if (NewTitle.Trim().Length < 4) {
                m_popupsModel.AddPopup(new ErrorPopupItemModel("Title too short"));
                return;
            }
            m_post.OnUpdate += OnPostUpdated;

            m_updatePostService.Execute(m_postId, NewTitle, NewDescription, NewPostImage.Get());
        }

        private void OnPostUpdated() {
            m_post.OnUpdate -= OnPostUpdated;
            m_historyService.BackPressedItem(this);
        }

        public void UpdateImage() {
            Platform.Adapter.GetGalleryImage(texture => {
                NewPostImage.Set(CropTexture(ResizeTexture(texture))); 
                PostImage.Set(texture.ToSprite());
            });
        }

        private Texture2D ResizeTexture(Texture2D texture) {
            if (texture.width > TEXTURE_SIZE || texture.height > TEXTURE_SIZE) {
                float ratio = (float) texture.width / texture.height;
                if (ratio > 1) {
                    texture.Resize((int) (TEXTURE_SIZE * ratio), TEXTURE_SIZE);
                }
                else {
                    texture.Resize(TEXTURE_SIZE, (int) (TEXTURE_SIZE / ratio));
                }

                texture.Apply();
            }

            return texture;
        }

        private Texture2D CropTexture(Texture2D texture) {
            if (texture.height == texture.width) {
                return texture;
            }

            int targetTextureSide = Math.Min(texture.height, texture.width);
            Texture2D result = new Texture2D(targetTextureSide, targetTextureSide);
            int x = texture.width > targetTextureSide ? (texture.width - targetTextureSide) / 2 : 0;
            int y = texture.height > targetTextureSide ? (texture.height - targetTextureSide) / 2 : 0;
            result.SetPixels(texture.GetPixels(x, y, targetTextureSide, targetTextureSide));
            result.Apply();
            return result;
        }

    }
}