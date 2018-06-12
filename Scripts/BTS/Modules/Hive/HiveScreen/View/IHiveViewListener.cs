using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHiveViewListener : IViewEventListener {
    void AddToHiveClicked();
    void ViewAllClicked();
    void RefferedByFriendClicked();
}
