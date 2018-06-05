using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    public class UpdatePostView:BaseControlledView<IUpdatePostViewListener>, IUpdatePostView {
        [SerializeField] private Image m_postImage;
        [SerializeField] private InputField m_postTitle;
        [SerializeField] private InputField m_postDescription;

        private IUpdatePostViewModel m_viewModel;
        
        public void SetViewModel(IUpdatePostViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.PostImage.Subscribe(SetImage);
            m_viewModel.Description.Subscribe(SetDescription);
            m_viewModel.Title.Subscribe(SetTitle);
        }

        public void OnBackPressed() {
            m_controller.OnBackPressed();
        }
        
        public void OnTitleChanged(string text) {
            m_viewModel.NewTitle = m_postTitle.text;
        }
        
        public void OnDescriptionChanged(string text) {
            m_viewModel.NewDescription = m_postDescription.text;
        }
    
        public void OnImageChangeClick() {
            m_viewModel.UpdateImage();
        }
        
        public void OnSaveChangeClick() {
            m_viewModel.SaveChanges();
        }

        private void SetImage(Sprite obj) {
            m_postImage.overrideSprite = obj;
        }

        private void SetDescription(string obj) {
            m_postDescription.text = obj;
        }

        private void SetTitle(string obj) {
            m_postTitle.text = obj;
        }
        
        
    }
}