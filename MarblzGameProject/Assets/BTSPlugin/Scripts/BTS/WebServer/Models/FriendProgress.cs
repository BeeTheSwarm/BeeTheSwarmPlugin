using UnityEngine;
using System;
using System.Collections;

namespace BTS {

internal class FriendProgress  {

	public int TrackNumber;
	public string PlayerId;
	public long TrackTime;

	public string FormatedTime {
		get {
			return ConvertGoogleScoreToTime(TrackTime);
		}
	}

	private string ConvertGoogleScoreToTime(long score) {
		TimeSpan t = TimeSpan.FromMilliseconds(score);
		string _time  = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Minutes, t.Seconds, t.Milliseconds);
		
		return _time;
	}
}
}
