using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddFriendCodePopupViewListener : IViewEventListener {
    void OnBackClicked();
    void OnAddClicked();
}
