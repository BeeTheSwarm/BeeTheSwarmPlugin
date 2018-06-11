using UnityEngine;
using System.Collections;

namespace BTS {

    internal class HiveLeaderboaderItemViewModel {
        public float Impact { get; private set; }
        public string UserName { get; private set; }

        internal HiveLeaderboaderItemViewModel(string userName, float impact) {
            UserName = userName;
            Impact = impact;
        }
    }

}