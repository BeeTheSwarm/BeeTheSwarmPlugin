using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace BTS {

	internal enum InternetConnectionState {
		Undefined,
		Online,
		Offline
	}

	internal enum AuthTokenState {
		Undefined,
		Updating,
		Valid,
		Expired
	}

	internal class BTS_WebServerManager : NonMonoSingleton<BTS_WebServerManager> {

		public static event Action<BTS_Player> 	OnLoginConnectionSuccessful 		= delegate{};
		public static event Action<string> 		OnLoginConnectionFail 				= delegate{};

		public static event Action<BTS_Player> 	OnRegistrationSuccessful 			= delegate{};
		public static event Action<string> 		OnRegistrationFail 					= delegate{};
		
		public static event Action	 			OnAuthCodeSuccessful 				= delegate{};
		public static event Action<string> 		OnAuthCodeFail 						= delegate{};

		public static event Action<int, bool>   OnAddBeesSuccessful 			    = delegate{};
		public static event Action<string> 		OnAddBeesFail 						= delegate{};
		
		public static event Action<BTS_Player>	OnGetUserInfoSuccessful 			= delegate{};
		public static event Action<string>		OnGetUserInfoFail 					= delegate{};
		
		public static event Action              OnGetEventsSuccessful             	= delegate{};
		public static event Action<string>      OnGetEventsFail                     = delegate{};

		public static event Action<int, bool>   OnRewardEventSuccessful             = delegate{};
		public static event Action<string>      OnRewardEventFail                   = delegate{};
		
		public static event Action 				OnResetAuthSuccessful 				= delegate{};
		public static event Action<string> 		OnResetAuthFail 					= delegate{};

		public static event Action<string> 		OnResendCodeSuccessful 				= delegate{};
		public static event Action<string> 		OnResendCodeFail 					= delegate{};

		public static event Action              OnResetEmailSuccessful              = delegate{};
		public static event Action<string>      OnResetEmailFail                    = delegate{};

		public static event Action              OnResetPasswordSuccessful           = delegate{};
		public static event Action<string>      OnResetPasswordFail                 = delegate{};
		
		public static event Action              OnReassignEmailSuccessful           = delegate{};
		public static event Action<string>      OnReassignEmailFail                 = delegate{};
		
		public static event Action              OnReassignPasswordSuccessful        = delegate{};
		public static event Action<string>      OnReassignPasswordFail              = delegate{};
		
		public static event Action<string> 		OnRequestTimeOut 					= delegate{};

		private const int TOTAL_SERVICES_COUNT = 2;

		private static bool _IsInitialized = false;
		private static int InitializedServicesCount = 0;

		public static event Action<string> PackageReceived = delegate {};
		public static event Action<CampaignInfo> OnCampaignInfoLoaded = delegate {};

		private static AuthTokenState _authTokenState;
		private static List<WS_RequestResult> _delayedRequests = new List<WS_RequestResult> ();

		public void Init() {
			if(_IsInitialized ) {return;}

			_IsInitialized = true;
		}

		//--------------------------------------
		// Get / Set
		//--------------------------------------

		public static bool IsInitialized {
			get {
				return _IsInitialized;
			}
		}

		//--------------------------------------
		// Built-in UNITY functions
		//--------------------------------------


		//--------------------------------------
		// Responce Handler
		//--------------------------------------

		public static void HandlePackageError(WS_RequestResult RequestResult) { 
			if (!String.IsNullOrEmpty(RequestResult.www.text)) {
				BTS_BaseServerPackage pack = new BTS_BaseServerPackage (RequestResult.www.text);

				if (pack.RawResponce.Contains ("211")) {
					BTS_BasePackage package = RequestResult.Package;
					_delayedRequests.Add (RequestResult);

					if (_authTokenState != AuthTokenState.Updating) {
						string authToken = BTS_PlayerData.Instance.StoredAuthToken;
						_authTokenState = AuthTokenState.Updating;
						BTS_ConnectionController.Instance.ResetAuth (authToken);
					}

				} else if (pack.RawResponce.Contains("210")) {
					BTS_PlayerData.Instance.ResetStoredLogin();
					BTS_Manager.Instance.Connect();
				} else if (pack.RawResponce.Contains("201")) {
					OnLoginConnectionFail("Sorry! Log In Failed.");
				} else if (pack.RawResponce.Contains("202")) {
					OnLoginConnectionFail("Sorry! Log In Failed.");
					
				} else if (pack.RawResponce.Contains("203")) {
					OnRegistrationFail("User with this login already exist.");
					
				} else if (pack.RawResponce.Contains("204")){
					OnRegistrationFail("Verification code is invalid.");
					
				} else if (pack.RawResponce.Contains("205")) {
					switch (RequestResult.Package.Id) {
						case BTS_AuthDirect.PackId:
							OnLoginConnectionFail ("Acount is unconfirmed.");
							break;	
						case BTS_Register.PackId:
							OnRegistrationFail("Acount is unconfirmed.");
							break;
					}
				} else if (pack.RawResponce.Contains("206")) {
					OnLoginConnectionFail("User with that login is unregistered.");
					
				} else
					switch (RequestResult.Package.Id) {
					case BTS_Register.PackId:
						OnRegistrationFail (pack.Error.Describtion);
						break;

					case BTS_AuthCode.PackId:
						OnAuthCodeFail (pack.Error.Describtion);
						break;
						
					case BTS_ResendCode.PackId:
						OnResendCodeFail (pack.Error.Describtion);
						break;

					case BTS_AuthDirect.PackId:
						OnLoginConnectionFail (pack.Error.Describtion);
						break;

					case BTS_GetUserInfo.PackId:
						OnGetUserInfoFail (pack.Error.Describtion);
						break;

					case BTS_GetEvents.PackId:
						OnGetEventsFail(pack.Error.Describtion);
						break;
					
					case BTS_Event.PackId:
						OnRewardEventFail(pack.Error.Describtion);
						break;
						
					case BTS_AddBees.PackId:
						OnAddBeesFail(pack.Error.Describtion);
						break;
							
					case BTS_ResetAuth.PackId:
						OnResetAuthFail (pack.Error.Describtion);
						OnLoginConnectionFail (pack.Error.Describtion);
						break;
						
					case BTS_ResetEmail.PackId:
						OnResetEmailFail(pack.Error.Describtion);
						break;
						
					case BTS_ResetPassword.PackId:
						OnResetPasswordFail(pack.Error.Describtion);
						break;
						
					case BTS_ReassignEmail.PackId:
						OnReassignEmailFail(pack.Error.Describtion);
						break;
						
					case BTS_ReassignPassword.PackId:
						OnReassignPasswordFail(pack.Error.Describtion);
						break;
					}
				
			} else {
				OnLoginConnectionFail (RequestResult.www.error);
			}

			Debug.LogError(BTS_WebServer.WEBSERVER_LOG_HEADER + "Package " + RequestResult.Package.Id + " Failed" + RequestResult.www.error);

			PackageReceived(RequestResult.Package.Id);

			if (RequestResult.Status == BTS_RequestStatus.Timeout) {
				OnRequestTimeOut (RequestResult.Package.Id);
			}
		}

		public static void HandleResponce(BTS_BaseServerPackage pack) {
			switch(pack.Id) {
				
			case BTS_Register.PackId:
				BTS_PlayerData.Instance.SetUserID (pack.GetDataField<int> ("user_id"), true);
				BTS_PlayerData.Instance.SetAuthToken (pack.GetDataField<string> ("auth_token"), true);
				OnRegistrationSuccessful (BTS_PlayerData.Instance.Player);
				break;

			case BTS_AuthCode.PackId:
				Dictionary <string, System.Object> userD = pack.GetDataField<Dictionary <string, System.Object>> ("user");
				BTS_PlayerData.Instance.Player.MergeStats (
					int.Parse (userD["id"].ToString()),
					userD["name"].ToString(),
					userD["email"].ToString(),
					int.Parse (userD["bees"].ToString()),
					int.Parse (userD["level"].ToString()),
					int.Parse (userD["progress"].ToString()),
					userD["ref"].ToString()					
				);
				BTS_PlayerData.Instance.SetUserID (int.Parse (userD["id"].ToString ()), true);
				BTS_PlayerData.Instance.SetAuthToken (pack.GetDataField<string> ("auth_token"), true);
				
				OnAuthCodeSuccessful ();

				BTS_Manager.Instance.Connect ();
				new BTS_GetUserInfo (BTS_PlayerData.Instance.UserID).Send ();
				break;

			case BTS_ResendCode.PackId:
				BTS_PlayerData.Instance.SetUserID (pack.GetDataField<int> ("user_id"), false);
				BTS_PlayerData.Instance.SetAuthToken (pack.GetDataField<string> ("auth_token"), false);
				
				OnResendCodeSuccessful ("We send verification code in your email");
				break;	
				
			case BTS_ResetEmail.PackId:
				BTS_PlayerData.Instance.SetUserID (pack.GetDataField<int> ("user_id"), false);
				BTS_PlayerData.Instance.SetAuthToken (pack.GetDataField<string> ("auth_token"), false);

				OnResetEmailSuccessful();
				break;
				
			case BTS_ResetPassword.PackId:
				BTS_PlayerData.Instance.SetUserID (pack.GetDataField<int> ("user_id"), false);
				BTS_PlayerData.Instance.SetAuthToken (pack.GetDataField<string> ("auth_token"), false);

				OnResetPasswordSuccessful();
				break;
				
			case BTS_ReassignEmail.PackId:
				OnReassignEmailSuccessful();
				break;
				
			case BTS_ReassignPassword.PackId:
				OnReassignPasswordSuccessful();
				break;
				
			case BTS_AuthDirect.PackId:
				Dictionary <string, System.Object> userDict = pack.GetDataField<Dictionary <string, System.Object>> ("user");
				BTS_PlayerData.Instance.Player.MergeStats (	
					int.Parse (userDict["id"].ToString()),
					userDict["name"].ToString(),
					userDict["email"].ToString(),
					int.Parse (userDict["bees"].ToString()),
					int.Parse (userDict["level"].ToString()),
					int.Parse (userDict["progress"].ToString()),
					userDict["ref"].ToString()				
				);
				BTS_PlayerData.Instance.SetUserID(int.Parse (userDict["id"].ToString ()), true);
				BTS_PlayerData.Instance.SetAuthToken (pack.GetDataField<string> ("auth_token"), true);
				try {
					string image_url = pack.GetDataField<string> ("image_url");
					string imageName = userDict ["avatar"].ToString ();
					if (imageName != null && imageName != String.Empty) {
						string avatarURL = image_url + imageName;
						BTS_PlayerData.Instance.Player.SetAvatarURL (avatarURL);
					}
				} catch (NullReferenceException e) {
					Debug.Log (e.Message);
				}
				
				OnLoginConnectionSuccessful (BTS_PlayerData.Instance.Player);
				break;

			case BTS_GetUserInfo.PackId:
				Dictionary <string, System.Object> dict = pack.GetDataField<Dictionary <string, System.Object>> ("user");
				BTS_PlayerData.Instance.Player.MergeStats (
					int.Parse (dict["id"].ToString()),
					dict["name"].ToString(),
					dict["email"].ToString(),
					int.Parse (dict["bees"].ToString()),
					int.Parse (dict["level"].ToString()),
					int.Parse (dict["progress"].ToString()),
					dict["ref"].ToString()	
				);

				OnGetUserInfoSuccessful (BTS_PlayerData.Instance.Player);
				break;

			case  BTS_GetEvents.PackId:
				BTS_GetEvent_Data btsData = JsonUtility.FromJson<BTS_GetEvent_Data>(Json.Serialize(pack.Data));
				BTS_PlayerData.Instance.Player.UpdateBTSStatsEvent(btsData);

				OnGetEventsSuccessful();
				break;
				
			case BTS_Event.PackId:
				Dictionary <string, System.Object> Udict = pack.GetDataField<Dictionary <string, System.Object>> ("user");
				int reward = pack.GetDataField<int>("reward");
				
				BTS_PlayerData.Instance.Player.MergeStats (
					int.Parse (Udict["id"].ToString()),
					Udict["name"].ToString(),
					Udict["email"].ToString(),
					int.Parse (Udict["bees"].ToString()),
					int.Parse (Udict["level"].ToString()),
					int.Parse (Udict["progress"].ToString()),
					pack.GetDataField<int>("reward")
				);
				if (reward > 0)
					OnRewardEventSuccessful(reward, true);
				else 
					OnRewardEventSuccessful(reward, false);
				break;
				
			case BTS_AddBees.PackId:
				Dictionary <string, System.Object> Userdict = pack.GetDataField<Dictionary <string, System.Object>> ("user");
				int bees = int.Parse (Userdict["bees"].ToString());
				BTS_PlayerData.Instance.Player.UpdateStats(
					int.Parse (Userdict["bees"].ToString()),
					int.Parse (Userdict["level"].ToString()),
					int.Parse (Userdict["progress"].ToString())
				); 
				break;
			
			case BTS_ResetAuth.PackId:
				string newAuthToken = pack.GetDataField<string> ("auth_token");
				BTS_PlayerData.Instance.SetAuthToken (newAuthToken, true);
				_authTokenState = AuthTokenState.Valid;
				OnResetAuthSuccessful ();

				for (int p = 0; p < _delayedRequests.Count; p++) {
					switch (_delayedRequests[p].Package.Id) {

						case BTS_AuthCode.PackId:
							new BTS_AuthCode(BTS_PlayerData.Instance.UserID, BTS_PlayerData.Instance.VerificationCode).Send();
							break;

						case BTS_GetUserInfo.PackId:
							new BTS_GetUserInfo(BTS_PlayerData.Instance.UserID).Send();
							break;

						case BTS_GetEvents.PackId:
							new BTS_GetEvents().Send();
							break;

						case BTS_Event.PackId:
							BTS_Event rewardEvent = (BTS_Event) _delayedRequests[p].Package;
							new BTS_Event(rewardEvent.events, rewardEvent.score).Send();
							break;

						case BTS_AddBees.PackId:
							BTS_AddBees addBees = (BTS_AddBees) _delayedRequests[p].Package;
							new BTS_AddBees(addBees.count).Send();
							break;
					}

					_delayedRequests.RemoveAt (p);
					p--;
				}
				break;
			}

			PackageReceived(pack.Id);
		}

		public static bool CheckForInternetConnection() {
			return false;
		}
	}
}
