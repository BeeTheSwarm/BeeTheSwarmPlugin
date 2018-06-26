using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BTS;

[CreateAssetMenu(fileName = "BTS_Settings", menuName = "BTS/Settings", order = 1)]
public class BTS_Settings : ScriptableObject {

	private const string SETTINGS_NAME = "BTS_Settings";
	private static BTS_Settings _instance;

	private static BTS_Settings Instance {
		get {
			if (_instance == null) {
				_instance = Resources.Load<BTS_Settings> (SETTINGS_NAME);
			}
			return _instance;
		}
	}

	public string BTS_GAME_ID = string.Empty;

	private enum ServerType{
		ReleaseServerUrl,
		TestServerUrl
	};

	private const ServerType SERVER_TYPE = ServerType.ReleaseServerUrl;

	void Awake () {
		_instance = Resources.Load<BTS_Settings> (SETTINGS_NAME);
	}

	public static string GetServerUrl () {
#if UNITY_EDITOR
        return BTS_Config.RELEASE_SERVER_URL;
#endif
        switch (SERVER_TYPE) {

			case ServerType.ReleaseServerUrl:
				return BTS_Config.RELEASE_SERVER_URL;

			case ServerType.TestServerUrl:
				return BTS_Config.TEST_SERVER_URL;
		}

		return BTS_Config.TEST_SERVER_URL;
	}

	public static string GetGameID () {
		return Instance.BTS_GAME_ID;
	}
}