using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;

public class BTSPromoController : SingletonPrefab<BTSPromoController> {

	const string ZOMBEES_ID_IOS                  = "1041708050";
	const string ZOMBEES_URL_ANDROID             = "https://play.google.com/store/apps/details?id=com.BeeTheSwarm.ZomBees";
	const string STD_ID_IOS						 = "1214312696";
	const string STD_URL_ANDROID				 = "https://play.google.com/store/apps/details?id=com.beetheswarm.stackthedots";
	const string FB_ID_IOS						 = "1170933749";
	const string FB_URL_ANDROID 				 = "https://play.google.com/store/apps/details?id=com.beetheswarm.flappybee";
	const string GAME2048_ID_IOS				 = "1089506962";
	const string GAME2048_URL_ANDROID			 = "https://play.google.com/store/apps/details?id=com.beetheswarm.the2048game";
	const string BUSGCRUSH_ID_IOS				 = "1182934928";
	const string BUGSCRUSH_URL_ANDROID			 = "https://play.google.com/store/apps/details?id=com.beetheswarm.bugscrush";

	Animator _animator;

	bool isShowed = false;

	private int interval = 60 * 60; //1 hour
	private int del = 10;

	[SerializeField] private List<GameObject> promos;

	/////////get set

	private int LastPlayedPromo{

		get { 
			return PlayerPrefs.GetInt ("last played");
		}
		set{ 
			PlayerPrefs.SetInt ("last played", value);
		}
	}

	private string LastShowedDateTime{

		get { 
			return PlayerPrefs.GetString ("last showed date time");
		}
		set { 
			PlayerPrefs.SetString ("last showed date time", value);
		}
	}


	/////Unity functions

	void Awake(){
		_animator = GetComponent<Animator> ();
	}

	void Enable(){
		
	}

	void Disable(){
	
	}

	///////////////public functions


	public void Show(){
		_animator.SetTrigger ("FadeIn");
	}

	public void Hide(){
		_animator.SetTrigger ("FadeOut");
	}

	public void GoToBtsApp(){
		#if UNITY_ANDROID
		Debug.Log(AndroidNativeUtility.Instance.ToString());
		AndroidNativeUtility.OnPackageCheckResult += OnPackageCheckResultCallback;
		AndroidNative.isPackageInstalled("com.beetheswarm.app");
		#elif UNITY_IOS
		if(SA.IOSNative.System.SharedApplication.CheckUrl("beetheswarm://")){
		SA.IOSNative.System.SharedApplication.OpenUrl("beetheswarm://");
		} else{
		SA.IOSNative.System.SharedApplication.OpenUrl("itms-apps://itunes.apple.com/us/app/bee-the-swarm/id1019379941?mt=8");
		}
		#endif
	}

	public void OpenZombeesPage(){
	
		#if UNITY_IPHONE
		IOSNativeUtility.RedirectToAppStoreRatingPage(ZOMBEES_ID_IOS);
		#elif UNITY_ANDROID
		AndroidNativeUtility.RedirectToGooglePlayRatingPage(ZOMBEES_URL_ANDROID);
		#endif

		Debug.Log("OpenZombeesPage");
	}

	public void OpenSTDPage(){

		#if UNITY_IPHONE
		IOSNativeUtility.RedirectToAppStoreRatingPage(STD_ID_IOS);
		#elif UNITY_ANDROID
		AndroidNativeUtility.RedirectToGooglePlayRatingPage(STD_URL_ANDROID);
		#endif

		Debug.Log("OpenSTDPage");
	}

	public void OpenFlappyBeePage(){
	
		#if UNITY_IPHONE
		IOSNativeUtility.RedirectToAppStoreRatingPage(FB_ID_IOS);
		#elif UNITY_ANDROID
		AndroidNativeUtility.RedirectToGooglePlayRatingPage(FB_URL_ANDROID);
		#endif

		Debug.Log("OpenFlappyBeePage");
	}

	public void Open2048Page(){

		#if UNITY_IPHONE
		IOSNativeUtility.RedirectToAppStoreRatingPage(GAME2048_ID_IOS);
		#elif UNITY_ANDROID
		AndroidNativeUtility.RedirectToGooglePlayRatingPage(GAME2048_URL_ANDROID);
		#endif

		Debug.Log("Open2048Page");
	}

	public void OpenBugsCrushPage(){
	
		#if UNITY_IPHONE
		IOSNativeUtility.RedirectToAppStoreRatingPage(BUGSCRUSH_ID_IOS);
		#elif UNITY_ANDROID
		AndroidNativeUtility.RedirectToGooglePlayRatingPage(BUGSCRUSH_URL_ANDROID);
		#endif

		Debug.Log("OpenBugsCrushPage");
	} 

	public void ShowOurGamesPromo(){

		if (isShowed)
			return;

		DateTime dateTime = DateTime.UtcNow;
		DateTime lastShowTime = ParsedDateTime (LastShowedDateTime);

		if (TimeToShow (dateTime, lastShowTime, interval)) {
			PromoToShow ();
			Show ();
			isShowed = true;
			LastShowedDateTime = DateTime.UtcNow.ToString ();
		}
	}

	private void PromoToShow(){

		foreach (GameObject ob in promos) {
			ob.SetActive (false);
		}

		if (LastPlayedPromo == promos.Count - 1)
				LastPlayedPromo = 0;
			else
				LastPlayedPromo++;
		promos [LastPlayedPromo].SetActive (true);
	}

	private bool TimeToShow(DateTime dateTime, DateTime prevDateTime, int IntervalInSeconds){
		long delta = (long)(dateTime - prevDateTime).TotalSeconds;
		return delta > IntervalInSeconds;
	}

	static DateTime ParsedDateTime(string dateTime){
		if (dateTime == String.Empty) {
			return DateTime.MinValue;
		}
		return Convert.ToDateTime (dateTime);
	}

	private void OnPackageCheckResultCallback (AN_PackageCheckResult result){
		if (result.IsSucceeded) {
			AndroidNative.LaunchApplication ("com.beetheswarm.app", string.Empty);
		} else {
			Application.OpenURL ("https://play.google.com/store/apps/details?id=com.beetheswarm.app");
		}
	}
}
