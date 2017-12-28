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

	Dictionary<int, int> _beesRewardSheet;
	
	bool _isHighscore = false;
	bool _isHighscoreLoaded = false;

	public int Coins {

		get { 
			return GM.coins;
		}
	}

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


	void Awake () {
		//PlayerPrefs.DeleteAll ();

		_beesRewardSheet = new Dictionary<int, int> ();
		//Realise
		_beesRewardSheet.Add (5, 	1);
		_beesRewardSheet.Add (10, 	2);
		_beesRewardSheet.Add (20, 	3);
		_beesRewardSheet.Add (30, 	4);
		_beesRewardSheet.Add (50, 	5);
		_beesRewardSheet.Add (80, 	6);
		_beesRewardSheet.Add (100, 	7);
		_beesRewardSheet.Add (150, 	8);
		_beesRewardSheet.Add (200,  9);
		_beesRewardSheet.Add (300, 10);

		//Test
		/*_beesRewardSheet.Add (1, 	1);
		_beesRewardSheet.Add (2, 	2);
		_beesRewardSheet.Add (3, 	3);
		_beesRewardSheet.Add (4, 	4);
		_beesRewardSheet.Add (5, 	5);
		_beesRewardSheet.Add (6, 	6);
		_beesRewardSheet.Add (7, 	7);
		_beesRewardSheet.Add (8, 	8);
		_beesRewardSheet.Add (9, 	9);
		_beesRewardSheet.Add (10, 	10);*/
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

		Debug.Log ("Coins submitted!");
//		Reward();
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
	
	

	public void SetReward () {
		int bees = 0;
		int points = 0;
		int rewardedScore = 0;
		int _beesEarnedCount = BTS_Manager.Instance.BeesEarnedToday;

		foreach (KeyValuePair<int, int> pair in _beesRewardSheet) {
			if (Score >= pair.Key) {
				rewardedScore = pair.Key;
				bees = pair.Value;
				if (bees > _beesEarnedCount) {
					int beesToReward = bees - _beesEarnedCount;
					BTS_Manager.Instance.Reward (beesToReward);
				}
//				points = bees;
			} else {
				break;
			}
		}
//		if (points > 0) {
//			int pointsToReward = points;
//			
//			else
//				BTS_Manager.Instance.Reward (pointsToReward);
//		}
	}


}
