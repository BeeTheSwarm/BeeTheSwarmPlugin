using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGreetingViewListener : IViewEventListener {
    void OnPopupShown();
}
