﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameManager : MonoBehaviour {

    enum State { MENU, SHOP, PAUSE, GAME, GAMEOVER};
    State GameState;

    [Header("Ad Management")]
    public AdManager adManager;

    [Header("Canvas Groups")]
    public CanvasGroup MAIN_MENU_CG;
    public CanvasGroup GAME_OVER_CG;
    public CanvasGroup IN_GAME_CG;
    public CanvasGroup PAUSE_CG;
    public CanvasGroup SHOP_CG;
	public CanvasGroup SECOND_MENU_CG;
	public CanvasGroup SETTINGS_MENU_CG;
    

    [Header("Ball & Box Containers")]
    public GameObject BallSpawner;
    public GameObject BlocksManager;

    [HideInInspector]
    public int score;
    public int bestScore;

    [Header("Score Variables")]
    public Text scoreText;
    public Text menuScoreText;
    public Text shopScoreText;
    public Text gameOverScoreText;
    public Text gameOverBestText;

	[SerializeField]Animator _menusAnimatorController;
	[SerializeField]Animator _settingsAnimatorController;

	const string BLOCKSMASH_ID_IOS = "";   //need add when game create on AppStore
	const string BLOCKSMASH_URL_ANDROID = "https://www.google.com"; //need add when game create on GooglePlay

    private PlayerPrefsManager PPM;

	// Use this for initialization
	void Start () {
		
        // Screen doesn't sleep ! A monster !! 
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        PPM = this.GetComponent<PlayerPrefsManager>();
        
        score = PPM.LoadCoins();
        bestScore = PPM.LoadBestScore();

        menuScoreText.text = "" + score;
        scoreText.text = "" + score;
        shopScoreText.text = "" + score;

        GameState = State.MENU;

        Time.timeScale = 1;

        EnableCG(MAIN_MENU_CG);
	  //DisableCG (SECOND_MENU_CG);
        DisableCG(GAME_OVER_CG);
     // DisableCG(PAUSE_CG);
        DisableCG(IN_GAME_CG);
        DisableCG(SHOP_CG);
		DisableCG (SETTINGS_MENU_CG);

        BallSpawner.SetActive(false);
        BlocksManager.SetActive(false);

    }

	    // Update is called once per frame
    void Update () {
		
	}

    public void UpdateScoreText()
    {
        score = PPM.LoadCoins();

        scoreText.text = "" + score;
        shopScoreText.text = "" + score;
        menuScoreText.text = "" + score;

    }

    public void SetGameOver()
    {
		StartCoroutine (PlayBTSPromoCoroutine());

        if (GameState != State.GAMEOVER)
        {

			if (GameState == State.GAME) {

				_menusAnimatorController.SetTrigger ("ShowGameOver");
				GameState = State.GAMEOVER;

			} else {
				_menusAnimatorController.SetTrigger ("HideGameOver");

			}


            int linesAmount = BlocksManager.GetComponent<BlocksManager>().linesAmount;

            PPM.SaveCoins(score);
        
            GameState = State.GAMEOVER;

            adManager.gameOverCount++;
            Debug.Log(adManager.gameOverCount);

            //Set Game Over score & best texts
            gameOverScoreText.text = "" + linesAmount;

            if (linesAmount > bestScore)
                bestScore = linesAmount;

            gameOverBestText.text = "" + bestScore;

            //Save the best score
            PPM.SaveBestScore(bestScore);

            //Display the GameOverCanvas
          //  EnableCG(GAME_OVER_CG);
			DisableCG (SECOND_MENU_CG);
            DisableCG(MAIN_MENU_CG);
         //   DisableCG(IN_GAME_CG);
            DisableCG(SHOP_CG);
			DisableCG (SETTINGS_MENU_CG);

            BallSpawner.SetActive(false);
            BlocksManager.SetActive(false);

            for (int i = 0; i < BlocksManager.transform.childCount; i++)
            {
                //Delete all the previous Blocks
                BlocksManager.SetActive(false);
                Destroy(BlocksManager.transform.GetChild(i).gameObject);

                //We should delete again for safety when GO is displayed.            
            }



            //Then we reset the ballcount to 1
            BallSpawner.GetComponent<BallControl>().numberOfBalls = 1;
            BallSpawner.GetComponent<BallControl>().numberOfBallsText.text = "x 1";
        }

    }

    void EnableCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }
    void DisableCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }

    public void StartGame()
    {
		
		if (GameState == State.GAMEOVER) {
			_menusAnimatorController.SetTrigger ("HideGameOver");
		}
		if (GameState == State.PAUSE)
			_menusAnimatorController.SetTrigger ("HideAdditionalMenu");

		GameState = State.GAME;


		if (GameState == State.MENU)
			_menusAnimatorController.SetTrigger ("ShowMainMenu");
		else 
			_menusAnimatorController.SetTrigger ("HideMenu");

													//BallSpawner.GetComponent<BallControl>().ResetSettings();
		BallSpawner.SetActive(false);

        //Reset the number of lines too
        BlocksManager.GetComponent<BlocksManager>().linesAmount = 1;
        BlocksManager.GetComponent<BlocksManager>().levelText.text = "" + BlocksManager.GetComponent<BlocksManager>().linesAmount;

        for (int i = 0; i < BlocksManager.transform.childCount; i++)
        {
            //Delete all the previous Blocks
            Destroy(BlocksManager.transform.GetChild(i).gameObject);
        }

        //BlocksManager.GetComponent<BlocksManager>().SpawnBlockLine();

        UpdateScoreText();

        BallSpawner.SetActive(true);
        BlocksManager.SetActive(true);
		BallSpawner.GetComponent<BallControl>().ResetSettings();

        Time.timeScale = 1;

        EnableCG(IN_GAME_CG);
	  //DisableCG (SECOND_MENU_CG);
     // DisableCG(MAIN_MENU_CG);
    //  DisableCG(PAUSE_CG);
     // DisableCG(GAME_OVER_CG);
        DisableCG(SHOP_CG);
    }

    public void MainMenu()
    {

        menuScoreText.text = "" + score;

        //Save the best score
        PPM.SaveBestScore(bestScore);

        //Reset the number of lines too
        BlocksManager.GetComponent<BlocksManager>().linesAmount = 1;

        GameState = State.MENU;

        Time.timeScale = 1;

        EnableCG(MAIN_MENU_CG);
		DisableCG (SECOND_MENU_CG);
        DisableCG(GAME_OVER_CG);
        DisableCG(PAUSE_CG);
        DisableCG(IN_GAME_CG);
        DisableCG(SHOP_CG);
		DisableCG (SETTINGS_MENU_CG);

        BallSpawner.SetActive(false);
        BlocksManager.SetActive(false);

    }

	/*public void SecondMenu(){
	
		GameState = State.MENU;
		Time.timeScale = 1;

		EnableCG (SECOND_MENU_CG);
		DisableCG (PAUSE_CG);
		DisableCG (GAME_OVER_CG);
		DisableCG (IN_GAME_CG);
		DisableCG (MAIN_MENU_CG);
		DisableCG (SHOP_CG);

		BallSpawner.SetActive (false);
		BlocksManager.SetActive (false);


	}*/

    public void Pause()
    {
		if (GameState == State.GAME) {
			
			_menusAnimatorController.SetTrigger ("ShowAdditionalMenu");
			GameState = State.PAUSE;
									//	BallSpawner.SetActive(false);

		} else {
			_menusAnimatorController.SetTrigger ("HideMenu");


		}

        
		Time.timeScale = 0;



        PPM.SaveCoins(score);

        EnableCG(SECOND_MENU_CG);
		DisableCG (PAUSE_CG);
        DisableCG(GAME_OVER_CG);
        DisableCG(IN_GAME_CG);
        DisableCG(MAIN_MENU_CG);
        DisableCG(SHOP_CG);
		DisableCG (SETTINGS_MENU_CG);

        menuScoreText.text = "" + score;

    }

    public void ContinueGame()
    {
		
		if (GameState == State.PAUSE) {
			_menusAnimatorController.SetTrigger ("HideAdditionalMenu");
			GameState = State.GAME;
		} else {
			Debug.Log (GameState);
			_menusAnimatorController.SetTrigger ("ShowAdditionalMenu");

		}

		Time.timeScale = 1;
										//	BallSpawner.SetActive(true);
    //  EnableCG(IN_GAME_CG);
	//	DisableCG (SECOND_MENU_CG);
        DisableCG(GAME_OVER_CG);
  //    DisableCG(PAUSE_CG);
        DisableCG(MAIN_MENU_CG);
        DisableCG(SHOP_CG);
		DisableCG (SETTINGS_MENU_CG);
    }

    public void OpenShop()
    {
        GameState = State.SHOP;

        shopScoreText.text = "" + score;

        EnableCG(SHOP_CG);
		DisableCG (SECOND_MENU_CG);
        DisableCG(GAME_OVER_CG);
        DisableCG(PAUSE_CG);
        DisableCG(MAIN_MENU_CG);
        DisableCG(IN_GAME_CG);
    }

    public void FastForwardFunction()
    {
        Time.timeScale = 3;
    }

    public void OpenBlockSmashReview()
    {
		
		#if UNITY_IPHONE
			IOSNativeUtility.RedirectToAppStoreRatingPage(BLOCKSMASH_ID_IOS);
		#elif UNITY_ANDROID
			AndroidNativeUtility.RedirectToGooglePlayRatingPage(BLOCKSMASH_URL_ANDROID);
		#endif
		Debug.Log ("OpenBlockSmashReview");

      //  Application.OpenURL("https://play.google.com/store/apps/details?id=com.yourComp.yourGameName");
    }

	public void OnAboutUsClick(){
		
		Application.OpenURL ("https://campaign.beetheswarm.com/feed");
	}

	public void OpenSettingsMenu(){
	
		if (GameState == State.PAUSE) {

			_settingsAnimatorController.SetTrigger ("ShowSettingsMenu");
		
		} else {
			_settingsAnimatorController.SetTrigger ("HideSettingsMenu");

		}
	}

		public void BackToAdditionalMenu(){

			if (GameState == State.PAUSE) {
			
				_settingsAnimatorController.SetTrigger ("HideSettingsMenu");
		} else {
				_settingsAnimatorController.SetTrigger ("ShowSettingsMenu");
		}
	}

	public void OnEarnBeesButtonClick(){
	

		EarnBeesInterstitialController.Instance.Show (EarnBeesInterstitialController.EarnBeesInterstitialShowType.Table);
	}

	IEnumerator PlayBTSPromoCoroutine(){

		yield return new WaitForSeconds (0.5f);
		BTSPromoController.Instance.ShowOurGamesPromo ();
	}
}
