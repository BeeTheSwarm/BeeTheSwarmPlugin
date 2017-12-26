using UnityEngine;
using System.Collections;


public class LandingSceneControllerExample : MonoBehaviour {

	[SerializeField] private bool _ClearPlayerPrefs = false;

	void Start () {
		if (_ClearPlayerPrefs)
			PlayerPrefs.DeleteAll ();

//		SA.Analytics.Google.Client;
//		SA.Analytics.Google.GA_Manager.StartTracking();

		BTS_Manager.Instance.Connect ();
	}

	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width / 20, Screen.height / 20, Screen.width / 5, Screen.height / 20), "Log in")) {
			BTS_Manager.Instance.Connect ();
		}

		if (GUI.Button (new Rect (2 * Screen.width / 20 + Screen.width / 5, Screen.height / 20, Screen.width / 5, Screen.height / 20), "Log out")) {
			BTS_Manager.Instance.Disconnect ();
		}

		if (GUI.Button (new Rect (Screen.width / 20, 2 * Screen.height / 20 + Screen.height / 20, Screen.width / 5, Screen.height / 20), "Events System")) {
			BTS_Manager.Instance.GetEvents(); 
			BTS_Manager.Instance.ShowEventSystemInterstitial();
		}

		if (GUI.Button (new Rect (2 * Screen.width / 20 + Screen.width / 5, 2 * Screen.height / 20 + Screen.height / 20, Screen.width / 5, Screen.height / 20), "Reward 1 Bee")) {
			BTS_Manager.Instance.Reward (1);
		}
	}
}
