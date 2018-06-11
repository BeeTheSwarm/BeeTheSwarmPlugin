using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    internal class StartCampaignView : BaseControlledView<IStartCampaignViewListener>, IStartCampaignView {
        [SerializeField] private InputField m_campaignTitle;
        [SerializeField] private InputField m_campaignWebsite;
        [SerializeField] private InputField m_postTitle;
        [SerializeField] private Text m_categoryName;
        [SerializeField] private Image m_categoryImage;
        [SerializeField] private InputField m_postDescription;
        [SerializeField] private GameObject m_createPostView;
        [SerializeField] private GameObject m_createCampaignView;

        [SerializeField] private Image m_campaignImage;

        [SerializeField] private Button m_selectImageBtn;
        [SerializeField] private Text m_errorText;
        private IStartCampaignViewModel m_viewModel;

        public void OnCreateCampaignClick() {
            m_viewModel.CreateCampaign();
        }
        
        public void OnPostViewBackClick() {
            ShowCampaingScreen();
        }

        private void ShowCampaingScreen() {
            m_createPostView.SetActive(false);
            m_createCampaignView.SetActive(true);
        }
        
        public void OnBackClick() {
            m_controller.OnBackPressed();
        }
        
        public void OnSelectCategoryClick() {
            m_controller.OnSelectCategoryClicked();
        }
        
        public void OnCreatePostClick() {
            m_viewModel.CreatePost();
        }

        public void OnCampaignTitleSet() {
            m_viewModel.CampaignTitle = m_campaignTitle.text;
        }
        
        public void OnPostTitleSetClick() {
            m_viewModel.PostTitle = m_postTitle.text;
        }

        public void OnDescriptionSetClick() {
            m_viewModel.PostDescription = m_postDescription.text;
        }

        public void OnWebSiteSet() {
            m_viewModel.Website = m_campaignWebsite.text;
        }

        public void OnSelectImageClick() {
            m_controller.OnSelectImagePressed();
        }
        
        private void SetImage(Texture2D image) {
            if (image == null) {
                m_selectImageBtn.gameObject.SetActive(true);
                m_campaignImage.gameObject.SetActive(false);
                m_campaignImage.overrideSprite = null;
            }
            else {
                m_campaignImage.overrideSprite = image.ToSprite();
                m_campaignImage.gameObject.SetActive(true);
                m_selectImageBtn.gameObject.SetActive(false);
            }
        }

        public void SetViewModel(IStartCampaignViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.CategoryImage.Subscribe(SetCategoryImage);
            m_viewModel.CategoryName.Subscribe(SetCategoryName);
            m_viewModel.PostImage.Subscribe(SetImage);
        }

        private void SetCategoryName(string obj) {
            m_categoryName.text = obj;
        }

        private void SetCategoryImage(Sprite obj) {
            m_categoryImage.overrideSprite = obj;
        }

        public void ShowError(string errorText) {
            StartCoroutine(ShowErrorText(errorText));
        }

        private IEnumerator ShowErrorText(string errorText) {
            m_errorText.gameObject.SetActive(true);
            m_errorText.text = errorText;
            yield return new WaitForSeconds(4f);
            m_errorText.gameObject.SetActive(false);
            m_errorText.text = string.Empty;
        }

        public void Clear() {
            m_campaignTitle.text = string.Empty;
            m_campaignWebsite.text = string.Empty;
            m_postTitle.text = string.Empty;
            m_postDescription.text = string.Empty;
            SetImage(null);
            ShowCampaingScreen();
        }

        public void ShowPostView() {
            m_createPostView.SetActive(true);
            m_createCampaignView.SetActive(false);

        }
    }
}