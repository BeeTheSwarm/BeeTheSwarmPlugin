using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BTS;

public class BTS_Settings : ScriptableObject {
     
	private enum ServerType{
		ReleaseServerUrl,
		TestServerUrl
	};

	private const ServerType SERVER_TYPE = ServerType.ReleaseServerUrl;
    
	public static string GetServerUrl () {
        string url = SERVER_TYPE == ServerType.ReleaseServerUrl ? BTS_Config.RELEASE_SERVER_URL : BTS_Config.TEST_SERVER_URL;
		return url;
	}

}