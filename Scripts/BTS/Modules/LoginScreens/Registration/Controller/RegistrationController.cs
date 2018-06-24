using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class RegistrationController : BasePopupController<IRegistrationView>, IRegistrationViewListener, IRegistrationController {
    [Inject] private IUserProfileService m_userService;
    [Inject] private IFeedsController m_feedsController;
    [Inject] private ISignInController m_signInController;
    [Inject] private IPopupsModel m_popupsModel;
    [Inject] private IRegisterService m_registerService;
    [Inject] private IResendCodeService m_resendCodeService;
    [Inject] private ILoadInitDataService m_loadInitDataService;
    [Inject] private ILoaderController m_loaderController;


    [Inject] private IConfirmRegistrationService m_confirmRegistrationService;

    private RegistrationScreenViewModel m_viewModel = new RegistrationScreenViewModel();

    public RegistrationController() {
    }

    protected override void PostSetView() {
        base.PostSetView();
        m_view.SetViewModel(m_viewModel);
    }
    
    public void OnCloseClick() {
        Hide();
    }

    private bool CorrectMail(string mail) {
        try {
            MailAddress mailAddress = new MailAddress(mail);
        }
        catch (FormatException exception) {
            return false;
        }

        return true;
    }

    public void OnRegisterClick(string email, string password, string repeatPassword, string name, string referal) {
        if (string.IsNullOrEmpty(email) || !CorrectMail(email)) {
            m_view.ShowError("incorrect email");
            m_popupsModel.AddPopup(new ErrorPopupItemModel("Incorrect login"));
            return;
        }

        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword)) {
            m_popupsModel.AddPopup(new ErrorPopupItemModel("Passwords could not be empty"));
            return;
        }

        if (!password.Equals(repeatPassword)) {
            m_popupsModel.AddPopup(new ErrorPopupItemModel("Passwords are not equal"));
            m_view.ShowError("passwords are not equal");
            return;
        }

        if (password.Length < 8) {
            m_popupsModel.AddPopup(new ErrorPopupItemModel("Password minimum length is 8 symbols"));
            return;
        }

        if (string.IsNullOrEmpty(referal)) {
            referal = string.Empty;
        }

        m_viewModel.Email = email;
        m_registerService.Execute(name, email, password, referal, (result) => {
            if (result) {
                m_view.ShowConfirmationPage();
            }
            else {
                m_view.ShowError("unknown error");
            }
        });
    }

    public void OnResendEmailClick() {
        m_resendCodeService.Execute(m_viewModel.Email);
    }

    public void OnBackClick() {
        Hide();
        m_signInController.Show();
    }

    public void OnSubmitClick(string text) {
        m_confirmRegistrationService.Execute(int.Parse(text), result => {
            if (result) {
                m_loaderController.Show("Loading");
                m_loadInitDataService.Execute(() => {
                    m_loaderController.Hide();
                    Hide();
                    m_feedsController.Show();
                    m_signInController.Hide();
                });
            }
        });
    }

    public void ShowRegistrationPage(string login) {
        base.Show();
        m_viewModel.Email = login;
        m_view.ShowRegistrationPage();
    }

    public void ShowConfirmationPage(string login) {
        base.Show();
        m_viewModel.Email = login;
        m_view.ShowConfirmationPage();
    }
}