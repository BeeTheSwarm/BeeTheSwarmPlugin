using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IYesNoPopupView : IControlledView {
    void SetYesButtonLabel(string yesButtonLabel);
    void SetMessage(string message);
    void SetNoButtonLabel(string noButtonLabel);
}
