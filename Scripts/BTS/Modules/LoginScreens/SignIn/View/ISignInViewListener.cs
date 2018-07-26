using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISignInViewListener : IViewEventListener
{
    void OnSignInClick(string text1, string text2);
    void OnRegisterClick();
    void OnForgotPasswordClick();
    void OnBackClick();
}
