using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegistrationView : IControlledView
{
    void ShowConfirmationPage();
    void ShowError(string errorMessage);
    void SetViewModel(RegistrationScreenViewModel m_viewModel);
    void ShowRegistrationPage();
}
