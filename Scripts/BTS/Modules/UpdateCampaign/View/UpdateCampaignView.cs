using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    public class UpdateCampaignView : BaseControlledView<IUpdateCampaignViewListener>, IUpdateCampaignView {
        [SerializeField] private InputField m_title;
        [SerializeField] private InputField m_website;
        [SerializeField] private Image m_categoryImage;
        [SerializeField] private Text m_categoryName;
        [SerializeField] private Text m_errorText;

        private IUpdateCampaignViewModel m_viewModel;

        public void SetViewModel(IUpdateCampaignViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.CategoryImage.Subscribe(SetCategoryImage);
            m_viewModel.CategoryName.Subscribe(SetCategoryName);
            m_viewModel.Website.Subscribe(SetWebsite);
            m_viewModel.CampaignTitle.Subscribe(SetTitle);
        }

        private void SetTitle(string obj) {
            m_title.text = obj;
        }

        private void SetWebsite(string obj) {
            m_website.text = obj;
        }

        private void SetCategoryName(string obj) {
            m_categoryName.text = obj;
        }

        private void SetCategoryImage(Sprite obj) {
            m_categoryImage.overrideSprite = obj;
        }

        public void OnBackClick() {
            m_controller.OnBackPressed();
        }
        
        public void OnSelectCategoryClick() {
            m_viewModel.SelectCategory();
        }
        
        public void OnSaveChangesClick() {
            m_viewModel.SaveChanges();
        }
        
        public void OnSetName() {
            m_viewModel.NewTitle = m_title.text;
        }
        
        public void OnSetWebsite() {
            m_viewModel.NewWebsite = m_website.text;
        }
        
        public void Clear() {
            m_title.text = string.Empty;
            m_website.text = string.Empty;
            m_categoryName.text = string.Empty;
            m_errorText.text = string.Empty;
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
    }
}