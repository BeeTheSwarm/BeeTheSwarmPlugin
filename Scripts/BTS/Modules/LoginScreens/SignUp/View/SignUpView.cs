using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpView : BaseControlledView<ISignUpViewListener>, ISignUpView
{
    [SerializeField]
    private InputField m_emailInput;

    public void ClearInput() {
        m_emailInput.text = string.Empty;
    }

    public void OnSignInClicked()
    {
        m_controller.OnSignInClick();
    }



    public void OnSignUpClicked()
    {
        m_controller.OnSignUpClick(m_emailInput.text);
    }
}
