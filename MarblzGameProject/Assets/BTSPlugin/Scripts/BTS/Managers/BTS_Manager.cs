using UnityEngine;
using System;
using System.Collections;
using System.IO;
using BTS;
using SA.Analytics.Google;

public class BTS_Manager : SA_Singleton<BTS_Manager> {

	public static Action 					OnLoginConnectionSuccessful = delegate{};
	public static Action<string> 			OnLoginConnectionFail = delegate{};

	public static Action 					OnDisconnected = delegate{};

	public static Action                    OnGetEventsSuccessful = delegate {};
	public static Action                    OnGetEventsFail = delegate {};
	
	public static Action<int, bool>         OnEventSuccessful = delegate{};
	public static Action<string>            OnEventFail = delegate{};
	
	public static Action<int, bool> 		OnRewardEarned = delegate{};
	public static Action<string> 			OnRewardFail = delegate{};
	
	public static Action<string>            OnResetEmail = delegate {};

	public static Action 					OnDailyBeesMaximumReached = delegate{};

	private ConnectionState _connectionState = ConnectionState.None;
	private ConnectionType _connectionType = ConnectionType.None;

	private BTS_Player _player;

	private bool _isAutoConnect = false;

	private BTS_PopupController _popup;
	private BTS_InterstitialController _interstitial;
	private BTS_RegistrationController _registration;
	private BTS_EventSystemController _events;
	private BTS_PanelController _panel;
	private BTS_InviteController _invite;

	//--------------------------------------
	//Get/Set
	//--------------------------------------

	public BTS_Player Player {
		get {
			return _player;
		}
	}

	public ConnectionState State {
		get {
			return _connectionState;
		}
	}

	public int BeesEarnedToday {
		get {
			return BTS_RewardManager.Instance.BeesEarnedToday;
		}
	}
	
	public bool IsConnected {
		get {
			return (_connectionState == ConnectionState.Connected);
		}
	}
	
	internal BTS_PopupController Popup {
		get {
			return _popup;
		}
	}

	//--------------------------------------
	//UNITY functions
	//--------------------------------------

	void Awake () {
		DontDestroyOnLoad (this.gameObject);
		BTS_PlayerData.Instance.Init ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.K)) {
			Reward (10);
		}
	}

	void OnEnable () {
		Subscribe ();
	}

	void OnDisable () {
		Unsubscribe ();

		//Disconnect ();
	}

	//--------------------------------------
	//Public functions
	//--------------------------------------

	public void Init() {
		Debug.Log ("[BTS Manager Initialized]");
		InstantiatePopup ();

		if (BTS_PlayerData.Instance.IsLoginStoring) {
			Connect();
		}
	}

	public void Connect (Action interstitialClosedCallback = null) {
		if (_connectionState == ConnectionState.Connected)
			return;

		if (Application.internetReachability == NetworkReachability.NotReachable) {
			Debug.LogWarning ("Network is not reachable! Check the internet connection.");
			return;
		}

		InstantiatePopup ();
		
		TryAutoConnect ();

		if (_isAutoConnect == false) {
			if(interstitialClosedCallback != null) {
				BTS_InterstitialController.OnClosed += interstitialClosedCallback;
			}

			ShowInterstitial ();
		}
	}

	public void Disconnect () {
		if (_connectionState == ConnectionState.Disconnected)
			return;

		_connectionState = ConnectionState.Disconnected;

		BTS_PlayerData.Instance.ResetStoredLogin ();

		if (OnDisconnected != null)
			OnDisconnected ();
	}

	public void GetUserUnfo() {
		new BTS_GetUserInfo (BTS_PlayerData.Instance.UserID).Send ();
	}

	public void Reward (int bees) {
		if (_connectionState != ConnectionState.Connected)
			return;

		BTS_RewardManager.Instance.Reward (bees);
	}

	public void RewardEvent(int level, int score) {
		if (_connectionState != ConnectionState.Connected)
			return;

		BTS_RewardManager.Instance.RewardEvent(level, score);
	}

	public void GetEvents() {
		if (_connectionState != ConnectionState.Connected)
			return;
		
		new BTS_GetEvents().Send();
	}

	public void DisableAutoLogin () {
		BTS_PlayerData.Instance.ResetStoredLogin ();
	}

	//--------------------------------------
	//Private functions
	//--------------------------------------

	internal void Connect (string login, string password) {
		Debug.Log (_connectionState);
		if (_connectionState == ConnectionState.Connecting
			|| _connectionState == ConnectionState.Connected)
			return;

		_connectionState = ConnectionState.Connecting;
		_connectionType = ConnectionType.LoginAndPassword;

		InstantiatePopup ();

		BTS_ConnectionController.Instance.Connect (login, password);
	}

	private void TryAutoConnect () {
		ConnectionType autoConnectType = BTS_PlayerData.Instance.AutoConnectType;

		switch (autoConnectType) {

		case ConnectionType.None:
			_isAutoConnect = false;
			break;

		case ConnectionType.LoginAndPassword:
			InstantiatePopup ();

			int userID = BTS_PlayerData.Instance.UserID;

			if (userID != -1) {
				_isAutoConnect = true;

				BTS_ConnectionController.OnGetUserInfoSuccessful += OnGetUserInfoSuccessfulHandler;
				BTS_ConnectionController.OnGetUserInfoFail += OnGetUserInfoFailHandler;
				BTS_ConnectionController.Instance.GetUserInfo (userID);
			} else {
				_isAutoConnect = false;
			}
			break;
		}
	}

	private void InstantiatePopup () {
		if (_popup == null) {
			string prefabName = "";

			if (Screen.width > Screen.height) {
				prefabName = BTS.Constants.POPUP_PREFAB_NAME_HORIZONTAL;
			} else {
				prefabName = BTS.Constants.POPUP_PREFAB_NAME_VERTICAL;
			}

			GameObject prefab = Resources.Load (prefabName) as GameObject;

			if (prefab != null) {
				GameObject popupGO = Instantiate (prefab);
				_popup = popupGO.GetComponent <BTS_PopupController> ();
			} else {
				Debug.LogError ("Cant find " + prefabName + ". Check the prefab in project and in Resources folder.");
			}
		}
	}

	public void ShowInterstitial () {
		if (_interstitial == null) {
			string prefabName = String.Empty;

			if (Screen.width > Screen.height) {
				prefabName = BTS.Constants.INTERSTITIAL_PREFAB_NAME_HORIZONTAL;
			} else {
				prefabName = BTS.Constants.INTERSTITIAL_PREFAB_NAME_VERTICAL;
			}

			GameObject prefab = Resources.Load (prefabName) as GameObject;

			if (prefab != null) {
				GameObject interstitialGO = Instantiate (prefab) as GameObject;
				_interstitial = interstitialGO.GetComponent <BTS_InterstitialController> ();
			}
		} else {
			_interstitial.Show ();
		}
	}

	internal void ShowRegistration () {
		if (_registration == null) {
			string prefabName = String.Empty;

			if (Screen.width > Screen.height) {
				prefabName = BTS.Constants.REGISTRATION_PREFAB_NAME_HORIZONTAL;
			} else {
				prefabName = BTS.Constants.REGISTRATION_PREFAB_NAME_VERTICAL;
			}

			GameObject prefab = Resources.Load (prefabName) as GameObject;

			if (prefab != null) {
				GameObject registrationGO = Instantiate (prefab) as GameObject;
				_registration = registrationGO.GetComponent <BTS_RegistrationController> ();
			}
		}

		_registration.ShowRegistration ();
	}

	internal void ShowEventSystemInterstitial() {
		if (_events == null) {
			string prefabName = String.Empty;

				prefabName = BTS.Constants.EVENTSYSTEM_PREFAB_NAME_VERTICAL;

			GameObject prefab = Resources.Load (prefabName) as GameObject;

			if (prefab != null) {
				GameObject eventsGO = Instantiate (prefab) as GameObject;
				_events = eventsGO.GetComponent <BTS_EventSystemController> ();
			}
		}

		_events.Show();
	}

	internal void ShowPanel() {
		if (_panel == null) {
			string prefabName = String.Empty;

			if (Screen.width > Screen.height) {
				prefabName = BTS.Constants.PANEL_PREFAB_NAME_HORIZONTAL;
			} else {
				prefabName = BTS.Constants.PANEL_PREFAB_NAME_VERTICAL;
			}
			GameObject prefab = Resources.Load (prefabName) as GameObject;
			if (prefab != null) {
				GameObject eventsGO = Instantiate (prefab) as GameObject;
				_panel = eventsGO.GetComponent <BTS_PanelController> ();
			}
		}
		_panel.Show();
	}
	
	internal void ShowInvite() {
    		if (_invite == null) {
    			string prefabName = String.Empty;
    
    			if (Screen.width > Screen.height) {
    				prefabName = BTS.Constants.INVITE_PREFAB_NAME_VERTICAL;
    			} else {
    				prefabName = BTS.Constants.INVITE_PREFAB_NAME_VERTICAL;
    			}
    			GameObject prefab = Resources.Load (prefabName) as GameObject;
    			if (prefab != null) {
    				GameObject inviteGO = Instantiate (prefab) as GameObject;
    				_invite = inviteGO.GetComponent <BTS_InviteController> ();
    			}
    		}
    		_invite.Show();
    }

	internal void ShowBTSStatus() {
		if (_popup != null) {
			_popup.ShowCurStats();
		}
	}

	private void Subscribe () {
		BTS_WebServerManager.OnLoginConnectionSuccessful    += OnLoginConnectionSuccessfullHandler;
		BTS_WebServerManager.OnLoginConnectionFail          += OnLoginConnectionFailHandler;
		BTS_WebServerManager.OnRegistrationSuccessful 		+= OnRegistrationSuccessfulHandler;
		BTS_WebServerManager.OnRegistrationFail 			+= OnRegistrationFailHandler;
		BTS_WebServerManager.OnAuthCodeSuccessful 			+= OnAuthCodeSuccessfulHandler;
		BTS_WebServerManager.OnAuthCodeFail 				+= OnAuthCodeFailHandler;
		BTS_WebServerManager.OnGetEventsSuccessful          += OnGetEventsSuccessfullHandler;
		BTS_WebServerManager.OnGetEventsFail                += OnGetEventsFailHandler;
		BTS_WebServerManager.OnRewardEventSuccessful        += OnEventSuccessfulHandler;
		BTS_WebServerManager.OnRewardEventFail              += OnEventFailHandler;

		BTS_RewardManager.OnRewardSuccessful 				+= OnRewardSuccessfulHandler;
		BTS_RewardManager.OnRewardFail 						+= OnRewardFailHandler;

		BTS_RewardManager.OnDailyBeesMaximumReached 		+= OnDailyBeesMaximumReachedHandler;
	}

	private void Unsubscribe () {
		BTS_WebServerManager.OnLoginConnectionSuccessful    -= OnLoginConnectionSuccessfullHandler;
		BTS_WebServerManager.OnLoginConnectionFail          -= OnLoginConnectionFailHandler;
		BTS_WebServerManager.OnRegistrationSuccessful 		-= OnRegistrationSuccessfulHandler;
		BTS_WebServerManager.OnRegistrationFail 			-= OnRegistrationFailHandler;
		BTS_WebServerManager.OnAuthCodeSuccessful 			-= OnAuthCodeSuccessfulHandler;
		BTS_WebServerManager.OnAuthCodeFail 				-= OnAuthCodeFailHandler;
		BTS_WebServerManager.OnGetEventsSuccessful          -= OnGetEventsSuccessfullHandler;
		BTS_WebServerManager.OnGetEventsFail                -= OnGetEventsFailHandler;
		BTS_WebServerManager.OnRewardEventSuccessful        -= OnEventSuccessfulHandler;
		BTS_WebServerManager.OnRewardEventFail              -= OnEventFailHandler;
		
		BTS_RewardManager.OnRewardSuccessful 				-= OnRewardSuccessfulHandler;
		BTS_RewardManager.OnRewardFail 						-= OnRewardFailHandler;

		BTS_RewardManager.OnDailyBeesMaximumReached 		-= OnDailyBeesMaximumReachedHandler;
	}

	//--------------------------------------
	//Handlers
	//--------------------------------------

	private void OnLoginConnectionSuccessfullHandler (BTS_Player player) {
		Debug.Log ("OnLoginConnectionSuccessfullHandler");
		_connectionState = ConnectionState.Connected;

		BTS_RewardManager.Instance.Init ();

		BTS_ConnectionController.OnLoginConnectionSuccessful -= OnLoginConnectionSuccessfullHandler;
		BTS_ConnectionController.OnLoginConnectionFail -= OnLoginConnectionFailHandler;

		_player = player;

		if (OnLoginConnectionSuccessful != null)
			OnLoginConnectionSuccessful ();

	}

	private void OnLoginConnectionFailHandler (string error) {
		Debug.Log ("OnLoginConnectionFailHandler");

		Disconnect ();

		BTS_ConnectionController.OnLoginConnectionSuccessful -= OnLoginConnectionSuccessfullHandler;
		BTS_ConnectionController.OnLoginConnectionFail -= OnLoginConnectionFailHandler;

		Debug.Log ("BTS ConnectionFail " + error);

		if (error.Contains ("Account is unconfirmed")) {
			ShowRegistration ();
			_registration.ShowVerificationScreen ();
		}

		if (OnLoginConnectionFail != null)
			OnLoginConnectionFail (error);

	}

	private void OnGetUserInfoSuccessfulHandler (BTS_Player player) {
		_connectionState = ConnectionState.Connected;

		Debug.Log ("OnGetUserInfoSuccessfulHandler");

		BTS_RewardManager.Instance.Init ();

		BTS_ConnectionController.OnGetUserInfoSuccessful -= OnGetUserInfoSuccessfulHandler;
		BTS_ConnectionController.OnGetUserInfoFail -= OnGetUserInfoFailHandler;

		if (OnLoginConnectionSuccessful != null)
			OnLoginConnectionSuccessful ();
	}

	private void OnGetUserInfoFailHandler (string error) {
		Debug.Log ("OnGetUserInfoFailHandler");

		BTS_ConnectionController.OnGetUserInfoSuccessful -= OnGetUserInfoSuccessfulHandler;
		BTS_ConnectionController.OnGetUserInfoFail -= OnGetUserInfoFailHandler;

		Disconnect ();
	}

	private void OnRegistrationSuccessfulHandler (BTS_Player player) {
		Debug.Log ("OnRegistrationSuccessfulHandler");

	}

	private void OnRegistrationFailHandler (string error) {
		Debug.Log ("OnRegistrationFailHandler");

	}

	private void OnAuthCodeSuccessfulHandler () {
		Debug.Log ("OnAuthCodeSuccessfulHandler");

		if (OnLoginConnectionSuccessful != null)
			OnLoginConnectionSuccessful();

	}

	private void OnAuthCodeFailHandler (string error) {
		Debug.Log ("OnAuthCodeFailHandler");
	}
	
	private void OnGetEventsSuccessfullHandler() {
		Debug.Log ("OnGetEventsSuccessfulHandler");

		if (_connectionState != ConnectionState.Connected)
			return;
		if (OnGetEventsSuccessful != null)
			OnGetEventsSuccessful();
	}

	private void OnGetEventsFailHandler(string error) {
		if (_connectionState != ConnectionState.Connected)
			return;
		Debug.Log ("OnGetEventsFailHandler");
	}

	private void OnEventSuccessfulHandler(int reward, bool showNativeUI) {
		if (_connectionState != ConnectionState.Connected)
			return;
		Debug.Log ("OnEventSuccessfulHandler");

		GetEvents();
		if (OnEventSuccessful != null)
			OnEventSuccessful(reward, false);
	}

	private void OnEventFailHandler(string error) {
		if (_connectionState != ConnectionState.Connected)
			return;
		Debug.Log ("OnEventFailHandler");
		
		if (OnEventFail != null)
			OnEventFail(error);
	}

	private void OnRewardSuccessfulHandler (int bees, bool showNativeUI) {
		Debug.Log ("OnBeesRewardSuccessfulHandler");

		if (_connectionState != ConnectionState.Connected)
			return;

		if (OnRewardEarned != null)
			OnRewardEarned (bees, showNativeUI);
	}

	private void OnRewardFailHandler (string error) {
		Debug.Log ("OnBeesRewardFailHandler");

		if (_connectionState != ConnectionState.Connected)
			return;

		if (OnRewardFail != null)
			OnRewardFail (error);
	}

	private void OnDailyBeesMaximumReachedHandler () {
		Debug.Log ("OnDailyBeesMaximumReachedHandler");

		if (OnDailyBeesMaximumReached != null)
			OnDailyBeesMaximumReached ();
	}
}
