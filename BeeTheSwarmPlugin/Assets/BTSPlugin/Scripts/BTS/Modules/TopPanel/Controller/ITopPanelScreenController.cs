using UnityEngine;
using System.Collections;
using System;

public interface ITopPanelController : IViewController {
    event Action OnBackBtnPressed;
    event Action OnAvatarPressed;
    void SetAvatarEnabled(bool value);
    void SetBackButtonEnabled(bool value);
}
