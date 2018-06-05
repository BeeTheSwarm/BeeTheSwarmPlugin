using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoaderController : IPopupController {
    void Show(string message);
    void Hide();
}
