using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	public GameManager GM;
	private PlayerPrefsManager PPM;

	public static Action<int> OnHighScoreLoaded = delegate {};
	public static Action<int> OnHighScore = delegate {};

	//int _score = 0;
	//int _highscore;

	const string _highscorePlayerPrefsKey = "Highscore";
	const string LEADERBOARD_SCORE_ID = "Points High Score";

	bool _isHighscore = false;
	bool _isHighscoreLoaded = false;

	public int Score {

		get { 
			return GM.score;
		}
	}

	public int HighScore {

		get{ 
			return GM.bestScore;
		}
	}

	public bool HighScoreLoaded{

		get{ 
			return _isHighscoreLoaded;
		}
	}


	void Start(){
		
		//GM.bestScore = PPM.LoadBestScore();
		OnHighScoreLoaded (GM.bestScore);

		#if UNITY_ANDROID
		if(UM_GameServiceManager.Instance.ConnectionSate ==UM_ConnectionState.CONNECTING){
			UM_GameServiceManager.ActionLeaderboardsInfoLoaded += LeaderboardsInfoLoadedHandler;
		}
		else if (UM_GameServiceManager.Instance.IsConnected){

			Debug.Log("UM_GameServiceManager.Instance.IsConnected");
			LeaderboardsInfoLoadedHandler(null);
		}

		#elif UNITY_IPHONE

		#endif
	}

	private void LeaderboardsInfoLoadedHandler(UM_Result result){
		UM_GameServiceManager.ActionLeaderboardsInfoLoaded -= LeaderboardsInfoLoadedHandler;
		UM_Score leaderboardScore = UM_GameServiceManager.Instance.GetLeaderboard (LEADERBOARD_SCORE_ID).GetCurrentPlayerScore (UM_TimeSpan.ALL_TIME, UM_CollectionType.GLOBAL);
		int leaderboardIntegerScore = (int)leaderboardScore.LongScore;

		if (leaderboardIntegerScore > GM.bestScore) {
			GM.bestScore = leaderboardIntegerScore;
			PPM.SaveBestScore (GM.bestScore);
		} else {
			SubmitScore (GM.bestScore);
		}
		Debug.Log ("OnHighScoreLoaded!");

		_isHighscoreLoaded = true;

		OnHighScoreLoaded (GM.bestScore);
	}

	/*public void OnSubmitScore(int bestscore){
		if (_isHighscore) {
			_isHighscore = false;
			GM.bestScore = bestscore;
		//	PPM.SaveBestScore (GM.bestScore);
			SubmitScore (GM.bestScore);
		}
	}*/

	public void SubmitScore(int score){
		UM_GameServiceManager.ActionScoreSubmitted += HandleActionScoreSubmitted;
		UM_GameServiceManager.Instance.SubmitScore (LEADERBOARD_SCORE_ID, score);

		Debug.Log ("Score submitted!");

	}

	private void HandleActionScoreSubmitted(UM_LeaderboardResult res){
	
		UM_GameServiceManager.ActionScoreSubmitted -= HandleActionScoreSubmitted;
		Debug.Log ("HandleActionScoreSubmitted");

		if (res.IsSucceeded) {
			UM_Score playerScore = res.Leaderboard.GetCurrentPlayerScore (UM_TimeSpan.ALL_TIME, UM_CollectionType.GLOBAL);

			Debug.Log ("score submitted, new player high score:" + playerScore.LongScore);
		} else {
			Debug.Log ("score submission failed:" + res.Error.Code + " / " + res.Error.Description);
		}
	}



}
