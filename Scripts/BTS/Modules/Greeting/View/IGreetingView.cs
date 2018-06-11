using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGreetingView : IControlledView
{
    void ShowPopup(PopupItemModel popupItemModel);
}
