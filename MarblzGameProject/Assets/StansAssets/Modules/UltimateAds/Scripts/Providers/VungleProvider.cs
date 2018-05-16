//#define VUNGLE_ENABLED

////////////////////////////////////////////////////////////////////////////////
//  
// @module Ultimate Ads
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
// @website https://stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.UltimateAds {
	internal class VungleProvider : IVideoAd {

		private VungleNetwork _network;

		private string appID;
		private string iosAppID;
		private string androidAppID;
		private string windowsAppID;

		private string _placementID;

		private Action _OnVideoLoaded = delegate {};
		private Action<bool> _OnFinished = delegate {};
		private Action<bool> _OnDownloadClicked = delegate {};

		private bool _inited = false;
		private bool _isVideoReady = false;

		List<string> placements = new List<string> ();

		public VungleProvider(VungleNetwork network){
			_network = network;
		}

		public void Init() {
			if (_inited) return;

			Debug.Log (_network.Name);
			placements.Clear ();

			#if VUNGLE_ENABLED

				#if UNITY_ANDROID || UNITY_EDITOR
					appID = _network.AndroidAppId;

					foreach(ANVunglePlacement placement in ANPlacements) {
						placements.Add(placement.ID);
					}			
				#elif UNITY_IPHONE || UNITY_EDITOR
					appID = _network.iOSAppId;

					foreach(IOSVunglePlacement placement in IOSPlacements) {
						placements.Add(placement.ID);
					}	
				#elif (UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO) || UNITY_EDITOR
					appID = _network.WinAppId;

					foreach(WinVunglePlacement placement in WinPlacements) {
						placements.Add(placement.ID);
					}	
				#endif

				string[] array = placements.ToArray();

				Vungle.adPlayableEvent += Vungle_adPlayableEvent;
				Vungle.onAdStartedEvent += Vungle_onAdStartedEvent;
				Vungle.onAdFinishedEvent += Vungle_onAdFinishedEvent;

				if(array.Length == 0)
					Debug.Log ("You didn't specify Vungle placements for choosen platform");

				Vungle.init(appID, array);

				_inited = true;
				Debug.Log (string.Format("Vungle Init: {0} | {1} | {2}", _network.AndroidAppId, _network.iOSAppId, _network.WinAppId));
			#endif
		}

		public void Load() {
			_isVideoReady = false;
		}

		public bool IsVideoReady () {
			return _isVideoReady;
		}

		public bool Show() {
			#if VUNGLE_ENABLED
				if (_isVideoReady) {
					Dictionary<string, object> options = new Dictionary<string, object> ();

					#if UNITY_ANDROID
						options.Add ("orientation", VungleAdOrientation.MatchVideo);
					#elif UNITY_IOS
						//set to true for matchVideo and false for autoRotate.
						options.Add ("orientation", true);
					#endif
						options.Add ("immersive", true);

					Vungle.playAd(options, _placementID);

					return true;
				}
			#endif

			return false;
		}

		#if VUNGLE_ENABLED
			private void Vungle_adPlayableEvent (string placementID, bool adPlayable)
			{			
				if (adPlayable) {
					_placementID = placementID;
					_isVideoReady = true;
					_OnVideoLoaded ();
				} else {
					Debug.Log ("Vungle video ad failed to load");
				}
			}

			private void Vungle_onAdFinishedEvent (string placementID, AdFinishedEventArgs args)
			{
				_isVideoReady = true;
				_OnDownloadClicked (args.WasCallToActionClicked);
				_OnFinished (args.IsCompletedView);
			}

			private void Vungle_onAdStartedEvent (string placementID)
			{
				_isVideoReady = false;
			}

			public List<ANVunglePlacement> ANPlacements {
				get {
					return _network.ANPlacements;
				}
			}

			public List<IOSVunglePlacement> IOSPlacements {
				get {
					return _network.IOSPlacements;
				}
			}

			public List<WinVunglePlacement> WinPlacements {
				get {
					return _network.WinPlacements;
				}
			}
		#endif

		public Action OnLoaded {
			get {
				return _OnVideoLoaded;
			}
			set {
				_OnVideoLoaded = value;
			}
		}

		public Action<bool> OnFinished {
			get {
				return _OnFinished;
			}
			set {
				_OnFinished = value;
			}
		}

		public Action<bool> OnDownloadclicked {
			get {
				return _OnDownloadClicked;
			}
			set {
				_OnDownloadClicked = value;
			}
		}
	}
}
