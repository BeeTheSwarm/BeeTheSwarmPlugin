using System;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    public class TutorialGameView: MonoBehaviour {
        public event Action<int> OnStateChanged = delegate {};
        public event Action<int> OnGameFinished = delegate {};
        [SerializeField] private GameObject m_initPanel;
        [SerializeField] private GameObject m_startGamePanel;
        [SerializeField] private GameObject m_progressPanel;
        [SerializeField] private GameObject m_resultPanel;
        [SerializeField] private GameObject m_gameContainer;
        [SerializeField] private MiniGameBallView m_ball;
        [SerializeField] private Text m_scoreText;
        [SerializeField] private Text m_greatScoreText;
        [SerializeField] private Text m_resultScoreText;

        private void Awake() {
            m_ball.OnGameStarted += StartGameHandler;
            m_ball.OnGameOver += GameOverHandler;
        }
        
        private void ScoreAddedHandler() {
            m_scoreText.text = m_ball.Score.ToString();
            if (m_ball.Score > 10 && !m_progressPanel.activeInHierarchy) {
                OnStateChanged.Invoke(2);
            }
            m_greatScoreText.text = "Great\n"+m_ball.Score;
        }

        private void StartGameHandler() {
            OnStateChanged.Invoke(1);
            m_ball.OnScoreAdded += ScoreAddedHandler;
            m_initPanel.gameObject.SetActive(false);
            m_startGamePanel.gameObject.SetActive(true);
        }

        private void GameOverHandler() {
            m_ball.OnScoreAdded -= ScoreAddedHandler;
            m_gameContainer.SetActive(false);
            m_initPanel.gameObject.SetActive(false);
            m_startGamePanel.gameObject.SetActive(false);
            m_progressPanel.gameObject.SetActive(false);
            m_resultPanel.gameObject.SetActive(true);
            m_resultScoreText.text = "You got " + m_ball.GameTime;
            OnStateChanged.Invoke(3);
            OnGameFinished.Invoke(m_ball.GameTime);
        }

    }
}