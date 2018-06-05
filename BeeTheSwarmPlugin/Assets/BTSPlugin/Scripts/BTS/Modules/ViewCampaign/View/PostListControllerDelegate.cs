using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class PostListControllerDelegate : IPostListControllerDelegate {
        [Inject] private ISwarmAnimationController m_swarmAnimation;
        [Inject] private IFeedsService m_feedService;
        [Inject] private IFeedsModel m_feedModel;
        [Inject] private IPopupsModel m_popupsModel;
        [Inject] private IImagesService m_imagesService;
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private ICampaignCategoriesModel m_categoriesModel;
        [Inject] private IViewCampaignController m_viewCampaignController;
        [Inject] private IInviteFriendsController m_inviteFriendsController;
        [Inject] private IEditPostController m_editPostController;
        [Inject] private IUpdateCampaignController m_updateCampaignController; 
        [Inject] private IYesNoPopupController m_yesNoController;
        [Inject] private IDeletePostService m_deletePostService;
        [Inject] private IFavoriteCampaignService m_favoriteCampaignService;
        [Inject] private IUnfavoriteCampaignService m_unfavoriteCampaignService;
        [Inject] private ICreateCommentService m_createCommentService;
        [Inject] private IFeedCampaignService m_feedCampaignService;
        [Inject] private IUpdatePostController m_updatePostController;
        [Inject] private ILoaderController m_loader;


        private ObservableList<PostViewModel> m_viewModel = new ObservableList<PostViewModel>();
        private Action<int, int, Action<List<PostModel>>> m_postSource;
        private bool m_waitingResponce;
        private int m_maxItemsCount = 100;
        private IPostlistContainer m_view;
        public bool PostsClickable { get; set; }
        public bool PostsEditable { get; set; }

        public void PostInject() {
            m_feedModel.OnCampaignDeleted += CampaignDeletedHandler;
            m_feedModel.OnPostDeleted += PostDeletedHandler;
        }

        private void PostDeletedHandler(int postId) {
            m_viewModel.Get().ForEach(post => {
                if (post.PostId == postId) {
                    m_viewModel.Remove(post);
                    Update();
                }
            });
        }

        private void CampaignDeletedHandler(int obj) {
            m_viewModel.Get().ForEach(post => {
                if (post.CampaignId == obj) {
                    m_viewModel.Remove(post);
                }
            });
        }

        public PostListControllerDelegate() {
            PostsClickable = true;
            PostsEditable = false;
        }

        private PostViewModel GenerateItemsData(PostModel post) {
            PostViewModel result = new PostViewModel(post.Id, post.Campaign.Id);
            post.OnUpdate += () => {
                result.PostTitle.Set(post.Title);
                result.PostDescription.Set(post.Description);
                m_imagesService.GetImage(post.Image, result.PostImage.Set);
            };
            result.CampaignTitle.Set(post.Campaign.Title);
            result.PostTitle.Set(post.Title);
            result.PostDescription.Set(post.Description);
            result.Editable.Set(post.Campaign.User.Id == m_userModel.UserId && PostsEditable);
            result.EditCallback = OnEdit;
            result.Date.Set(post.CreatedAt);
            m_imagesService.GetImage(post.Image, result.PostImage.Set);
            m_imagesService.GetImage(m_userModel.User.Avatar, result.UserAvatar.Set);
            var nextlevel = m_feedModel.GetCampaignLevel(post.Campaign.Level + 1);
            result.MoneyEarned.Set(m_feedModel.GetRaisedMoneyForLevel(post.Campaign.Level));
            result.CurrentTarget.Set(nextlevel != null ? nextlevel.Amount : -1);

            result.Progress.Set(post.Campaign.Funded);
            var category = m_categoriesModel.GetCategory(post.Campaign.Category);
            m_imagesService.GetImage(category.Image, result.CategoryImage.Set);
            result.OnFeed += OnFeedHandler;
            result.CommentsCount.Set(post.CommentsCount);
            var comments = post.Comments.GetRange(0, Math.Min(post.Comments.Count, 3));
            comments.ForEach(postComment => {
                CommentViewModel viewModel = CreateCommentViewModel(postComment);
                result.Comments.Add(viewModel);
            });
            result.OnCommentCallback = OnComment;
            post.OnCommentAdded += (comment) => { result.CommentsCount.Set(post.CommentsCount); };
            post.OnCommentInserted += (comment) => {
                result.CommentsCount.Set(post.CommentsCount);
                CommentViewModel viewModel = CreateCommentViewModel(comment);
                result.Comments.Insert(viewModel);
            };
            result.OnLoadMoreCommentsCallback += OnLoadMoreComments;
            result.OnPostClickCallback += OnViewCampaign;
            result.IsFavorite.Set(post.Campaign.IsFavorite);
            result.OnFavoriteClickCallback += FavoriteClickHandler;
            
            post.Campaign.OnCampaignUpdated += () => {
                result.CampaignTitle.Set(post.Campaign.Title);
                var categoryModel = m_categoriesModel.GetCategory(post.Campaign.Category);
                m_imagesService.GetImage(categoryModel.Image, result.CategoryImage.Set);
                nextlevel = m_feedModel.GetCampaignLevel(post.Campaign.Level + 1);
                result.MoneyEarned.Set(m_feedModel.GetRaisedMoneyForLevel(post.Campaign.Level));
                result.CurrentTarget.Set(nextlevel != null ? nextlevel.Amount : -1);
                result.Progress.Set(post.Campaign.Funded);
            };
            post.Campaign.AddedToFavorite += () => {
                result.IsFavorite.Set(true);
            };
            post.Campaign.RemovedFromFavorite += () => {
                result.IsFavorite.Set(false);
            };
            return result;
        }

        private void OnEdit(int postId, Vector3 obj) {
            m_editPostController.Show(responce => {
                switch (responce) {
                    case EditMenuResponce.NoAction:

                        break;
                    case EditMenuResponce.DeletePost:
                        m_yesNoController.Show("Are you sure you want to delete your post?", "Delete", "Cancel", (result) => {
                            if (result == YesNoPopupResponce.Confirmed) {
                                m_deletePostService.Execute(postId, r => { });
                            }
                        });
                        break;
                    case EditMenuResponce.EditPost:
                        m_updatePostController.Show(postId);
                        break;
                }
            }, obj);
        }

        private void OnViewCampaign(int postId) {
            if (!PostsClickable) {
                return;
            }

            var campaignId = m_feedModel.GetCampaignByPostId(postId);
            if (campaignId != null) {
                m_viewCampaignController.Show(campaignId.Value);
            }
        }

        private CommentViewModel CreateCommentViewModel(CommentModel comment) {
            CommentViewModel viewModel = new CommentViewModel(comment.User.Name, comment.Text);
            m_imagesService.GetImage(comment.User.Avatar, viewModel.UserAvatar.Set);
            return viewModel;
        }

        private void OnLoadMoreComments(int postId, int offset) {
            PostViewModel postViewModel = m_viewModel.Get().Find(model => { return model.PostId == postId; });
            if (postViewModel == null) {
                return;
            }

            m_feedService.GetPostComments(postId, offset, 3, (list) => {
                list.ForEach(comment => {
                    CommentViewModel viewModel = CreateCommentViewModel(comment);
                    postViewModel.Comments.Add(viewModel);
                });
            });
        }

        public void OnComment(int postId, string text) {
            m_createCommentService.Execute(postId, text);
        }

        private void FavoriteClickHandler(int postId, bool newStatus) {
            if (newStatus) {
                m_favoriteCampaignService.Execute(postId);
            }
            else {
                m_unfavoriteCampaignService.Execute(postId);
            }
        }

        public void OnFeedHandler(int campaignId, int count, PostViewModel viewModel) {
            if (m_userModel.User.Bees >= count)
            {
                m_swarmAnimation.Show();
                m_feedCampaignService.Execute(campaignId, count);
            }
            else {
                m_popupsModel.AddPopup(new ErrorPopupItemModel("You have not enough bees"));
            }
        }

        public void SetView(IView postlistContainer) {
        }

        public void SetMaxItems(int count) {
            m_maxItemsCount = count;
        }

        public void SetItemsSource(Action<int, int, Action<List<PostModel>>> postSource) {
            m_postSource = postSource;
        }

        public void Update() {
            if (m_waitingResponce) {
                return;
            }

            if (m_viewModel.Count() < m_maxItemsCount) {
                m_waitingResponce = true;
                m_postSource.Invoke(m_viewModel.Count(), Math.Min(3, m_maxItemsCount - m_viewModel.Count()), (list) => {
                    list.ForEach(post => { m_viewModel.Add(GenerateItemsData(post)); });
                    m_waitingResponce = false;
                });
            }
        }

        public void SetView(IPostlistContainer postlistContainer) {
            m_view = postlistContainer;
            m_view.SetViewModel(m_viewModel);
        }

        public void Clear() {
            m_viewModel.Clear();
        }
    }
}