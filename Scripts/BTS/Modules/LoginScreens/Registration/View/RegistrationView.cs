using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationView : BaseControlledView<IRegistrationViewListener>, IRegistrationView {
    [Header("Registation Form")] [SerializeField]
    private GameObject m_registrationForm;
    [SerializeField] private InputField m_emailInput;
    [SerializeField] private InputField m_passwordInput;
    [SerializeField] private InputField m_repeatPasswordInput;
    [SerializeField] private InputField m_referralCodeInput;
    [SerializeField] private InputField m_usernameInput;
    [SerializeField] private Text m_errorText;
    [SerializeField] private GameObject m_rightsScreen;
    [SerializeField] private ScrollRect m_rightsScreenScroll;
    [Header("Confirmation Form")] [SerializeField]
    private GameObject m_confirmationForm;
    [SerializeField] private InputField m_verificationCodeInput;
    [SerializeField] private GameObject m_changeEmailScreen;

    private RegistrationScreenViewModel m_viewModel;

    public void OnCloseRegistrationFormClicked() {
        if (m_rightsScreen.activeInHierarchy) {
            m_rightsScreen.SetActive(false);
        }
        else {
            m_controller.OnCloseClick();
        }
    }

    public void OnRegisterClicked() {
        m_controller.OnRegisterClick(m_emailInput.text, m_passwordInput.text, m_repeatPasswordInput.text, m_usernameInput.text, m_referralCodeInput.text);
    }

    public void OnRightsClicked() {
        m_rightsScreen.SetActive(true);
        m_rightsScreenScroll.verticalNormalizedPosition = 1;
    }

    public void OnBackClicked() {
        m_controller.OnBackClick();
    }

    public void OnResendMailClicked() {
        m_controller.OnResendEmailClick();
    }

    public void OnSubmitClicked() {
        m_controller.OnSubmitClick(m_verificationCodeInput.text);
    }

    public void OnChangeEmailClicked() {
        m_changeEmailScreen.SetActive(true);
    }

    public void OnChangeEmailCancelClicked() {
        m_changeEmailScreen.SetActive(false);
    }

    public void ShowConfirmationPage() {
        m_registrationForm.SetActive(false);
        m_confirmationForm.SetActive(true);
    }

    public void ShowError(string errorMessage) {
        m_errorText.text = errorMessage;
    }

    public void SetViewModel(RegistrationScreenViewModel viewModel) {
        m_viewModel = viewModel;
    }

    public void ShowRegistrationPage() {
        m_registrationForm.SetActive(true);
        m_confirmationForm.SetActive(false);
        m_emailInput.text = m_viewModel.Email;
        m_passwordInput.text = string.Empty;
        m_repeatPasswordInput.text = string.Empty;
        m_referralCodeInput.text = string.Empty;
        m_usernameInput.text = string.Empty;
        m_verificationCodeInput.text = string.Empty;
    }
}