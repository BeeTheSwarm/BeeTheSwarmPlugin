using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpView : BaseControlledView<ISignUpViewListener>, ISignUpView
{
    [SerializeField]
    private InputField m_emailInput;
    [SerializeField]
    private Button m_backButton;

    private void Awake() {
        m_backButton.onClick.AddListener(BackButtonClick);
    }

    private void BackButtonClick() {
        m_controller.BackClicked();
    }

    public void ClearInput() {
        m_emailInput.text = string.Empty;
    }

    public void Setup(bool isStandalone) {
        m_backButton.gameObject.SetActive(!isStandalone);
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
