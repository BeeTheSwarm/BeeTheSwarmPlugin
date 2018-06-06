using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBadgeAwardPopupViewListener : IViewEventListener
{
    void OnFeedClick();
    void OnCloseClick();
}
