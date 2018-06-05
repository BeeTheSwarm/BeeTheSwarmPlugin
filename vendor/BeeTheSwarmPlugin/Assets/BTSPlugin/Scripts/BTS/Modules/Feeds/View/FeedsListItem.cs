using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Globalization;

namespace BTS {
    public class FeedsListItem : MonoBehaviour {

        private PostViewModel m_viewModel;
        [SerializeField]
        private Image m_categoryImage;
        [SerializeField]
        private Text m_campaignTitle;
        [SerializeField]
        private Text m_postTitle;
        [SerializeField]
        private Text m_date;
        [SerializeField]
        private Text m_moneyEarned;
        [SerializeField]
        private ProgressBar m_campaignProgress;
        [SerializeField]
        private Text m_postDescription;
        [SerializeField]
        private Image m_postImage;
        [SerializeField]
        private Text m_commentCounter;
        [SerializeField]
        private Button m_editButton;
        [Header("Comments")]
        [SerializeField]
        private Avatar m_userAvatar;
        [SerializeField]
        private PostCommentItem m_commentOrigin;
        [SerializeField]
        private Transform m_commentsParent;
        [SerializeField]
        private InputField m_commentText;
        [SerializeField]
        private Button m_loadMoreCommentsBtn;
        [SerializeField] private GameObject m_commentInputContainer;
        [SerializeField] private Toggle m_faivoriteToggle;
        private List<PostCommentItem> m_visibleComments = new List<PostCommentItem>();

        private IFeedItemListener m_feedListener;


        public PostViewModel Model {
            get {
                return m_viewModel;
            }
        }

        public void OnClickOnPost() {
            if (m_viewModel.OnPostClickCallback != null) {
                m_viewModel.OnPostClickCallback.Invoke(m_viewModel.PostId);
            }
        }

        internal void SetModel(PostViewModel model) {
            m_viewModel = model;
            m_viewModel.UserAvatar.Subscribe(m_userAvatar.SetAvatar);
            m_viewModel.PostTitle.Subscribe(SetPostTitle);
            m_viewModel.PostDescription.Subscribe(SetPostDescription);
            m_viewModel.CampaignTitle.Subscribe(SetCampaignTitle);
            m_viewModel.Date.Subscribe(SetDate);
            m_viewModel.MoneyEarned.Subscribe(SetMoneyEarned);
            m_viewModel.Editable.Subscribe(SetEditButton);
            m_viewModel.CategoryImage.Subscribe(SetCategoryImage);
            m_viewModel.IsFavorite.Subscribe(SetFavoriteMark);
            m_viewModel.PostImage.Subscribe(SetPostImage);
            m_viewModel.CurrentTarget.Subscribe(SetCurrentTarget);
            m_viewModel.Progress.Subscribe(SetProgress);
            List<CommentViewModel> comments = m_viewModel.Comments.Get();
            comments.ForEach(comment => {
                AddComment(comment);
            });
            m_viewModel.CommentsCount.Subscribe(SetCommentsCount);
            m_viewModel.Comments.OnAdd += AddComment;
            m_viewModel.Comments.OnInsert += InsertComment;
        }

        public void Unsubscribe() {
            if (m_viewModel != null) {
                m_viewModel.UserAvatar.Unsubscribe(m_userAvatar.SetAvatar);
                m_viewModel.PostTitle.Unsubscribe(SetPostTitle);
                m_viewModel.PostDescription.Unsubscribe(SetPostDescription);
                m_viewModel.CampaignTitle.Unsubscribe(SetCampaignTitle);
                m_viewModel.Date.Unsubscribe(SetDate);
                m_viewModel.MoneyEarned.Unsubscribe(SetMoneyEarned);
                m_viewModel.Editable.Unsubscribe(SetEditButton);
                m_viewModel.CategoryImage.Unsubscribe(SetCategoryImage);
                m_viewModel.IsFavorite.Unsubscribe(SetFavoriteMark);
                m_viewModel.PostImage.Unsubscribe(SetPostImage);
                m_viewModel.CurrentTarget.Unsubscribe(SetCurrentTarget);
                m_viewModel.Progress.Unsubscribe(SetProgress);
                List<CommentViewModel> comments = m_viewModel.Comments.Get();
                comments.ForEach(comment => {
                    AddComment(comment);
                });
                m_viewModel.CommentsCount.Unsubscribe(SetCommentsCount);
                m_viewModel.Comments.OnAdd -= AddComment;
                m_viewModel.Comments.OnInsert -= InsertComment;
            }
        }

        public void DisableComments() {
            m_commentInputContainer.SetActive(false);
            m_commentsParent.gameObject.SetActive(false);
            m_loadMoreCommentsBtn.gameObject.SetActive(false);
        }
        
        private void SetFavoriteMark(bool obj) {
            m_faivoriteToggle.isOn = obj;
        }

        private void SetPostTitle(string title) {
            m_postTitle.text = title;
        }

        private void SetCampaignTitle(string title) {
            m_campaignTitle.text = title;
        }

        public void OnLoadModeComments() {
            if (m_viewModel.OnLoadMoreCommentsCallback != null) {
                m_viewModel.OnLoadMoreCommentsCallback.Invoke(m_viewModel.PostId, m_visibleComments.Count);
            }
        }

        private void AddComment(CommentViewModel comment) {
            PostCommentItem instance = Instantiate(m_commentOrigin, m_commentsParent);
            instance.SetViewModel(comment);
            instance.transform.SetAsFirstSibling();
            m_visibleComments.Add(instance);
            instance.SetBackground(m_visibleComments.Count % 2 == 0);
            SetCommentsCount(m_viewModel.CommentsCount.Get());
        }

        private void InsertComment(CommentViewModel comment) {
            PostCommentItem instance = Instantiate(m_commentOrigin, m_commentsParent);
            instance.SetViewModel(comment);
            instance.transform.SetAsLastSibling();
            m_visibleComments.Add(instance);
            instance.SetBackground(m_visibleComments.Count % 2 == 0);
            SetCommentsCount(m_viewModel.CommentsCount.Get());
        }

        private void SetCommentsCount(int count) {
            m_loadMoreCommentsBtn.gameObject.SetActive(count > 0 && count > m_visibleComments.Count);
            m_commentCounter.text = "View other " + (count - m_visibleComments.Count) + " comments";
        }

        private void SetProgress(int value) {
            m_campaignProgress.SetProgress(value, m_viewModel.CurrentTarget.Get());
        }

        private void SetCurrentTarget(int value) {
            m_campaignProgress.SetProgress(m_viewModel.Progress.Get(), value);
        }

        private void SetEditButton(bool obj) {
            m_editButton.gameObject.SetActive(obj);
        }

        private void SetMoneyEarned(int obj) {
            CultureInfo ci = CultureInfo.CurrentCulture;
            m_moneyEarned.text = obj.ToString("C", ci) + " RAISED";
        }

        private string GetPostDate(int timestamp) {
            DateTime date = TimeDateUtils.FromUnixTime(timestamp);
            int days = TimeDateUtils.GetDaysDifference(date);
            if (days == 0) {
                return "Today";
            }
            else if (days == 1) {
                return "Yesturday";
            }
            else {
                return date.ToShortDateString();
            }
        }

        private void SetDate(int value) {
            m_date.text = GetPostDate(value);
        }

        private void SetPostDescription(string value) {
            m_postDescription.text = value;
        }

        private void SetCampaingTitle(string name) {
            m_campaignTitle.text = name;
        }

        private void SetCategoryImage(Sprite image) {
            m_categoryImage.overrideSprite = image;
        }

        private void SetPostImage(Sprite image) {
            m_postImage.overrideSprite = image;
        }
        
        public void OnFeed() {
            m_viewModel.Donate(1);
        }

        public void OnFavorite() {
            m_viewModel.ToggleFavorite();
            
        }

        public void OnAvatarClick() {
            if (m_viewModel.OnAvatarClickCallback != null) {
                m_viewModel.OnAvatarClickCallback.Invoke(m_viewModel.PostId);
            }
        }

        public void OnEditClick() {
            if (m_viewModel.EditCallback != null) {
                m_viewModel.EditCallback.Invoke(m_viewModel.PostId, m_editButton.transform.position);
            }
        }

        public void OnAddPostClick() {
            if (m_viewModel.EditCallback != null) {
                m_viewModel.EditCallback.Invoke(m_viewModel.PostId, m_editButton.transform.position);
            }
        }
        
        public void OnComment() {
            m_viewModel.OnCommentCallback.Invoke(m_viewModel.PostId, m_commentText.text);
            m_commentText.text = string.Empty;
        }
    }
}