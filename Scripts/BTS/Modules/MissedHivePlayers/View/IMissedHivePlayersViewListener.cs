using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMissedHivePlayersViewListener : IViewEventListener {
    void AddToHiveClicked();
    void ViewAllClicked();
    void RefferedByFriendClicked();
}
