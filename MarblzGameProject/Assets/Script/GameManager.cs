﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static event Action<bool> OnSecondMenuConditionChanged = delegate {};

    
    //enum State { MENU, SHOP, PAUSE, GAME, GAMEOVER};
     

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
    public int coins;
    public int bestScore;
	public int score;	

    [Header("Coins Variables")]
    public Text scoreText;
    public Text menuScoreText;
    public Text shopScoreText;
    public Text gameOverScoreText;
    public Text gameOverBestText;

   // [SerializeField] Text _countdownText;
	[SerializeField]Animator _menusAnimatorController;
	[SerializeField]Animator _settingsAnimatorController;
	[SerializeField]Animator _shopAnimatorController;
    [SerializeField] Animator m_rewardBeesAnimatorController;

	const string BLOCKSMASH_ID_IOS = "1313223895";   //need add when game create on AppStore
	const string BLOCKSMASH_URL_ANDROID = "https://play.google.com/store/apps/details?id=com.beetheswarm.blocksmash"; //need add when game create on GooglePlay

    private PlayerPrefsManager PPM;
	public ScoreManager ScoreManager;


    // Use this for initialization
	void Start () {
		
		BTS_Manager.Instance.Init();

        // Screen doesn't sleep ! A monster !! 
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        PPM = this.GetComponent<PlayerPrefsManager>();
	//	PlayerPrefs.DeleteAll ();
        coins = PPM.LoadCoins();
		score = 0;
		//score=10000;
		//PPM.SaveCoins (score);
        bestScore = PPM.LoadBestScore();
		Debug.Log (coins);
        menuScoreText.text = "" + coins;
        scoreText.text = "" + coins;
        shopScoreText.text = "" + coins;


        GameStateManager.Instance.MenuGame();
		AdsController.Instance.ShowBanner ();
        Time.timeScale = 1;

        EnableCG(MAIN_MENU_CG);
	 //   DisableCG (SECOND_MENU_CG);
        DisableCG(GAME_OVER_CG);
      //  DisableCG(PAUSE_CG);
        DisableCG(IN_GAME_CG);
        DisableCG(SHOP_CG);
		DisableCG (SETTINGS_MENU_CG);

        BallSpawner.SetActive(false);
        BlocksManager.SetActive(false);

    }

    void OnEnable()
    {
       // GameStateManager.OnGameStarted += StartGame;
       // GameStateManager.OnGamePaused += Pause;
       // GameStateManager.OnGameRestarted += OnGameRestartedHandler;
       // GameStateManager.OnGameResumed += OnGameResumedHandler;
       // GameStateManager.OnGameOver += OnGameOverHandler;
    }

     void OnDisable()
    {
      //  GameStateManager.OnGameStarted -= StartGame;
       // GameStateManager.OnGamePaused -= Pause;
       // GameStateManager.OnGameRestarted -= OnGameRestartedHandler;
       // GameStateManager.OnGameResumed -= OnGameResumedHandler;
       // GameStateManager.OnGameOver -= OnGameOverHandler;
    }


    // Update is called once per frame
    void Update () {


	}


    public void UpdateScoreText()
    {
        coins = PPM.LoadCoins();

        scoreText.text = "" + coins;
        shopScoreText.text = "" + coins;
        menuScoreText.text = "" + coins;

    }

    public void SetGameOver()
    {
		
		AdsController.Instance.VideoShow ();
		StartCoroutine (PlayBTSPromoCoroutine());

        

     //   if (GameStateManager.Instance.State != GameState.GameOver)
      //  {

            if (GameStateManager.Instance.State == GameState.Live)
            {

                _menusAnimatorController.SetTrigger("ShowGameOver");
                GameStateManager.Instance.OverGame();

            }
           // else
           // {
          //      _menusAnimatorController.SetTrigger("HideGameOver");

        //    }


            int linesAmount = BlocksManager.GetComponent<BlocksManager>().linesAmount;
            score = linesAmount;

            PPM.SaveCoins(coins);
           // GameStateManager.Instance.OverGame();
            //  GameState = State.GAMEOVER;

            //Set Game Over score & best texts
            gameOverScoreText.text = "" + linesAmount;

            if (linesAmount > bestScore)
                bestScore = linesAmount;

            gameOverBestText.text = "" + bestScore;
            Debug.Log(gameOverBestText.text);
            //Save the best score
            PPM.SaveBestScore(bestScore);

            ScoreManager.SubmitScore(bestScore);

            //Display the GameOverCanvas
            //  EnableCG(GAME_OVER_CG);
            DisableCG(SECOND_MENU_CG);
            DisableCG(MAIN_MENU_CG);
            //   DisableCG(IN_GAME_CG);
            DisableCG(SHOP_CG);
            DisableCG(SETTINGS_MENU_CG);

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
       // }

    }

    void EnableCG(CanvasGroup cg)
    {
		if (cg.name.Equals ("PANEL SECOND MENU"))
			OnSecondMenuConditionChanged (false);
		
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }
    void DisableCG(CanvasGroup cg)
    {
		Debug.Log ("cg name " + cg.name);
		if (cg.name.Equals ("PANEL SECOND MENU"))
			OnSecondMenuConditionChanged (true);
		
		cg.alpha = 0;       
		cg.blocksRaycasts = false;
        cg.interactable = false;
    }

    public void StartGame()
    {
       
        if (GameStateManager.Instance.State == GameState.GameOver) {
           
			_menusAnimatorController.SetTrigger ("HideGameOver");
		}
        if (GameStateManager.Instance.State == GameState.Pause)
        {
            
            _menusAnimatorController.SetTrigger("HideAdditionalMenu");
        }
        GameStateManager.Instance.StartGame();
       
        //GameState = State.GAME;


        if (GameStateManager.Instance.State == GameState.Menu)
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

	    OnSecondMenuConditionChanged (true);
	    score = 0;
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

        menuScoreText.text = "" + coins;

        //Save the best score
        PPM.SaveBestScore(bestScore);

        //Reset the number of lines too
        BlocksManager.GetComponent<BlocksManager>().linesAmount = 1;

        //GameState = State.MENU;

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


    public void Pause()
    {
        
        GameStateManager.Instance.PauseGame();
        
        //if (GameState == State.GAME) {

        _menusAnimatorController.SetTrigger ("ShowAdditionalMenu");
			//GameState = State.PAUSE;

		//} else {
		//	_menusAnimatorController.SetTrigger ("HideMenu");


		//}
		        
		Time.timeScale = 0;

		PPM.SaveCoins(coins);

        EnableCG(SECOND_MENU_CG);
		DisableCG (PAUSE_CG);
        DisableCG(GAME_OVER_CG);
        DisableCG(IN_GAME_CG);
        DisableCG(MAIN_MENU_CG);
        //DisableCG(SHOP_CG);
		DisableCG (SETTINGS_MENU_CG);

        menuScoreText.text = "" + coins;

    }

    public void ContinueGame()
    {
        //StartCoroutine(ResumeToGame(1f));
	//	if (GameState == State.PAUSE) {
			_menusAnimatorController.SetTrigger ("HideAdditionalMenu");
			//GameState = State.GAME;

		//} else {
		//	Debug.Log (GameState);
		//	_menusAnimatorController.SetTrigger ("ShowAdditionalMenu");

		//}

		Time.timeScale = 1;

	//  EnableCG(IN_GAME_CG);
		DisableCG (SECOND_MENU_CG);
        DisableCG(GAME_OVER_CG);
  //    DisableCG(PAUSE_CG);
        DisableCG(MAIN_MENU_CG);
        //DisableCG(SHOP_CG);
		DisableCG (SETTINGS_MENU_CG);
    }

    public void OpenShop()
    {
		//GameState = State.SHOP;

		shopScoreText.text = "" + coins;
		//EnableCG(SHOP_CG);
		//DisableCG (SECOND_MENU_CG);
      //  DisableCG(GAME_OVER_CG);
      //  DisableCG(PAUSE_CG);
     //   DisableCG(MAIN_MENU_CG);
     //   DisableCG(IN_GAME_CG);

		if (GameStateManager.Instance.State == GameState.Pause) {
			
			_shopAnimatorController.SetTrigger ("FadeIn");
		
		} else {
			_shopAnimatorController.SetTrigger ("FadeOut");

		}

    }

	public void CloseShop(){
		
		_shopAnimatorController.SetTrigger ("FadeOut");
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

     }

	public void OnBTSButtonClick() {
//		BTS_Manager.Instance.ShowInterstitial();
		BTS_Manager.Instance.ShowPanel();
		if (BTS_Manager.Instance.IsConnected) {
			BTS_Manager.Instance.ShowBTSStatus();
		} 
	}

	public void OnAboutUsClick(){
		
		Application.OpenURL ("https://campaign.beetheswarm.com/feed");
	}

	public void OpenSettingsMenu(){
	
		if (GameStateManager.Instance.State == GameState.Pause) {

			_settingsAnimatorController.SetTrigger ("ShowSettingsMenu");
		
		} else {
			_settingsAnimatorController.SetTrigger ("HideSettingsMenu");

		}
	}

		public void BackToAdditionalMenu(){

			if (GameStateManager.Instance.State == GameState.Pause) {
			
				_settingsAnimatorController.SetTrigger ("HideSettingsMenu");
		} else {
				_settingsAnimatorController.SetTrigger ("ShowSettingsMenu");
		}
	}

    public void OpenRewardBeesScreen() {
        if (GameStateManager.Instance.State == GameState.Pause)
        {
               m_rewardBeesAnimatorController.SetTrigger("OpenRewardBeesScreen");
        }
        else {
                m_rewardBeesAnimatorController.SetTrigger("HideRewardBeesScreen");     
        }
    }

    public void CloseRewardBeesScreen()
    {
        m_rewardBeesAnimatorController.SetTrigger("HideRewardBeesScreen");
    }


  /*  public void OnEarnBeesButtonClick(){
			EarnBeesInterstitialController.Instance.Show (EarnBeesInterstitialController.EarnBeesInterstitialShowType.Table);
	}*/

	public void OnLeaderboardsButtonClick () {
		UM_GameServiceManager.Instance.ShowLeaderBoardsUI ();
	}

	IEnumerator PlayBTSPromoCoroutine(){

		yield return new WaitForSeconds (0.5f);
		BTSPromoController.Instance.ShowOurGamesPromo ();
	}


	public bool TrySpendCoins(int scoreToSpend){
		if ((coins - scoreToSpend) >=0){
			coins -= scoreToSpend;

			PPM.SaveCoins (coins);
			shopScoreText.text = "" + coins;

			return true;
		}
		return false;
	}


    /*IEnumerator ResumeToGame(float countdownTimeStep) {
        for (int seconds = 3; seconds >= 1; seconds--) {
            _countdownText.text = seconds.ToString();
            yield return new WaitForSeconds(countdownTimeStep);
        }   
    }*/

}
