using System;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    public class GetGamesResponse : PackageResponse {
        public List<GameModel> Games { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Games = new List<GameModel>();
            var gamesSource = (List<object>)data["games"];
            foreach (object gameItem in gamesSource) {
                    GameModel game = new GameModel();
                    game.ParseJSON((Dictionary<string, object>)gameItem);
                    Games.Add(game);
            }
        }
    }

}
