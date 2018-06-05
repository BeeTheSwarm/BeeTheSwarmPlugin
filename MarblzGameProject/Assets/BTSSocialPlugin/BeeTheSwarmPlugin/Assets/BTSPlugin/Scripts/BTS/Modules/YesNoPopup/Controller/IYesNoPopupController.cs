using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IYesNoPopupController : IPopupController {
    
    void Show(string message, string yesButtonLabel, string noButtonLabel, Action<YesNoPopupResponce> callback);
}
