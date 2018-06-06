using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingUtil : MonoBehaviour {

	public static Action OnInternetCheckStarted = delegate{};
	public static Action<bool> OnInternetCheckEnded = delegate{};

	private static NetworkingUtil _instance;

	public static NetworkingUtil Instance {
		get {
			if (_instance == null) {
				_instance = (new GameObject ().AddComponent<NetworkingUtil> ());
			}

			return _instance;
		}
	}

	//Constants 
	private const Int16 TIME_OUT = 3;

	//Coroutines
	IEnumerator _checkInternetCoroutine;
	IEnumerator _timeOutCoroutine;

	public void CheckInternetConnection (Action<bool> action) { 
		_checkInternetCoroutine = CheckInternetConnectionCoroutine ((state) => {
			StopCoroutine (_timeOutCoroutine);
			if (state == true) {
				action (true);
			} else {
				action (false);
			}
		});
		StartCoroutine (_checkInternetCoroutine);

		_timeOutCoroutine = ConnectionTimeOutCoroutine ( ()=> {
			StopCoroutine (_checkInternetCoroutine);
			action (false);
		}, TIME_OUT);
		StartCoroutine (_timeOutCoroutine);
	}

	private void Reset () {
		_checkInternetCoroutine = null;
		_timeOutCoroutine = null;
	}

	IEnumerator CheckInternetConnectionCoroutine(Action<bool> action){
		WWW www = new WWW("http://google.com");
		yield return www;
		if (www.error != null) {
			action (false);
		} else {
			action (true);
		}
	} 

	IEnumerator ConnectionTimeOutCoroutine (Action action, float timeout) {
		//Carefull
		Time.timeScale = 1f;
		yield return new WaitForSeconds (timeout);
	}
}
