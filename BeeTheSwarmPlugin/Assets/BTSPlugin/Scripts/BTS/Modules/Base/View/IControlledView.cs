using UnityEngine;
using System.Collections;
using System;

public interface IControlledView: IView
{
    void SetListener(IViewEventListener controller);
    void Show();
    void Hide();
}
