using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BTS {
    public class GetTutorialStateResponce : PackageResponse {
        public bool TutorialAvailable { get; private set; }
        public int NextTutorial { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            TutorialAvailable = bool.Parse(data["status"].ToString());
            NextTutorial = int.Parse(data["available"].ToString());
        }
    }
}