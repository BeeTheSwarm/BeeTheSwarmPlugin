using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInView : BaseControlledView<ISignInViewListener>, ISignInView
{
    [SerializeField]
    private InputField m_emailInput;
    [SerializeField]
    private InputField m_passwordInput;
    
    public void onSignInClicked()
    {
        Debug.Log("onSignInClicked");
        m_controller.OnSignInClick(m_emailInput.text, m_passwordInput.text);
    }

    public void ClearInput() {
        m_emailInput.text = string.Empty;
        m_passwordInput.text = string.Empty;
    }
    
    public void onRegisterInClicked()
    {
        m_controller.OnRegisterClick();
    }

    public void onForgotPasswordClicked()
    {
        m_controller.OnForgotPasswordClick();
    }
}
