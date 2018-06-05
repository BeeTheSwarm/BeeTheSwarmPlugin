using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IYesNoPopupViewListener : IViewEventListener
{
    void OnDelete();
    void OnCancel();
}
