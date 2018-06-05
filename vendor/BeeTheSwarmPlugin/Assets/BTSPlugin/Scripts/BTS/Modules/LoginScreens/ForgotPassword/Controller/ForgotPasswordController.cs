using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgotPasswordController : BaseScreenController<IForgotPasswordView>, IForgotPasswordViewListener,
    IForgotPasswordController {
    [Inject] private ISignUpController m_signUpController;
    [Inject] private ISignInController m_signInController;
    [Inject] private IFeedsController m_feedsController;
    [Inject] private IPopupsModel m_popupsModel;

    [Inject] private IResetPasswordService m_resetPasswordService;
    [Inject] private IConfirmResetPassword m_confirmResetPasswordService;

    private string m_email;
    public void SetPassword(int code, string password, string confirmPassword) {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)) {
            m_popupsModel.AddPopup(new ErrorPopupItemModel("Passwords could not be empty"));
            return;
        }
        if (!password.Equals(confirmPassword)) {
            m_popupsModel.AddPopup(new ErrorPopupItemModel("Passwords are not equal"));
            return;
        }
        if (password.Length < 8) {
            m_popupsModel.AddPopup(new ErrorPopupItemModel("Password minimum length is 8 symbols"));
            return;
        }
        m_confirmResetPasswordService.OnSuccessFinish += SuccessHandler;
        m_confirmResetPasswordService.Execute(m_email, code, password, confirmPassword);
    }

    private void SuccessHandler() {
        m_confirmResetPasswordService.OnSuccessFinish -= SuccessHandler;
        Hide();
        m_signUpController.Hide();
        m_feedsController.Show();
        m_signInController.Hide();
    }

    public override bool StoredInHistory
    {
        get { return false; }
    }

    public void BackPressed() {
        Hide();
    }

    public override void Show() {
        base.Show();
        m_view.Clear();
    }

    public void SendCode(string emailInputText) {
        m_email = emailInputText;
        m_resetPasswordService.Execute(m_email, result => {
            if (result) {
                m_view.ShowPasswordsPage();
            }
        });
    }
}