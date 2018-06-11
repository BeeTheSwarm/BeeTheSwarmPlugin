using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddFriendCodePopupController : IPopupController
{
    void Show(Action<string> callback);
}
