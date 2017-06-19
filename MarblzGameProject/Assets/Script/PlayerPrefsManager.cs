using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveCoins(int coins)
    {
        PlayerPrefs.SetInt("COINS", coins);
    }

    public int LoadCoins()
    {
        return PlayerPrefs.GetInt("COINS");
    }

    public void SaveBestScore(int bestScore)
    {
        PlayerPrefs.SetInt("BESTSCORE", bestScore);
    }

    public int LoadBestScore()
    {
        return PlayerPrefs.GetInt("BESTSCORE");
    }

    public void SaveBallPurchased(int ballId)
    {
        PlayerPrefs.SetInt("BALL" + ballId, 1);
    }

    public int LoadBallPurchased(int ballId)
    {
        return PlayerPrefs.GetInt("BALL" + ballId);
    }
}
