using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegistrationController : IPopupController {
    void ShowConfirmationPage(string login);
    void ShowRegistrationPage(string email);
}
