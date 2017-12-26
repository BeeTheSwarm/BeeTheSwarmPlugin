using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	public class BTS_Player {

		public static Action 				OnPlayerloaded = delegate{};

		public static Action<BTS_Player> 	OnLevelUp = delegate{};
		public static Action<int, bool> 	OnBeesEarned = delegate{};
		
		private int 	_btsID;
		private string  _name;
		private string  _email;
		private string  _phone;
		private int 	_btsBees;
		private int 	_btsLevel;
		private int 	_btsLevelProgress;
		private int 	_btsRewardBees;
		
		private string 	_avatarURL;
		private string  _refCode;
		private string  _imageUrl;
		private string  _ImagePreviewUrl;
		private string  _authToken;

		private const string BTS_ID 			= "BTS_ID";
		private const string BTS_LEVEL_PROGRESS = "BTS_LEVEL_PROGRESS";
		private const string BTS_LEVEL 			= "BTS_LEVEL";
		private const string BTS_BEES 			= "BTS_BEES";

		private IPlayerDataLoader _DataLoader = null;
		private BTS_GetEvent_Data _eventData;
		private Dictionary<int, string> _btsEvent_Keys = new Dictionary<int, string>();

		//--------------------------------------
		//Get/Set
		//--------------------------------------

		public int BtsID {
			get {
				return _btsID;
			}
		}
		
		public string Name {
			get {
				return _name;
			}
		}

		public string Email {
			get {
				return _email;
			}
		}

		public string Phone {
			get {
				return _phone;
			}
		}

		public int Bees {
			get {
				return _btsBees;
			}
		}
		
		public int Level {
			get {
				return _btsLevel;
			}
		}
		
		public int LevelProgress {
			get {
				return _btsLevelProgress;
			}
		}

		public int RewardBees {
			get {
				return _btsRewardBees;
			}
		}

		public string AvatarURL {
			get {
				return _avatarURL;
			}
		}

		public string RefCode {
			get {
				return _refCode;
			}
		}

		public BTS_GetEvent_Data EventData {
			get {
				return _eventData;
			}
		}

		//--------------------------------------
		//Public functions
		//--------------------------------------

		internal BTS_Player () {}

		internal BTS_Player (BTS_Player player) {
				this._btsID 			= player.BtsID;
				this._name 		        = player.Name;
				this._email 			= player.Email;
				this._phone 			= player.Phone;
				this._btsBees 			= player.Bees;
				this._btsLevel 			= player.Level;
				this._btsLevelProgress 	= player.LevelProgress;
				this._btsRewardBees     = player.RewardBees;
				this._avatarURL 		= player.AvatarURL;
			    this._refCode           = player._refCode;
		}

		internal BTS_Player (IPlayerDataLoader loader) {
			_DataLoader = loader;

			_btsID 				= loader.HasKey (BTS_ID) 				?	loader.GetInt (BTS_ID) 				: -1;
			_btsLevelProgress	= loader.HasKey (BTS_LEVEL_PROGRESS) 	?	loader.GetInt (BTS_LEVEL_PROGRESS) 	: 0;
			_btsLevel 			= loader.HasKey (BTS_LEVEL) 			? 	loader.GetInt (BTS_LEVEL) 			: 1;
			_btsBees 			= loader.HasKey (BTS_BEES)  			?	loader.GetInt (BTS_BEES)			: 0;

			OnPlayerloaded ();
		}

		internal void MergeStats (int id, string name, string email, int bees, int level, int levelProgress) {

			_btsID 				= id;
			_name 			    = name;
			_email 				= email;
			_btsBees 			= bees;
			_btsLevel 			= level;
			_btsLevelProgress 	= levelProgress;
		}

		internal void MergeStats (int id, string name, string email, int bees, int level, int levelProgress, int reward) {

			_btsID 				= id;
			_name 			    = name;
			_email 				= email;
			_btsBees 			= bees;
			_btsLevel 			= level;
			_btsLevelProgress 	= levelProgress;
			_btsRewardBees 		= reward;

		}
		
		internal void MergeStats (int id, string name, string email, int bees, int level, int levelProgress, string  refCode) {

			_btsID 				= id;
			_name 			    = name;
			_email 				= email;
			_btsBees 			= bees;
			_btsLevel 			= level;
			_btsLevelProgress 	= levelProgress;
			_refCode 			= refCode;

		}
		
		internal void UpdateStats (int bees, int level, int levelProgress) {
			if (level > _btsLevel) {
				_btsLevel = level;
				OnLevelUp (this);
			}

			if (bees != _btsBees) {
				OnBeesEarned (bees - _btsBees, true);
				_btsBees = bees;
			}

			_btsLevelProgress 	= levelProgress;

		}

		internal void UpdateBTSStatsEvent (BTS_GetEvent_Data data) {
			_eventData = data;
			_btsEvent_Keys.Clear();
			for (int i = 0; i < _eventData.events.Length; i ++) {
				_btsEvent_Keys.Add(i, _eventData.events[i].title);
			}
		}

		internal void SetAvatarURL (string url) {
			this._avatarURL = url;
		}
		
		internal string GetEventKey(int level) {
			if (_btsEvent_Keys == null || _btsEvent_Keys.Count <= 0)
				return String.Empty;

			if (_btsEvent_Keys.ContainsKey(level - 1)) {
				return _btsEvent_Keys[level - 1];
			} else {
				return String.Empty;
			}
		}
		
		internal int GetBestScoreByLevel(int level) {
			if (_eventData.progress == null || _eventData.progress.Count == 0)
				return 0;
			int bestScore = 0;
			foreach (BTS_EventReward lev in _eventData.progress) {
				if (lev.title.Equals(GetEventKey(level))) {
					bestScore = _eventData.progress[level - 1].score;
					break;
				}
			}
			return bestScore;
		}
		
		internal int GetBeesRewardByLevel(int level) {
			if (_eventData.progress  == null || _eventData.progress .Count == 0)
				return 0;
			int reward = 0;
			foreach (BTS_EventReward lev in _eventData.progress) {
				if (lev.title.Equals(GetEventKey(level))) {
					reward = lev.reward;
					break;
				}
			}
			return reward;
		}

		internal void SavePlayerStats() {
			_DataLoader.SetInt(BTS_ID, 		_btsID);
			_DataLoader.SetInt(BTS_LEVEL, 	_btsLevel);
			_DataLoader.SetInt(BTS_BEES, 	_btsBees);
		}

		//--------------------------------------
		//Private functions
		//--------------------------------------


	}
}
