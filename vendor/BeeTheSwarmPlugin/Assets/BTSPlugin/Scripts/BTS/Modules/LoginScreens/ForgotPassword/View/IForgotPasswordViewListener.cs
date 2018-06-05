using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IForgotPasswordViewListener : IViewEventListener {
    void SetPassword(int code, string password, string confirmPassword);
    void BackPressed();
    void SendCode(string emailInputText);
}
