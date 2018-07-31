//#define SA_DEBUG_MODE
////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin
// @author Osipov Stanislav (Stan's Assets) 
// @support support@stansassets.com
// @website https://stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections;
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
using System.Runtime.InteropServices;
#endif



public class IOSNativeUtility : SA.Common.Pattern.Singleton<IOSNativeUtility> {


	#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
	[DllImport ("__Internal")]
	private static extern void _ISN_CopyToClipboard(string text);
	#endif

	public static void CopyToClipboard(string text) {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
		_ISN_CopyToClipboard(text);
		#endif
	}

    


}
