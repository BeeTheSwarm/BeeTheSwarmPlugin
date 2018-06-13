using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISignUpViewListener : IViewEventListener
{
    void OnSignInClick();
    void OnSignUpClick(string text);
    void BackClicked();
}
