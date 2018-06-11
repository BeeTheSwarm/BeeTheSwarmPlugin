using UnityEngine;
using System.Collections;

namespace BTS {

    internal class HiveLeaderboardItemViewModel {
        public float Impact { get; private set; }
        public string UserName { get; private set; }
        public int HiveId { get; private set; }
        public int Place { get; set; }

        public readonly Observable<Sprite> Avatar = new Observable<Sprite>();

        internal HiveLeaderboardItemViewModel(string userName, int hiveId, float impact) {
            UserName = userName;
            HiveId = hiveId;
            Impact = impact;
        }
    }

}