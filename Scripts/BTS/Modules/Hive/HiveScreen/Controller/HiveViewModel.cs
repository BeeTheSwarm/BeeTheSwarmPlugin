using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    internal class HiveViewModel {
        public int HiveId;
        public readonly Observable<int> Members = new Observable<int>();
        public readonly Observable<int> RankByMembers = new Observable<int>();
        public readonly Observable<float> Impact = new Observable<float>();
        public readonly Observable<int> RankByImpact = new Observable<int>();
        public readonly Observable<Sprite> ReffererAvatar = new Observable<Sprite>();
        public readonly Observable<bool> HasRefferer = new Observable<bool>();
        public readonly ObservableList<HiveMemberViewModel> MembersList = new ObservableList<HiveMemberViewModel>();

        public void Reset() {
            Members.Set(0);
            RankByMembers.Set(0);
            Impact.Set(0);
            RankByImpact.Set(0);
            ReffererAvatar.Set(null);
            HasRefferer.Set(false);
            MembersList.Clear();
        }
    }
}
