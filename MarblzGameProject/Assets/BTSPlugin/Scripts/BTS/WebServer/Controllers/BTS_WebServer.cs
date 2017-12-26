using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace BTS {

internal class BTS_WebServer : SA_Singleton<BTS_WebServer> {

	public const string WEBSERVER_LOG_HEADER = "ST_WebServer Log: ";
	public const string SECRET = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDDzooiCyBcgbuNFeVCCF0/5dma";

	private const string WEBSERVER_VERSION = "1.0";
	private string SERVER_URL;

	private const string WEBSERVER_ERROR_HEADER = "ST_WebServer Error: ";
	private const long  WEBSERVER_ALLOWED_REQUEST_TIME_RANGE = 900000;


	private static List<BTS_BasePackage> DelayedPackages =  new List<BTS_BasePackage>();

	private static bool _IsOfflineMode = false;

	private static string _RequestUrl = string.Empty;
	private static string _GameID;

	void Awake() {

		DontDestroyOnLoad(gameObject);
		
		SERVER_URL = BTS_Settings.GetServerUrl ();
		_GameID = BTS_Settings.GetGameID ();

		_RequestUrl = "https://" +  SERVER_URL;
	}

	//--------------------------------------
	// Public Methods
	//--------------------------------------



	public void Send(BTS_BasePackage package) {

		if(_IsOfflineMode) {
			Debug.Log("Request Denaied due to offline mode");
		}

		if(SessionManager.SessionUndefined || _IsOfflineMode) {
			if(package.AuthenticationRequired) {
				DelayedPackages.Add(package);
				Debug.LogWarning(package.Id +  " Caсhed");
				return;
			}
		}
		
		SendRequest(package);
	}

	public void SendDelayedPackages() {
		Debug.Log("SendDelayedPackages" + DelayedPackages.Count);
		foreach(BTS_BasePackage p in DelayedPackages) {
			SendRequest(p);
		}
	}


	//--------------------------------------
	// Get / Set
	//--------------------------------------

	public static Int32 CurrentTimeStamp {
		get {
			return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
		}
	}

	public static bool IsOfflineMode {
		get {
			return _IsOfflineMode;
		}
	}

	public static string GameID {
		get {
			return _GameID;
		}
	}


	//--------------------------------------
	// Private Methods
	//--------------------------------------

	private void SendRequest(BTS_BasePackage package) {

		BTS_ServerRequest request =  BTS_ServerRequest.Create();
		request.RequestCompleted += HandleRequestCompleted;
		request.Send(package, _RequestUrl);
		
	}


	void HandleRequestCompleted (WS_RequestResult result) {

		if(result.Status == BTS_RequestStatus.Completed) {

				Dictionary<string, object> resp = Json.Deserialize(result.www.text) as Dictionary<string, object>;

				string ResponceHash = result.www.responseHeaders["SIQ"]; 
				string ClientHash = HMAC(SECRET, result.www.text); 
				
				
				
				if(!ClientHash.Equals(ResponceHash)) {
					Debug.Log(WEBSERVER_ERROR_HEADER + "Hash Validation FAILED.");
					ValidationFailed(result);
					return;
				} else {
					Debug.Log("Hash validation ok");
				}
			
				BTS_BaseServerPackage pack =  new BTS_BaseServerPackage(result.www.text);
				if(pack.Error ==  null) {
					BTS_WebServerManager.HandleResponce(pack);
				} else {
					if (pack.Error.Describtion.Contains ("User does not exists")) {
						;
					}

					if (pack.Error.Describtion.Contains ("Acount is unconfirmed")) {
						;
					}
					BTS_WebServerManager.HandlePackageError(result);
				}
				
		} else {
			if(result.Status == BTS_RequestStatus.Timeout) {
				Debug.Log(WEBSERVER_ERROR_HEADER + "Request Failed by Timeout ");
			} else {
				Debug.Log(WEBSERVER_ERROR_HEADER + "Request Failed with error: " + result.www.error);
			}

			BTS_WebServerManager.HandlePackageError(result);
		}
	}

	void ValidationFailed(WS_RequestResult result) {
		BTS_WebServerManager.HandlePackageError(result);
	}

	public static string  HMAC(string key, string data) {
		var keyByte = ASCIIEncoding.UTF8.GetBytes(key);
		using (var hmacsha256 = new HMACSHA256(keyByte)) {
			hmacsha256.ComputeHash(ASCIIEncoding.UTF8.GetBytes(data));
			return ByteToString(hmacsha256.Hash);
		}
	}
	
	public static string ByteToString(byte[] buff) {
		string sbinary = "";
		for (int i = 0; i < buff.Length; i++)
			sbinary += buff[i].ToString("X2"); /* hex format */
		return sbinary.ToLower();
	}   
}
}
