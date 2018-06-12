using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    internal class HiveLeaderboardViewModel {
        public int HiveOwner;
        public readonly ObservableList<HiveLeaderboaderItemViewModel> MembersList = new ObservableList<HiveLeaderboaderItemViewModel>();
    }
}
