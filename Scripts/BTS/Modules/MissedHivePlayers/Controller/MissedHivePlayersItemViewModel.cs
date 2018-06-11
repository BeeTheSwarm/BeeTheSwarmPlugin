using UnityEngine;
using System.Collections;
using System;

namespace BTS {

    internal class MissedHivePlayersItemViewModel {
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public readonly Observable<Sprite> Avatar = new Observable<Sprite>();

        internal MissedHivePlayersItemViewModel(int userId, string userName) {
            UserName = userName;
            UserId = userId;
        }
    }

}