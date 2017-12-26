using UnityEngine;
using System;
using System.Collections;

namespace BTS {

	internal class BTS_PlayerData : SA_Singleton<BTS_PlayerData> {

		private const string AUTOLOGIN_TOKEN 		= "AUTOLOGIN_TOKEN_v1";
		private const string LOGIN_TOKEN 			= "LOGIN_TOKEN_v1";
		private const string AUTHTOKEN_TOKEN 		= "AUTHTOKEN_TOKEN_v1";
		private const string USERID_TOKEN 			= "USERID_TOKEN_v1";
		private const string VERY_FIRST_SESSION 	= "VERY_FIRST_SESSION";
		private const string NOTIFY_SCHEDULED 		= "NOTIFY_SCHEDULED";
		private const string NEW_LIFE_NOTIFY_ID 	= "NEW_LIFY_NOTIFY_ID";

		public static Action OnPlayerAvatarLoaded = delegate {};

		internal bool UseAntiCheatToolkitPrefs = false;

		private IPlayerDataLoader _DataLoader;
		private BTS_Player _player;

		private Sprite _avatarSprite;

		private bool _avatarLoading = false;
		private float _loadingStartTime = 0f;
		private float _timeout = 5f;

		//Stored login and pass
		ConnectionType _autoConnectType = ConnectionType.None;

		private static bool _isLoginStoring = false;// by default
		private string _storedLogin = string.Empty;
		private string _storedAuthToken = string.Empty;
		private int _verificationCode = 0;
		private int _storedUserID = 0;

		public string LastInputedEmailAddress = String.Empty;
		
		//--------------------------------------
		//Get/Set
		//--------------------------------------

		internal BTS_Player Player {
			get {
				return _player;
			}
		}

		internal Sprite PlayerAvatar {
			get {
				return _avatarSprite;
			}
		}

		internal ConnectionType AutoConnectType {
			get {
				return _autoConnectType;
			}
		}

		internal int NewLifeNotificationId {
			get { 
				if (!_DataLoader.HasKey (NEW_LIFE_NOTIFY_ID)) {
					return -1;
				} else {
					return _DataLoader.GetInt (NEW_LIFE_NOTIFY_ID);
				}
			}
			set {
				_DataLoader.SetInt (NEW_LIFE_NOTIFY_ID, value);
			}
		}

		internal bool IsNotifyScheduled {
			get {
				if (!_DataLoader.HasKey(NOTIFY_SCHEDULED)) {
					_DataLoader.SetString (NOTIFY_SCHEDULED, NOTIFY_SCHEDULED);
					return false;
				}
				return true;
			}
		}
		
		internal bool IsTheVeryFirstSession {
			get {
				if (!_DataLoader.HasKey(VERY_FIRST_SESSION)) {
					_DataLoader.SetString (VERY_FIRST_SESSION, VERY_FIRST_SESSION);
					return true;
				}
				return false;
			}
		}
		
		internal string StoredLogin {
			get {
				return _storedLogin;
			}
		}

		internal string StoredAuthToken {
			get {
				return _storedAuthToken;
			}
		}

		internal int UserID {
			get {
				if (_player.BtsID > 0)
					return _player.BtsID;

				if (_storedUserID > 0)
					return _storedUserID;

				return 0;
			}
		}

		public bool IsLoginStoring {
			get {
				return _isLoginStoring;
			}
		}

		public int VerificationCode {
			get {
				return _verificationCode;
			}
		}

		public UInt64 DateOffset {
			get {
				DateTime time = TimeZoneInfo.ConvertTimeToUtc(DateTime.UtcNow);
				TimeSpan offset = TimeZoneInfo.Utc.GetUtcOffset(time);
				UInt64 off = (UInt64)offset.TotalSeconds;
				return off;
			}
		}

		//--------------------------------------
		//Built-in UNITY functions
		//--------------------------------------

		void Awake() {
			DontDestroyOnLoad (gameObject);
			UseAntiCheatToolkitPrefs = false;
		}

		void Update () {
			if (_avatarLoading && (Time.realtimeSinceStartup - _loadingStartTime) >= _timeout) {
				_avatarLoading = false;

				AvatarLoadTimeOutHandler ();
			}
		}

		void OnEnable () {
			BTS_WebServerManager.OnGetUserInfoSuccessful 		+= OnGetUserInfoSuccessfulHandler;
			BTS_WebServerManager.OnLoginConnectionSuccessful 	+= OnLoginConnectionSuccessfulHandler;
			BTS_WebServerManager.OnResetAuthSuccessful 			+= OnResetAuthSuccessfulHandler;
			BTS_WebServerManager.OnAuthCodeSuccessful 			+= OnAuthCodeSuccessfulHandler;
		}

		void OnDisable () {
			BTS_WebServerManager.OnGetUserInfoSuccessful 		-= OnGetUserInfoSuccessfulHandler;
			BTS_WebServerManager.OnLoginConnectionSuccessful 	-= OnLoginConnectionSuccessfulHandler;
			BTS_WebServerManager.OnResetAuthSuccessful 			-= OnResetAuthSuccessfulHandler;
			BTS_WebServerManager.OnAuthCodeSuccessful 			-= OnAuthCodeSuccessfulHandler;
		}

		//--------------------------------------
		//Public functions
		//--------------------------------------

		internal void SaveLogin (string login, string authToken, bool rememberMe) {
			_autoConnectType = ConnectionType.LoginAndPassword;

			_DataLoader.SetString (LOGIN_TOKEN, login);
			_storedLogin = login;

			_isLoginStoring = rememberMe;
			if (_isLoginStoring && authToken.Length > 0) {
				_DataLoader.SetInt (AUTOLOGIN_TOKEN, 1);
				_DataLoader.SetString (AUTHTOKEN_TOKEN, authToken);
				_player.SavePlayerStats ();
			} else {
				_DataLoader.SetInt (AUTOLOGIN_TOKEN, 0);
			}
		}

		internal void SaveLogin(string login, bool rememberMe) {
			_autoConnectType = ConnectionType.LoginAndPassword;

			_DataLoader.SetString (LOGIN_TOKEN, login);
			_storedLogin = login;
			_isLoginStoring = rememberMe;
		}

		internal void SaveAuthToken (string newToken) {
			if (newToken.Length > 0) {
				_DataLoader.SetString (AUTHTOKEN_TOKEN, newToken);
			}
		}

		internal void ResetStoredLogin () {
			//if (_DataLoader.HasKey (LOGIN_TOKEN))
			//_DataLoader.SetString (LOGIN_TOKEN, "");
			if (_DataLoader.HasKey (AUTHTOKEN_TOKEN))
				_DataLoader.SetString (AUTHTOKEN_TOKEN, "");
			if (_DataLoader.HasKey (AUTOLOGIN_TOKEN))
				_DataLoader.SetInt (AUTOLOGIN_TOKEN, 0);

			_isLoginStoring = false;
			_autoConnectType = ConnectionType.None;
		}

		internal void Init() {
			if (UseAntiCheatToolkitPrefs) {
				Debug.Log("UseAntiCheatToolkitPrefs");
				_DataLoader = new AntiCheatToolkitPrefs() ;
			} else {
				_DataLoader = new ObfuscatedPrefs ();
			}
			_player = new BTS_Player (_DataLoader);

			if (_DataLoader.HasKey (AUTOLOGIN_TOKEN) && _DataLoader.GetInt (AUTOLOGIN_TOKEN) == 1) {

				if (_DataLoader.HasKey (AUTHTOKEN_TOKEN) && _DataLoader.GetString (AUTHTOKEN_TOKEN).Length > 0) {
					_autoConnectType = ConnectionType.LoginAndPassword;
					_storedAuthToken = _DataLoader.GetString (AUTHTOKEN_TOKEN);
				}
				_isLoginStoring = true;
			} else {
				_isLoginStoring = false;
			}

			if (_DataLoader.HasKey (LOGIN_TOKEN) && _DataLoader.GetString (LOGIN_TOKEN).Length > 0) {
				_storedLogin = _DataLoader.GetString (LOGIN_TOKEN);
			}
		}

		internal void SetAuthToken(string authToken, bool isStoring) {
			_storedAuthToken = authToken;
			if (isStoring) {
				_DataLoader.SetString (AUTHTOKEN_TOKEN, _storedAuthToken);
			}
		}
		
		internal void SetLogin (string login, bool isStoring) {
			_storedLogin = login;
			if (isStoring) {
				_DataLoader.SetString (LOGIN_TOKEN, _storedLogin);
			}
		}

		internal void SetUserID (int userID, bool isStoring) {
			_storedUserID = userID;
			if (isStoring) {
				_DataLoader.SetInt (USERID_TOKEN, _storedUserID);
			}
		}

		//--------------------------------------
		//Private functions
		//--------------------------------------

		private void AvatarLoadTimeOutHandler () {
			OnPlayerAvatarLoaded ();
		}

		//--------------------------------------
		//Handlers
		//--------------------------------------

		private void OnLoginConnectionSuccessfulHandler (BTS_Player player) {
			if (player.AvatarURL != null && player.AvatarURL != string.Empty) {
				StartCoroutine (LoadImageFromURL (player.AvatarURL));

				_loadingStartTime = Time.realtimeSinceStartup;
				_avatarLoading = true;
			} else {
				OnPlayerAvatarLoaded ();
			}
			SaveLogin(_storedLogin, _storedAuthToken, _isLoginStoring);
		}

		private void OnGetUserInfoSuccessfulHandler (BTS_Player player) {
			if (player.AvatarURL != null && player.AvatarURL != string.Empty) {
				StartCoroutine (LoadImageFromURL (player.AvatarURL));

				_loadingStartTime = Time.realtimeSinceStartup;
				_avatarLoading = true;
			} else {
				OnPlayerAvatarLoaded ();
			}
			SaveLogin (_storedLogin, _storedAuthToken, _isLoginStoring);
		}

		private void OnResetAuthSuccessfulHandler () {
			//SaveLogin (_storedLogin, _storedAuthToken, _isLoginStoring);
		}
		
		private void OnAuthCodeSuccessfulHandler () {
			SaveLogin (_storedLogin, _storedAuthToken, true);
		}

		//--------------------------------------
		//Coroutines
		//--------------------------------------

		private IEnumerator LoadImageFromURL (string url) {

			WWW www = new WWW(url);
			yield return www;

			try {
				Debug.Log ("Avatar url " + url);
				_avatarSprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
			} catch (Exception e) {
				Debug.Log ("Load image error: " + e.Message);
			}

			_avatarLoading = false;

			if (_avatarSprite != null)
				OnPlayerAvatarLoaded ();
		}
	}
}
