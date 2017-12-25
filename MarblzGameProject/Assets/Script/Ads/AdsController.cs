using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdsController : SA.Common.Pattern.Singleton<AdsController> {

	private bool _isShowAds = true;

	private const string NOADS_ID = "NoAds";
	private const int m_videoPercent = 30;

	public void Init(){
		
		CheckAds ();

		SA.UltimateAds.Banners.Init ();
		SA.UltimateAds.Video.Init ();
		SA.UltimateAds.Interstitial.Init ();

		UM_InAppPurchaseManager.Instance.OnPurchaseFinished += OnPurchaseFinishedHandler;
	}

	void Start(){
		DontDestroyOnLoad (gameObject);
	}

	public void DisableAds(){
		Debug.Log ("ADS DISABLED");
		_isShowAds = false;
		PlayerPrefs.SetInt ("AdsDisabled", 1);
	}


	private bool CheckAds(){
		if (PlayerPrefs.HasKey ("AdsDisabled")) {
			return (_isShowAds = false);
		}
		return true;
	}

	public void HideBanner(){
		if (!_isShowAds)
			return;

		SA.UltimateAds.Banners.Destroy ();
	}

	public void ShowBanner(){
		if (!_isShowAds)
			return;

		SA.UltimateAds.Banners.Show ();
	}

	public void VideoShow(){
		if (!_isShowAds)
			return;

		int randomValue = UnityEngine.Random.Range (1, 101);

		Debug.Log ("randomValue:" + randomValue);

		if (randomValue <= m_videoPercent && SA.UltimateAds.Video.IsVideoReady ()) {
			SA.UltimateAds.Video.Show ();
		} else {
			if (SA.UltimateAds.Interstitial.IsReady ()) {
				SA.UltimateAds.Interstitial.Show ();
			}
		}
	
	}

	void OnPurchaseFinishedHandler(UM_PurchaseResult result){
		if(result.isSuccess && result.product.id == NOADS_ID){
			UM_InAppPurchaseManager.Instance.OnPurchaseFinished -= OnPurchaseFinishedHandler;

			DisableAds();
		}
	}
}
