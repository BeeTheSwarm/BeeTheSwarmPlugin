using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_RewardManager : SA_Singleton<BTS_RewardManager> {

		private const string LAST_REWARDED_DATE_TOKEN = "LAST_REWARDED_DATE_TOKEN";
		private const string BEES_REWARDED_IN_ADAY = "BEES_REWARDED_IN_ADAY";

		public static event Action 					OnRewardSent = delegate{};

		public static event Action<int, bool> 		OnRewardSuccessful = delegate{};
		public static event Action<string> 			OnRewardFail = delegate{};

		public static event Action 					OnDailyBeesMaximumReached = delegate{};

		private DateTime _lastRewardedDate;
		private int _beesEarnedToday = 0;
		private bool isInitialized = false;

		private bool _showNativePopup = true;
		private bool _isDailyLimit = false;

		private int delayRefreshTime = 3600;

		//--------------------------------------
		//Get/Set
		//--------------------------------------

		public int BeesEarnedToday {
			get {
				return _beesEarnedToday;
			}
		}

		public bool IsDailyLimitReached {
			get {
				return _isDailyLimit;
			}
		}

		//--------------------------------------
		//Built-in UNITY functions
		//--------------------------------------

		void Awake () {
			DontDestroyOnLoad (gameObject);
		}

		void OnEnable () {
			BTS_Manager.OnLoginConnectionSuccessful 		+= OnLoginConnectionSuccessfulHandler;
			BTS_Manager.OnDisconnected 						+= OnDisconnectedHandler;

			BTS_Player.OnBeesEarned                         += OnAddBeesSuccessfulHandler;
			BTS_WebServerManager.OnAddBeesFail 				+= OnAddBeesFailhandler;
			BTS_WebServerManager.OnRewardEventSuccessful    += OnAddBeesSuccessfulHandler;
			BTS_WebServerManager.OnRewardEventFail          += OnAddBeesFailhandler;
		}

		void OnDisable () {
			BTS_Manager.OnLoginConnectionSuccessful 		-= OnLoginConnectionSuccessfulHandler;
			BTS_Manager.OnDisconnected 						-= OnDisconnectedHandler;

			BTS_Player.OnBeesEarned                         -= OnAddBeesSuccessfulHandler;
			BTS_WebServerManager.OnAddBeesFail 				-= OnAddBeesFailhandler;
			BTS_WebServerManager.OnRewardEventSuccessful    -= OnAddBeesSuccessfulHandler;
			BTS_WebServerManager.OnRewardEventFail          -= OnAddBeesFailhandler;

			PlayerPrefs.SetInt (BEES_REWARDED_IN_ADAY, _beesEarnedToday);
		}

		//--------------------------------------
		//Public functions
		//--------------------------------------

		public void Init () {
			isInitialized = true;

			CheckBeesReward ();
		}

		public void Reward (int bees) {
			if (Application.internetReachability == NetworkReachability.NotReachable)
				return;
			
			if (bees > 0) {
				new BTS_AddBees(bees).Send ();
			} 
			OnRewardSent ();

		}

		public void Reward (int bees, bool showNativeUI) {
			_showNativePopup = showNativeUI;
			Reward (bees);
		}
		
		public void RewardEvent(int level, int score) {
			if (Application.internetReachability == NetworkReachability.NotReachable)
				return;

			string eventKey = BTS_PlayerData.Instance.Player.GetEventKey(level);
			if (eventKey == String.Empty) {
				BTS_Manager.Instance.Popup.ShowError("Invalid level number");
				return;
			}
			new BTS_Event(eventKey, score).Send();
		}

		//--------------------------------------
		//Private functions
		//--------------------------------------

		private void CheckBeesReward () {
			if (BTS_PlayerData.Instance.Player.BtsID.ToString ().Length > 0 && PlayerPrefs.HasKey (BEES_REWARDED_IN_ADAY + BTS_PlayerData.Instance.Player.BtsID.ToString ())) {
				if (PlayerPrefs.HasKey (LAST_REWARDED_DATE_TOKEN)) {
					string last_reward_date = PlayerPrefs.GetString (LAST_REWARDED_DATE_TOKEN);
					_lastRewardedDate = DateTime.Parse (last_reward_date);

					if (_lastRewardedDate.DayOfYear < DateTime.Now.DayOfYear || _lastRewardedDate.Year != DateTime.Now.Year) {
						_beesEarnedToday = 0;
						PlayerPrefs.SetInt (BEES_REWARDED_IN_ADAY + BTS_PlayerData.Instance.Player.BtsID.ToString (), _beesEarnedToday);
						PlayerPrefs.SetString (LAST_REWARDED_DATE_TOKEN, DateTime.Now.ToString ());
						_showNativePopup = false;
					} else {
						_beesEarnedToday = PlayerPrefs.GetInt (BEES_REWARDED_IN_ADAY + BTS_PlayerData.Instance.Player.BtsID.ToString ());
					}
				} else {
					_beesEarnedToday = 0;
					_showNativePopup = false;
				}
			} else {
				_beesEarnedToday = 0;
				_showNativePopup = false;
			}

			Debug.Log ("------------------------------ BEES TODAY " + _beesEarnedToday);
		}

		private void DispatchOnTimeCustomPopupShowing () {
			_showNativePopup = true;
		}

		private void StartRefreshing () {
			CheckBeesReward ();
			Invoke ("CheckBeesReward", delayRefreshTime);
		}

		private void StopRefreshing () {
			CancelInvoke ("CheckBeesReward");
		}

		//--------------------------------------
		//Handlers
		//--------------------------------------

		private void OnLoginConnectionSuccessfulHandler () {
			StartRefreshing ();
		}

		private void OnDisconnectedHandler () {
			StopRefreshing ();
		}

		private void OnAddBeesSuccessfulHandler (int bees, bool showNativePopup) {
			_beesEarnedToday += bees;
			_showNativePopup = showNativePopup;
			
			_lastRewardedDate = new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			PlayerPrefs.SetString (LAST_REWARDED_DATE_TOKEN, _lastRewardedDate.ToString ());
			PlayerPrefs.SetInt (BEES_REWARDED_IN_ADAY + BTS_PlayerData.Instance.Player.BtsID.ToString (), _beesEarnedToday);

			OnRewardSuccessful (bees, _showNativePopup);
			DispatchOnTimeCustomPopupShowing ();
		}

		private void OnAddBeesFailhandler (string error) {
			OnRewardFail (error);
			DispatchOnTimeCustomPopupShowing ();

			if (error.Contains ("Reached limit of daily bees") && _isDailyLimit == false) {
				_isDailyLimit = true;
				if (OnDailyBeesMaximumReached != null)
					OnDailyBeesMaximumReached ();
			}
		}
	}
}
