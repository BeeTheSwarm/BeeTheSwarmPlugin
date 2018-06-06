using UnityEngine;
using System.Collections;

namespace BTS {

    internal class HiveMemberViewModel {
        public float Impact { get; private set; }
        public string UserName { get; private set; }
        public int HiveId { get; private set; }
        public int Place { get; set; }

        public readonly Observable<Sprite> Avatar = new Observable<Sprite>();

        internal HiveMemberViewModel(string userName, int hiveId, float impact) {
            Place = -1;
            UserName = userName;
            HiveId = hiveId;
            Impact = impact;
        }
    }

}