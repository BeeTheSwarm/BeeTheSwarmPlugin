using BTS.OurGames.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace BTS.OurGames.View {
    public class OurGamesItemView : MonoBehaviour {
        [SerializeField] private Image m_icon;
        [SerializeField] private Text m_name;

        private OurGamesItemViewModel m_viewModel;
        public void SetViewModel(OurGamesItemViewModel vewModel) {
            m_viewModel = vewModel;
            m_viewModel.GameIcon.Subscribe(SetIcon);
            m_name.text = m_viewModel.GameName.Get();
        }

        private void SetIcon(Sprite obj) {
            m_icon.overrideSprite = obj;
        }

        public void OnDownloadClick() {
            m_viewModel.Download();
        }
    }
}