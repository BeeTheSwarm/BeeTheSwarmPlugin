using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace BTS {
	
	internal class BTS_EventSystemController  : SA_Singleton<BTS_EventSystemController> {

		public static Action OnClosed = delegate{};

		private const string ANIMATOR_FADE_IN_TRIGGER = "FadeIn";
		private const string ANIMATOR_FADE_OUT_TRIGGER = "FadeOut";

		[SerializeField] private InputField _levelInputField;
		[SerializeField] private InputField _scoreInputField;

		Animator _animator;

		private string _level = "";
		private string _score 	= "";

		void Awake () {
			DontDestroyOnLoad (gameObject);
			_animator = GetComponent<Animator> ();
			_levelInputField.inputType = InputField.InputType.Standard;
			_levelInputField.inputType = InputField.InputType.Standard;
		}

		void Start () {
			_animator.SetTrigger (ANIMATOR_FADE_IN_TRIGGER);
		}
		
		//------------------------------------------------------------
		// Public Methods
		//------------------------------------------------------------

		internal void Show () {
			_animator.SetTrigger (ANIMATOR_FADE_IN_TRIGGER);
		}

		internal void Hide () {
			_animator.SetTrigger (ANIMATOR_FADE_OUT_TRIGGER);
		}

		public void OnGetRewardButtonClicked() {
			if (IsValid(_level, _score)) {
				Debug.Log("valid");
				int level = Int32.Parse(_level);
				int score = Int32.Parse(_score);
				BTS_Manager.Instance.RewardEvent(level, score);
			} else {
				Debug.Log("not valid");
				ShowLog ("Input data is invalid!");
			}
		}
		
		public void OnBackButtonClick () {
			Hide ();
		}

		// Input fields
		//--------------------------------------

		public void OnLevelEndEdit() {
			_level = _levelInputField.text;
		}

		public void OnScoreEndEdit() {
			_score = _scoreInputField.text;
		}

		//------------------------------------------------------------
		// Private Methods
		//------------------------------------------------------------

		private bool IsValid (string level, string score) {
			if (level == string.Empty) {
				//show _INPUT_DATA_WARNING
				return false;
			}
			if (score == string.Empty) {
				//show _INPUT_DATA_WARNING
				return false;
			}
			return true;
		}
		
		private void ShowLog (string message) {

			BTS_Manager.Instance.Popup.ShowError (message);
		}

		private void OnDestroy () {
			Destroy (this.gameObject);
		}
		
	}
}


