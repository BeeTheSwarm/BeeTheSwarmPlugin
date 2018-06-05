using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHiveLeaderboardViewListener : IViewEventListener {
    void OnMissingHivePlayerClick();
    void ScrolledToEnd();
}
