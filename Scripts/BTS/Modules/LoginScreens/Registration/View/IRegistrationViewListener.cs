using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegistrationViewListener : IViewEventListener
{
    void OnCloseClick();
    void OnRegisterClick(string email, string password, string repeatPassword, string name, string referal);
    void OnResendEmailClick();
    void OnSubmitClick(string text);
    void OnBackClick();
}
