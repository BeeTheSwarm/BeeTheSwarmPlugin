using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LandingSceneController : SA_Singleton<LandingSceneController>  {

	public static bool isNotFirstTimeLaunch = false;

	[SerializeField] List<GameObject> _prefabsToLoad;

	void Awake(){
	
		DontDestroyOnLoad (gameObject);
		Application.targetFrameRate = 30;

	}


	void Start(){

		GoToLevel ();
		Debug.Log ("Connections started");
		PlayServiceConnectionStart ();
		AdsController.Instance.Init ();
		LoadPrefabs ();
		
	}

	private void GoToLevel(){

		Time.timeScale = 1f;
		StartCoroutine (LoadLevelAfterDelay ("Level", 2.5f));

	}

	private void LoadPrefabs(){

		StartCoroutine (LoadPrefabsCoroutine ());
	}


	private void PlayServiceConnectionStart(){
		#if UNITY_EDITOR || UNITY_STANDALONE
		/*string playServiceID = "G:000000000";
		BTS_Manager.OnLoginConnectionSuccessfull += OnBTSConnectionSuccessfullHandler;
		BTS_Manager.OnLoginConnectionFail += OnBTSConnectionFailHandler;
		BTS_Manager.Instance.Connect (playServiceID);*/
		#else
		UM_GameServiceManager.OnPlayerConnected += OnPlayerConnected;
		UM_GameServiceManager.OnPlayerDisconnected += OnPlayerDisconnected; 
		UM_GameServiceManager.Instance.Connect();

		UM_InAppPurchaseManager.Client.OnServiceConnected += OnBillingConnectFinishedAction;
		UM_InAppPurchaseManager.Client.Connect ();
		#endif


	}

private void OnBillingConnectFinishedAction(UM_BillingConnectionResult result){
		UM_InAppPurchaseManager.Client.OnServiceConnected -= OnBillingConnectFinishedAction;
		if (result.isSuccess) {
			Debug.Log ("Billing Connected");
		} else {
			Debug.Log ("Failed to connect Billing" + result.message);
		}
	}

	private void OnPlayerConnected(){
		Debug.Log ("Player Connected");
		UM_GameServiceManager.Instance.LoadLeaderboardsInfo ();

		UM_GameServiceManager.OnPlayerConnected -= OnPlayerConnected;
		UM_GameServiceManager.OnPlayerDisconnected -= OnPlayerDisconnected;
	}

	private void OnPlayerDisconnected(){
	
		Debug.Log ("Player Disconnected");

		UM_GameServiceManager.OnPlayerConnected -= OnPlayerConnected;
		UM_GameServiceManager.OnPlayerDisconnected -= OnPlayerDisconnected;
	}

	IEnumerator LoadLevelAfterDelay(string levelname, float delay){
	
		yield return new WaitForSeconds (delay);

		SceneManager.LoadScene (levelname);
	}

	IEnumerator LoadPrefabsCoroutine () {
		yield return new WaitForEndOfFrame ();

		foreach (GameObject gameobject in _prefabsToLoad) {
			Instantiate (gameobject);
		}
	}
}
