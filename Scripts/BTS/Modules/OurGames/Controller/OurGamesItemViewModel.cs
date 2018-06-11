using System;
using UnityEngine;

namespace BTS.OurGames.Controller {
    public class OurGamesItemViewModel {
        public readonly Observable<Sprite> GameIcon = new Observable<Sprite>();
        public readonly Observable<string> GameName = new Observable<string>();
        public readonly Observable<string> GameUrl = new Observable<string>();

        public event Action OnClick = delegate {  };
        public void Download() {
            OnClick.Invoke();
        }
    }
}