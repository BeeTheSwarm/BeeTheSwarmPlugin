using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

    internal class BTS_GetGamesList : BTS_BasePackage<GetGamesResponse> {

        public const string PackId = "GetGames";
        public BTS_GetGamesList() : base(PackId) {

        }

        public override Int32 Timeout {
            get {
                return 5;
            }
        }
    }
}