using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace BTS {
    public class CategoryItemView : MonoBehaviour {
        [SerializeField]
        private Image m_image;
        [SerializeField]
        private Text m_title;

        private CategoryItemViewModel m_viewModel;

        public void SetViewModel(CategoryItemViewModel viewModel) {
            m_viewModel = viewModel;
            m_title.text = m_viewModel.Title;
            m_viewModel.Image.Subscribe(SetImage);
        }

        private void SetImage(Sprite obj) {
            m_image.overrideSprite = obj;
        }

        public void OnClick() {
            m_viewModel.Click();
        }
    }
}