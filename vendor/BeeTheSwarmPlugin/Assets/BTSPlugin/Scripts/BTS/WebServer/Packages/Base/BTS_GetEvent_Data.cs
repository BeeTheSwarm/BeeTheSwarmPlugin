using System.Collections.Generic;

namespace BTS {
	[System.Serializable]
	public class BTS_GetEvent_Data {
        
		public BTS_Level[] events;

		public List<BTS_EventReward> progress;
	}
    
	[System.Serializable]
	public class BTS_Level {

		public string title;
		public long refresh;
		public BTS_RewardByScore[] rewards;
	}

	[System.Serializable]
	public class BTS_RewardByScore {

		public int score;
		public int reward;
	}
    
	[System.Serializable]
	public class BTS_EventReward {

		public int score;
		public int reward;
		public string title;
	}
}
