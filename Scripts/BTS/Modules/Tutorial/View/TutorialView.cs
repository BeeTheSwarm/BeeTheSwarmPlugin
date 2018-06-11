using System.Collections;
using System.Collections.Generic;
using BTS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace BTS {
    public class TutorialView : BaseControlledView<ITutorialViewListener>, ITutorialView {
        [SerializeField] private ScrollSnap m_scrollSnap;
        [SerializeField] private Toggle[] m_pannelsSwitches;
        [SerializeField] private GameObject[] m_gamePages;
        [SerializeField] private GameObject m_gameContainter;

        [SerializeField] private BaseFeedsList m_feedsList;
        [SerializeField] private TutorialGameView m_gameView;

        private ScrollRect m_scrollRect;
        private bool m_swarmTutorialPassed = false;
        private bool m_miniGamePassed = false;
        private const int CAMPAIGN_PAGE = 7;
        private const int GAME_PAGE = 4;

        private TutorialViewModel m_viewModel;

        void Awake() {
            m_scrollRect = m_scrollSnap.GetComponent<ScrollRect>();
            m_scrollSnap.onPageChange += OnScrollSnapPageChanged;

            m_gameView.OnStateChanged += GameStateChangedHandler;
            m_gameView.OnGameFinished += GameFinishedHandler;
        }

        private void GameFinishedHandler(int score) {
            ChangeScrollRectState(true);
            m_controller.GetReward(score);
        }

        
        
        private void GameStateChangedHandler(int state) {
            m_pannelsSwitches[GAME_PAGE + state].isOn = true;
        }


        public void SetViewModel(TutorialViewModel viewModel) {
            m_viewModel = viewModel;
        }

        public IPostlistContainer GetPostsContainer() {
            return m_feedsList;
        }

        public void ResetToStartPage() {
            m_scrollSnap.ChangePage(0);
        }

        public void HideGame() {
            m_miniGamePassed = true;
            m_gameView.gameObject.SetActive(false);
            m_scrollSnap.ChangePage(GAME_PAGE+1);
        }

        public void OnBackPressed() {
            m_controller.OnBackPressed();
        }

        public void OnInviteButtonClick() {
            m_controller.OnInviteClick();
        }

        public void OnPlayGamesButtonClick() {
            m_controller.OnPlayGamesClick();
        }

        private void OnScrollSnapPageChanged(int page) {
            SetupPageToggle(page);
            switch (page) {
                case CAMPAIGN_PAGE:
                    m_controller.OnShowCampaign();
                    if (!m_viewModel.NeedToDonate) {
                        ChangeScrollRectState(false);
                        ShowDonateHint();
                    }
                    break;
                case GAME_PAGE:
                    if (m_viewModel.TutorialAvailable) {
                        ChangeScrollRectState(false);
                        StartGame();
                    }
                    else {
                        m_scrollSnap.ChangePage(page > m_scrollSnap.CurrentPage() ? page + 1 : page - 1);
                    }

                    break;
            }
        }

        private void SetupPageToggle(int page) {
            if (page < GAME_PAGE) {
                
            }
            else {
                page = page + 3;
            }
            m_pannelsSwitches[page].isOn = true;
        }

        private void ShowDonateHint() { 
            //todo add animation
        }
        
        private void HideDonateHint() {
            // todo hide animation
        }

        private void StartGame() {
            m_gameView.gameObject.SetActive(true);
        }

        private void ChangeScrollRectState(bool active) {
            m_scrollRect.enabled = active;
        }

        private void GoToNextScreen() {
            m_scrollSnap.NextScreen();
        }
    }
}