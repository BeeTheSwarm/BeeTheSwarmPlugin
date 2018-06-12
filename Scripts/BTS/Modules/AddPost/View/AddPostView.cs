using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    internal class AddPostView : BaseControlledView<IAddPostViewListener>, IAddPostView {
        [SerializeField] private InputField m_postTitle;
        [SerializeField] private InputField m_postDescription;

        [SerializeField] private Image m_campaignImage;

        [SerializeField] private Button m_selectImageBtn;
        [SerializeField] private Text m_errorText;
        private IAddPostViewModel m_viewModel;

        public void OnBackClick() {
            m_controller.OnBackPressed();
        }
        
        public void OnCreatePostClick() {
            m_viewModel.CreatePost();
        }

        public void OnPostTitleSetClick() {
            m_viewModel.PostTitle = m_postTitle.text;
        }

        public void OnDescriptionSetClick() {
            m_viewModel.PostDescription = m_postDescription.text;
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

        public void SetViewModel(IAddPostViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.PostImage.Subscribe(SetImage);
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
            m_postTitle.text = string.Empty;
            m_postDescription.text = string.Empty;
            SetImage(null);
        }

    }
}