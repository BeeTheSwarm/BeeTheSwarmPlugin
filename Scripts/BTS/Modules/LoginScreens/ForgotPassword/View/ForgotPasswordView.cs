using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    public class ForgotPasswordView : BaseControlledView<IForgotPasswordViewListener>, IForgotPasswordView {
        
        [SerializeField] private GameObject m_codeScreen;
        [SerializeField] private InputField m_emailInput;
        [SerializeField] private Button m_nextButton;
        [SerializeField] private Button m_backButton;
        [SerializeField] private Button m_confirmationViewBackButton;

        [SerializeField] private GameObject m_passwordsScreen;
        [SerializeField] private InputField m_codeInputField;
        [SerializeField] private InputField m_newPasswordInputField;
        [SerializeField] private InputField m_confirmPasswordInputField;
        [SerializeField] private Button m_doneButton;

        private void Awake() {
            m_nextButton.onClick.AddListener(NextBtnClickHandler);
            m_backButton.onClick.AddListener(BackBtnClickHandler);
            m_doneButton.onClick.AddListener(DoneBtnClickHandler);
            m_confirmationViewBackButton.onClick.AddListener(ConfirmationBackBtnClickHandler);
        }

        private void ConfirmationBackBtnClickHandler() {
            ResetScreens(false);
        }

        private void DoneBtnClickHandler() {
            m_controller.SetPassword(int.Parse(m_codeInputField.text), m_newPasswordInputField.text, m_confirmPasswordInputField.text);
        }

        private void BackBtnClickHandler() {
            m_controller.BackPressed();
        }

        private void NextBtnClickHandler() {
            m_controller.SendCode(m_emailInput.text);
        }

        public void Clear() {
            ResetScreens(true);
        }

        private void ResetScreens(bool clearEmail) {
            m_codeScreen.SetActive(true);
            m_passwordsScreen.SetActive(false);
            if (clearEmail) {
                m_emailInput.text = string.Empty;
            }
            m_codeInputField.text = string.Empty;
            m_newPasswordInputField.text = string.Empty;
            m_confirmPasswordInputField.text = string.Empty;
        }

        public void ShowPasswordsPage() {
            m_codeScreen.SetActive(false);
            m_passwordsScreen.SetActive(true);
        }
    }
}