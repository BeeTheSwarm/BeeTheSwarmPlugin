using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

	public static AdManager Instance{set;get;}
	public string InterstitialId;
	public string BannerId;

    public int ShowInterEveryXGameOvers;

    [HideInInspector]
    public int gameOverCount;

	private BannerView bannerView;
	private InterstitialAd interstitial;

	// Use this for initialization
	private void Start () {

        gameOverCount = 0;

		Instance = this;
		DontDestroyOnLoad (gameObject);

		//InvokeRepeating ("ShowVideo", 60f, 180);

		bannerView = new BannerView (BannerId, AdSize.Banner, AdPosition.Bottom);
		interstitial = new InterstitialAd (InterstitialId);

        LoadVideo();

        ShowBanner();
	}

    private void Update()
    {
        if(gameOverCount == ShowInterEveryXGameOvers)
        {
            ShowVideo();
            gameOverCount = 0;
        }
    }

    public void LoadVideo() {
		AdRequest request = new AdRequest.Builder ().Build ();
		interstitial.LoadAd (request);

	}

	public void ShowVideo()
	{
		if (interstitial.IsLoaded ())
			interstitial.Show ();
		else {
			AdRequest request = new AdRequest.Builder ().Build ();
			interstitial.LoadAd (request);
		}
	}

	public void ShowBanner()
	{
		AdRequest request = new AdRequest.Builder ().Build ();
		bannerView.LoadAd (request);

		bannerView.Show ();
	}

	public void RemoveBanner()
	{
		bannerView.Hide ();
	}

}
