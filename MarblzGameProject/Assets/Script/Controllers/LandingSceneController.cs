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
		Debug.Log ("Connection started");

		LoadPrefabs ();
	}

	private void GoToLevel(){

		Time.timeScale = 1f;
		StartCoroutine (LoadLevelAfterDelay ("Level", 2.5f));

	}

	private void LoadPrefabs(){

		StartCoroutine (LoadPrefabsCoroutine ());
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
