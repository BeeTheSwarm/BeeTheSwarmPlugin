using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BTS {

internal class BTS_ServerRequest : MonoBehaviour {


	public event Action<WS_RequestResult> RequestCompleted =  delegate {};
	private bool RequestReceived = false;


	private string _URL;
	private BTS_BasePackage _Package;

	public static BTS_ServerRequest Create() {
		GameObject g = new GameObject("WS_ServerRequest");
		g.transform.parent = BTS_WebServer.Instance.gameObject.transform;


		return g.AddComponent<BTS_ServerRequest>();
	}


	public void Send(BTS_BasePackage package, string url) {
		_URL = url;
		_Package = package;
		StartCoroutine(SendRequest(package));

		if(package.Timeout > 0) {
			Invoke("Timeout", package.Timeout);
		}
	}


	public BTS_BasePackage Package {
		get  {
			return _Package;
		}
	}


	private void Timeout() {

		if(RequestReceived) {
			return;
		}

		WS_RequestResult result  = new WS_RequestResult();
		result.www = null;
		result.Status = BTS_RequestStatus.Timeout;
		result.Package = Package;
		
		RequestCompleted(result);
		CleanUp();
	}


	private IEnumerator SendRequest(BTS_BasePackage package) {
	
		#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 
		Hashtable Headers = new Hashtable();
		#else
		Dictionary<string, string> Headers = new Dictionary<string, string>();
		#endif
		
		
		string RequestUrl = _URL;
		
		Dictionary<string, object> OriginalJSON =  new Dictionary<string, object>();
		OriginalJSON.Add("method", package.Id);
		OriginalJSON.Add("fields", package.GenerateData());
		
		
		string data = Json.Serialize(OriginalJSON);
		string hash = BTS_WebServer.HMAC(BTS_WebServer.SECRET, data); 
		
		byte[] binaryData = ASCIIEncoding.UTF8.GetBytes(data);
		Headers.Add("Content-Length", binaryData.Length.ToString());
		Headers.Add("Content-Type", "application/json");
		Headers.Add("siq", hash);
		
		switch(Application.platform) {
		case RuntimePlatform.Android:
			Headers.Add("platform", "2");
			break;
		default:
			Headers.Add("platform", "1");
			break;
		}

		//uid
		string auth_token = "";
		if (BTS_PlayerData.Instance != null && BTS_PlayerData.Instance.Player != null) {
			auth_token = BTS_PlayerData.Instance.StoredAuthToken;
		}
		Headers.Add ("auth", auth_token);

		//gid
		string gid = BTS_WebServer.GameID;
		Headers.Add ("gid", gid);
		
        Debug.Log("Sending: " + data);
		
		
		WWW www = new WWW(RequestUrl, binaryData, Headers);
		
		// Wait for download to complete
		yield return www;

		RequestReceived = true;

		WS_RequestResult result  = new WS_RequestResult();
		result.www = www;
		result.Package = Package;

		if(www.error == null) { 
			result.Status = BTS_RequestStatus.Completed;
		} else {
			result.Status = BTS_RequestStatus.Failed;
		}

		RequestCompleted(result);

		CleanUp();
	}


	private void CleanUp() {
		Destroy(gameObject);
	}

}
}
