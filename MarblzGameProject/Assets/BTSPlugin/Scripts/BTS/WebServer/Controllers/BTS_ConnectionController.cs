using UnityEngine;
using System;
using System.Collections;

namespace BTS {

	internal class BTS_ConnectionController : SA_Singleton<BTS_ConnectionController> {

		
		public static Action<BTS_Player> 		OnLoginConnectionSuccessful = delegate {};
		public static Action<string> 			OnLoginConnectionFail       = delegate {};

		public static Action<BTS_Player> 		OnGetUserInfoSuccessful     = delegate {};
		public static Action<string> 			OnGetUserInfoFail           = delegate {};
		
		public static Action                    OnPlayerLoginConnection     = delegate {};

		//--------------------------------------
		//Built-in UNITY functions
		//--------------------------------------

		void Awake () {
			DontDestroyOnLoad (gameObject);

			BTS_WebServerManager.Instance.Init ();
		}

		//--------------------------------------
		//Public functions
		//--------------------------------------

		internal void Connect (string login, string password) {

			BTS_WebServerManager.OnLoginConnectionSuccessful += OnLoginConnectionSuccessfulHandler;
			BTS_WebServerManager.OnLoginConnectionFail += OnLoginConnectionFailHandler;

			new BTS_AuthDirect (login, password).Send ();
		}

		internal void GetUserInfo (int userID) {
			
			BTS_WebServerManager.OnGetUserInfoSuccessful += OnGetUserInfoSuccessfulHandler;
			BTS_WebServerManager.OnGetUserInfoFail += OnGetUserInfoFailHandler;

			new BTS_GetUserInfo (userID).Send ();
		}

		internal void ResetAuth (string authToken) {
			new BTS_ResetAuth (authToken).Send ();
		}

		//--------------------------------------
		//Private functions
		//--------------------------------------

		//--------------------------------------
		//Handlers
		//--------------------------------------

		void OnLoginConnectionSuccessfulHandler (BTS_Player player) {
			BTS_WebServerManager.OnLoginConnectionSuccessful -= OnLoginConnectionSuccessfulHandler;
			BTS_WebServerManager.OnLoginConnectionFail -= OnLoginConnectionFailHandler;

			Debug.Log ("OnBTSPlayerConnected!");

			OnLoginConnectionSuccessful (player);
			OnPlayerLoginConnection();
		}

		void OnLoginConnectionFailHandler (string error) {
			BTS_WebServerManager.OnLoginConnectionSuccessful -= OnLoginConnectionSuccessfulHandler;
			BTS_WebServerManager.OnLoginConnectionFail -= OnLoginConnectionFailHandler;

			Debug.Log ("OnBTSPlayerDisconnected!");

			OnLoginConnectionFail (error);
		}

		void OnGetUserInfoSuccessfulHandler (BTS_Player player) {
			BTS_WebServerManager.OnGetUserInfoSuccessful -= OnGetUserInfoSuccessfulHandler;
			BTS_WebServerManager.OnGetUserInfoFail -= OnGetUserInfoFailHandler;

			OnGetUserInfoSuccessful (player);
		}

		void OnGetUserInfoFailHandler (string error) {
			BTS_WebServerManager.OnGetUserInfoSuccessful -= OnGetUserInfoSuccessfulHandler;
			BTS_WebServerManager.OnGetUserInfoFail -= OnGetUserInfoFailHandler;

			OnGetUserInfoFail (error);
		}
	}
}
