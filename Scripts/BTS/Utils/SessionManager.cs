using UnityEngine;
using System.Collections;

namespace BTS {
public class SessionManager  {

	public const int UNDEFINED_SESSION = -1;

	private static int _SessionId = -1;


	//--------------------------------------
	// Public Methods
	//--------------------------------------

	public static void SetSessionId(int sessionId) {
		_SessionId = sessionId;
	}


	//--------------------------------------
	// Get / Set
	//--------------------------------------

	public static int SessionId {
		get {
			return _SessionId;
		}
	}

	public static bool SessionUndefined {
		get {
			return SessionId == UNDEFINED_SESSION;
		}
	}
}
}
