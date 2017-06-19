using UnityEngine;
using UnityEngine.UI;

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
        DisableCG(GAME_OVER_CG);
        DisableCG(PAUSE_CG);
        DisableCG(IN_GAME_CG);
        DisableCG(SHOP_CG);

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
        if (GameState != State.GAMEOVER)
        {

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
            EnableCG(GAME_OVER_CG);
            DisableCG(MAIN_MENU_CG);
            DisableCG(IN_GAME_CG);
            DisableCG(SHOP_CG);

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
        GameState = State.GAME;

        BallSpawner.GetComponent<BallControl>().ResetSettings();

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

        Time.timeScale = 1;

        EnableCG(IN_GAME_CG);
        DisableCG(MAIN_MENU_CG);
        DisableCG(PAUSE_CG);
        DisableCG(GAME_OVER_CG);
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
        DisableCG(GAME_OVER_CG);
        DisableCG(PAUSE_CG);
        DisableCG(IN_GAME_CG);
        DisableCG(SHOP_CG);

        BallSpawner.SetActive(false);
        BlocksManager.SetActive(false);

    }

    public void Pause()
    {
        Time.timeScale = 0;

        PPM.SaveCoins(score);

        EnableCG(PAUSE_CG);
        DisableCG(GAME_OVER_CG);
        DisableCG(IN_GAME_CG);
        DisableCG(MAIN_MENU_CG);
        DisableCG(SHOP_CG);

        menuScoreText.text = "" + score;

    }

    public void ContinueGame()
    {
        Time.timeScale = 1;

        EnableCG(IN_GAME_CG);
        DisableCG(GAME_OVER_CG);
        DisableCG(PAUSE_CG);
        DisableCG(MAIN_MENU_CG);
        DisableCG(SHOP_CG);
    }

    public void OpenShop()
    {
        GameState = State.SHOP;

        shopScoreText.text = "" + score;

        EnableCG(SHOP_CG);
        DisableCG(GAME_OVER_CG);
        DisableCG(PAUSE_CG);
        DisableCG(MAIN_MENU_CG);
        DisableCG(IN_GAME_CG);
    }

    public void FastForwardFunction()
    {
        Time.timeScale = 3;
    }

    public void RateGame()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.yourComp.yourGameName");
    }
}
