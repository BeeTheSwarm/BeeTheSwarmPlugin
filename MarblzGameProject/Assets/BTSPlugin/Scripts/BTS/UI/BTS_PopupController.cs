using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

using CustomExtensions;
using String = System.String;

namespace BTS {
	
	internal class BTS_PopupController : MonoBehaviour {

		enum MessageType {
			LoginSuccessful,
			LoginFail,
			BeesEarned,
			StatsUpdate,
			CurrentStats,
			ErrorMessage
		};

		[Header("Welcome")] 
		[SerializeField] private GameObject _welcomeScreen;
		[SerializeField] private Text _name;
		[SerializeField] private Image _avatar;

		[Header("Login fail")] 
		[SerializeField] private Text _errorMessage;

		[Header("Current state")] 
		[SerializeField] private GameObject _currentStateScreen;
		[SerializeField] private Text _beesValue;
		[SerializeField] private Text _levelValue;
		[SerializeField] private Image _progressBar;
		[SerializeField] private Animator _levelUpAnimator;
		
		[Header("Bees earned")] 
		[SerializeField] private GameObject _earnedBeesScreen;
		[SerializeField] private Text _beesEarnedValue;

		[Space(5)] 
		[SerializeField] private Animator _mainAnimator;
		[SerializeField] private BTS_UIAnimatedTitle _beesAnimator;
		[SerializeField] private BTS_UIAnimatedTitle _levelAnimator;

		private const int _nameLimit = 7;
		
		private bool _isShowing = false;
		private bool _isLevelUp = false;
		private float _showDuration;
		private float _duration = 2f;
		private float _levelUpDuration = 1.1f;
		private bool _autoFadeOut = true;

		//Progress bar
		private const string ANIMATOR_TRIGGER = "LevelUp";
		private const float	_animationDuration = 2f;
		private bool 	_progressBarAnimationTrigger = false;
		private float 	_amountDestination = 0f;
		private float 	_interpolationParameter = 0f;
		
		private BTS_Player _player;

		bool _startFadeOut = false;
		float _fadeOutDelay = 0f;
		float _fadeOutTime = 0f;
		float _levelUpDelay = 0f;

		int _level = 0;
		int _bees = 0;

		int _newLevel = 0;
		int _newBees = 0;

		List<KeyValuePair<MessageType, object>> _queue = new List<KeyValuePair<MessageType, object>> ();

		//--------------------------------------
		//Get/Set
		//--------------------------------------



		//--------------------------------------
		//Built-in UNITY functions
		//--------------------------------------

		void Awake () {
			DontDestroyOnLoad (gameObject);

//			_defaultAvatarSprite = _avatar.sprite;
		}

		void OnEnable () {
			BTS_Manager.OnLoginConnectionSuccessful 		+= OnLoginConnectionSuccessfullHandler;
			BTS_Manager.OnLoginConnectionFail 				+= OnLoginConnectionFailHandler;
			BTS_Manager.OnRewardEarned 						+= OnRewardEarnedHandler;
			BTS_Manager.OnRewardFail 						+= OnRewardFailHandler;
			BTS_Manager.OnDisconnected 						+= OnDisconnectedHandler;

			BTS_PlayerData.OnPlayerAvatarLoaded 			+= OnPlayerAvatarLoadedHandler;

			BTS_WebServerManager.OnRequestTimeOut 			+= OnErrorMessageHandler;
			BTS_WebServerManager.OnRegistrationFail 		+= OnErrorMessageHandler;
			BTS_WebServerManager.OnAuthCodeFail 			+= OnErrorMessageHandler;
			BTS_WebServerManager.OnResendCodeSuccessful     += OnErrorMessageHandler;
			BTS_WebServerManager.OnResendCodeFail 			+= OnErrorMessageHandler;
			BTS_WebServerManager.OnResetEmailFail           += OnErrorMessageHandler;
			BTS_WebServerManager.OnResetPasswordFail        += OnErrorMessageHandler;
			BTS_WebServerManager.OnReassignEmailFail        += OnErrorMessageHandler;
			BTS_WebServerManager.OnReassignPasswordFail     += OnErrorMessageHandler;
		}

		void OnDisable () {
			BTS_Manager.OnLoginConnectionSuccessful 		-= OnLoginConnectionSuccessfullHandler;
			BTS_Manager.OnLoginConnectionFail 				-= OnLoginConnectionFailHandler;
			BTS_Manager.OnRewardEarned 						-= OnRewardEarnedHandler;
			BTS_Manager.OnRewardFail 						-= OnRewardFailHandler;
			BTS_Manager.OnDisconnected 						-= OnDisconnectedHandler;
			
			BTS_PlayerData.OnPlayerAvatarLoaded 			-= OnPlayerAvatarLoadedHandler;

			BTS_WebServerManager.OnRequestTimeOut 			-= OnErrorMessageHandler;
			BTS_WebServerManager.OnRegistrationFail 		-= OnErrorMessageHandler;
			BTS_WebServerManager.OnAuthCodeFail 			-= OnErrorMessageHandler;
			BTS_WebServerManager.OnResendCodeSuccessful     -= OnErrorMessageHandler;
			BTS_WebServerManager.OnResendCodeFail 			-= OnErrorMessageHandler;
			BTS_WebServerManager.OnResetEmailFail           -= OnErrorMessageHandler;
			BTS_WebServerManager.OnResetPasswordFail        -= OnErrorMessageHandler;
			BTS_WebServerManager.OnReassignEmailFail        -= OnErrorMessageHandler;
			BTS_WebServerManager.OnReassignPasswordFail     -= OnErrorMessageHandler;
		}

		void Update () {
			if (_progressBarAnimationTrigger) {

				if (_isLevelUp) {
					if (_levelUpDelay < _levelUpDuration) {
						_levelUpDelay += Time.deltaTime;
						return;
					}
					_levelUpDelay = 0;
					_isLevelUp = false;
					_progressBar.fillAmount = 0;
				} else {
					if (_interpolationParameter < 1.5f) {
						_progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, _amountDestination, _interpolationParameter);

						_interpolationParameter += Time.deltaTime / _animationDuration;
						if (_progressBar.fillAmount == 1) {
							_level++;
							_levelValue.text = _level.ToString ().FormatNumber ();
							_levelAnimator.SetNewTitle (_level.ToString ().FormatNumber ());
							_levelAnimator.StartAnimation ();
							_levelUpAnimator.SetTrigger(ANIMATOR_TRIGGER);
							_isLevelUp = true;
							_amountDestination = 0;
							_progressBar.fillAmount = _amountDestination;
						}
					} else {
						_progressBar.fillAmount = _amountDestination;
						_progressBarAnimationTrigger = false;
					}
				}
			}

			if (_startFadeOut == true) {
				if (Time.realtimeSinceStartup - _fadeOutTime >= _fadeOutDelay) {
					_startFadeOut = false;
					_fadeOutDelay = 0f;
					_fadeOutTime = 0f;

					FadeOut ();
				}
			}
		}

		//--------------------------------------
		//Public functions
		//--------------------------------------
		
		public void ShowError (string message) {
			_errorMessage.text = message;
			_errorMessage.gameObject.SetActive(true);
			if (_isShowing == true) {
				KeyValuePair<MessageType, object> pair = new KeyValuePair<MessageType, object> (MessageType.ErrorMessage, (object)message);
				if (!_queue.Contains (pair))
					_queue.Add (pair);
			} else {
				ShowMessage<string> (MessageType.ErrorMessage, message);
			}
		}

		public void ShowCurStats() {
			ShowMessage<string> (MessageType.CurrentStats, String.Empty);
		}

		//--------------------------------------
		//Private functions
		//--------------------------------------

		private void FadeIn () {
			if (_isShowing == true)
				return;

			_isShowing = true;
			_mainAnimator.SetTrigger ("FadeIn");
		}

		private void FadeOut () {
			if (_isShowing == false)
				return;

			_mainAnimator.SetTrigger ("FadeOut");
		}

		private void ShowMessage<T> (MessageType messageType, T parameter) {
			HideAllComponents ();
			_showDuration = _duration;
			
			switch (messageType) {

			case MessageType.LoginSuccessful:
				ShowWelcome ();
				break;

			case MessageType.LoginFail:
				ShowLoginFail ();
				break;

			case MessageType.BeesEarned:
				ShowBeesEarned (Convert.ToInt16(parameter));
				break;

			case MessageType.StatsUpdate:
				ShowStatsUpdate();
				break;
				
			case MessageType.CurrentStats:
				ShowCurrentStats ();
				break;
				
			case MessageType.ErrorMessage:
				_showDuration = _duration + 0.5f;
				ShowErrorMessage (parameter.ToString ());
				break;
			}
		}

		private void ShowWelcome () {
			string name = BTS_PlayerData.Instance.Player.Name;

			if (name.Length > _nameLimit) {
				name.Substring(0, _nameLimit);
			}

			_welcomeScreen.SetActive(true);
			_name.text = name;
			if (BTS_PlayerData.Instance.PlayerAvatar != null) {
				_avatar.gameObject.SetActive(true);
				_avatar.sprite = BTS_PlayerData.Instance.PlayerAvatar;
			} else {
				_avatar.gameObject.SetActive(false);
			}

			FadeIn ();
		}

		private void ShowLoginFail () {
			_errorMessage.gameObject.SetActive(true);

			FadeIn ();
		}

		private void ShowStatsUpdate () {
			_currentStateScreen.SetActive(true);

			_isLevelUp = false;
			_newBees = BTS_PlayerData.Instance.Player.Bees;
			_newLevel = BTS_PlayerData.Instance.Player.Level;

			//Bees
			if (_newBees != _bees) {
				_beesValue.text = _bees.ToString ().FormatNumber ();
				_beesAnimator.SetNewTitle (_newBees.ToString ().FormatNumber ());
				_beesAnimator.StartAnimation ();
				_bees = _newBees;
			}

			//Level
			if (_newLevel > _level) {
				_level = _newLevel;
				_levelValue.text = _level.ToString ().FormatNumber ();
				_levelAnimator.SetNewTitle (_level.ToString ().FormatNumber ());
				_levelAnimator.StartAnimation ();
				_levelUpAnimator.SetTrigger(ANIMATOR_TRIGGER);
				_isLevelUp = true;
			}
			_interpolationParameter = 0;
			_amountDestination = BTS_PlayerData.Instance.Player.LevelProgress / 100f;
			_progressBarAnimationTrigger = true;

			FadeIn ();
		}

		private void ShowBeesEarned (int beesEarned) {
			_beesEarnedValue.text = beesEarned.ToString ();
			_earnedBeesScreen.SetActive(true);

			FadeIn ();
		}

		private void ShowCurrentStats () {
			_isLevelUp = false;
			_bees = BTS_PlayerData.Instance.Player.Bees;
			_level = BTS_PlayerData.Instance.Player.Level;

			_currentStateScreen.SetActive(true);
			_amountDestination = BTS_PlayerData.Instance.Player.LevelProgress / 100f;
			_progressBarAnimationTrigger = true;
			
			if (_amountDestination == 1) {
				_amountDestination = 0;
				_level++;
			}
			
			_beesValue.text = _bees.ToString ().FormatNumber ();
			_levelValue.text = _level.ToString ().FormatNumber ();

			FadeIn ();
		}

		private void ShowErrorMessage (string message) {
			_errorMessage.text = message;
			_errorMessage.gameObject.SetActive(true);

			FadeIn ();
		}

		private void OnPopupFadeOut () {
			_isShowing = false;

			if (_queue.Count > 0) {
				KeyValuePair<MessageType, object> keyValuePair = _queue [0];
				_queue.RemoveAt (0);

				ShowMessage (keyValuePair.Key, keyValuePair.Value);
			}
		}

		private void HideAllComponents () {
			_welcomeScreen.SetActive(false);
			_currentStateScreen.SetActive(false);
			_earnedBeesScreen.SetActive(false);
			_errorMessage.gameObject.SetActive(false);
			_errorMessage.gameObject.SetActive(false);
		}

		private void FadeOutAfter () {
			if (_autoFadeOut == false)
				return;

			float delay = _showDuration;
			_fadeOutTime = Time.realtimeSinceStartup;
			_fadeOutDelay = delay;
			_startFadeOut = true;
		}

		//--------------------------------------
		//Handlers
		//--------------------------------------

		private void OnLoginConnectionSuccessfullHandler () {
			if (BTS_PlayerData.Instance.PlayerAvatar != null) {
				_avatar.gameObject.SetActive(true);
				_avatar.sprite = BTS_PlayerData.Instance.PlayerAvatar;
			}
			else {
				_avatar.gameObject.SetActive(false);				
			}
			
			if (BTS_Manager.Instance.State != ConnectionState.Connected)
				return;

			if (_isShowing) {
				_queue.Add (new KeyValuePair<MessageType, object> (MessageType.LoginSuccessful, 0));
			} else {
				ShowMessage (MessageType.LoginSuccessful, 0);
			}
			_queue.Add (new KeyValuePair<MessageType, object> (MessageType.CurrentStats, 0));
		}

		private void OnLoginConnectionFailHandler (string error) {

			_errorMessage.text = error;
			if (_isShowing == true) {
				_queue.Add (new KeyValuePair<MessageType, object> (MessageType.LoginFail, (object) 0));
			} else {
				ShowMessage (MessageType.LoginFail, 0);
			}
		}

		private void OnRewardEarnedHandler (int bees, bool showNativeUI) {
			Debug.Log("Show Reward Message");
			
			if (showNativeUI != true) {
				_beesAnimator.SetTitle (bees.ToString ().FormatNumber ());
				return;
			}

			if (bees > 0) {
				if (_isShowing) {
					_queue.Add(new KeyValuePair<MessageType, object>(MessageType.BeesEarned, (object) bees));
				} else {
					ShowMessage(MessageType.BeesEarned, bees);
				}
				_queue.Add(new KeyValuePair<MessageType, object>(MessageType.StatsUpdate, (object) 0));
			} else {
				if (_isShowing) {
					_queue.Add(new KeyValuePair<MessageType, object>(MessageType.CurrentStats, (object) 0));
				} else {
					ShowMessage(MessageType.StatsUpdate, 0);
				}
			}
		}

		private void OnRewardFailHandler (string error) {
			_errorMessage.text = error;
			ShowMessage (MessageType.ErrorMessage, error);
		}

		private void OnPlayerAvatarLoadedHandler () {
			if (BTS_PlayerData.Instance.PlayerAvatar != null) {
				_avatar.gameObject.SetActive(true);
				_avatar.sprite = BTS_PlayerData.Instance.PlayerAvatar;
			}
			else {
				_avatar.gameObject.SetActive(false);
			}

			/*if (BTS_Manager.Instance.State != ConnectionState.Connected)
				return;

			if (_isShowing == true) {
				_queue.Add (new KeyValuePair<MessageType, int> (MessageType.LoginSuccessful, 0));
			} else {
				ShowMessage (MessageType.LoginSuccessful, 0);
			}
			_queue.Add (new KeyValuePair<MessageType, int> (MessageType.CurrentStats, 0));*/
		}

		private void OnDisconnectedHandler () {
			_queue.Clear ();
			_startFadeOut = false;
			FadeOut ();

			_avatar.gameObject.SetActive(false);				
		}

		void OnErrorMessageHandler (string error) {
			if (_errorMessage.text == error) {
				return;
			}

			if (_isShowing == true) {
				KeyValuePair<MessageType, object> pair = new KeyValuePair<MessageType, object> (MessageType.ErrorMessage, (object)error);
				if (!_queue.Contains (pair))
					_queue.Add (pair);
			} else {
				ShowMessage (MessageType.ErrorMessage, error);
			}
		}
	}
}
