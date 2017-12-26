using UnityEngine;
using UnityEngine.UI;
using System;

namespace BTS {
	
	public class BTS_LoginResetController : MonoBehaviour {

		//Forgot email
		[Header("Forgot email screen")] 
		[SerializeField] private InputField _phoneNumberInput;

		[SerializeField] private InputField _phoneCodeInput;
		[SerializeField] private InputField _newEmailInput;
		[SerializeField] private InputField _confirmEmailInput;
	
		//Forgot password
		[Header("Forgot password screen")] 
		[SerializeField] private InputField _EmailInput;
		
		[SerializeField] private InputField _emailCodeInput;
		[SerializeField] private InputField _newPasswordInput;
		[SerializeField] private InputField _confirmPasswordInput;

		Animator _animator;
		private const string ANIMATOR_LOGIN_TRIGGER = "LoginFadeIn";
		private const string ANIMATOR_FORGOT_EMAIL_TRIGGER = "ForgotEmail";
		private const string ANIMATOR_FORGOT_PASSWORD_TRIGGER = "ForgotPassword";
		private const string ANIMATOR_CONFIRM_EMAIL_TRIGGER = "ConfirmEmail";
		private const string ANIMATOR_CONFIRM_PASSWORD_TRIGGER = "ConfirmPassword";

		private const string contactUsEmail = "info@beetheswarm.com";
		private const string emailSubject = "";
		private const string emailBody = "";
		
		private const int codeLowLimit = 4;
		private const int passwordLowLimit = 8;
		
		//Cache
		private UInt64 phone = 0;
		private string _code = "";
		private string _newEmail = "";
		private string _password = "";
		private string _confirmPassword = "";
		private string _confirmEmail = "";
		
		void Awake () {
			DontDestroyOnLoad (gameObject);
			_EmailInput.text = BTS_PlayerData.Instance.StoredLogin.Replace ("|","");
		}

		void OnEnable () {
			BTS_WebServerManager.OnResetEmailSuccessful       += OnResetEmailSuccessfulHandler;
			BTS_WebServerManager.OnResetEmailFail             += OnResetEmailFailHandler;
			BTS_WebServerManager.OnReassignEmailSuccessful    += OnReassignEmailSuccessfulHandler;
			BTS_WebServerManager.OnReassignEmailFail          += OnReassignEmailFailHAndler;
		
			BTS_WebServerManager.OnResetPasswordSuccessful    += OnResetPasswordSuccessfulHandler;
			BTS_WebServerManager.OnResetPasswordFail          += OnResetPasswordFailHanler;
			BTS_WebServerManager.OnReassignPasswordSuccessful += OnReassignPasswordSuccessfulHandler;
			BTS_WebServerManager.OnReassignPasswordFail       += OnReassignPasswordFailHandler;

			BTS_InterstitialController.onEmailValueChanged    += OnEmailValueChangedHandler;
		}

		void OnDisable () {
			BTS_WebServerManager.OnResetEmailSuccessful       -= OnResetEmailSuccessfulHandler;
			BTS_WebServerManager.OnResetEmailFail             -= OnResetEmailFailHandler;
			BTS_WebServerManager.OnReassignEmailSuccessful    -= OnReassignEmailSuccessfulHandler;
			BTS_WebServerManager.OnReassignEmailFail          -= OnReassignEmailFailHAndler;
			
			BTS_WebServerManager.OnResetPasswordSuccessful    -= OnResetPasswordSuccessfulHandler;
			BTS_WebServerManager.OnResetPasswordFail          -= OnResetPasswordFailHanler;
			BTS_WebServerManager.OnReassignPasswordSuccessful -= OnReassignPasswordSuccessfulHandler;
			BTS_WebServerManager.OnReassignPasswordFail       -= OnReassignPasswordFailHandler;
			
			BTS_InterstitialController.onEmailValueChanged    -= OnEmailValueChangedHandler;
		}
		
		void Start () {
			_animator = GetComponent<Animator> ();

		}
		
		public void ContactUs() {
			Application.OpenURL(string.Format("mailto:{0}?subject={1}&body={2}", contactUsEmail, emailSubject, emailBody));
		}

		// Forgot Email
		//--------------------------------------

		public void SendCodeOnPhone() {
			bool isInputValid = true;
			Action errorHandleAction;
			
			//PHONE NUMBER
			errorHandleAction = () => {
				isInputValid = false;

				_phoneNumberInput.text = "";
				_phoneNumberInput.placeholder.color = Color.red;
				ShowLog("Input data is invalid!");
			};
			try {
				if (_phoneNumberInput.text.Length > 0) {
					string phoneNumber = _phoneNumberInput.text.Replace(" ", "").Replace("+", "");
					phone = UInt64.Parse (phoneNumber);
				} else {
					errorHandleAction (); 
				}
			} catch (Exception exc) {
				errorHandleAction ();
			}

			if (isInputValid) {
				Debug.Log("input valid ok");
				new BTS_ResetEmail(phone).Send();
			}
		}
		
		public void OnConfirmEmailButtonClick() {
			bool isInputValid = true;
			Action errorHandleAction;

			//CODE
			errorHandleAction = () => {
				isInputValid = false;

				_phoneCodeInput.text = "";
				_phoneCodeInput.placeholder.color = Color.red;
			};
			try {
				if (_phoneCodeInput.text.Length >= codeLowLimit ) {
					_code = _phoneCodeInput.text;
				}
			}
			catch (Exception exc) {
				errorHandleAction();
			}

			//EMAIL
			errorHandleAction = () => {
				isInputValid = false;

				_newEmailInput.text = "";
				_newEmailInput.placeholder.color = Color.red;
			};
			try {
				if (_newEmailInput.text.Length > 0 && _newEmailInput.text.Contains ("@") && _newEmailInput.text.Contains (".")) {
					_newEmail = _newEmailInput.text;
				} else {
					errorHandleAction ();
				}
			}catch (Exception exc) {
				errorHandleAction ();
			}
			
			//CONFIRM EMAIL
			errorHandleAction = () => {
				isInputValid = false;

				_confirmEmailInput.text = "";
				_confirmEmailInput.placeholder.color = Color.red;
			};
			try {
				if (_confirmEmailInput.text == _newEmail) {
					;
				} else {
					errorHandleAction();
				}
			}
			catch (Exception exs) {
				errorHandleAction();
			}

			if (isInputValid) {
				Debug.Log("input valid ok");
				int code = Int32.Parse(_code);
				new BTS_ReassignEmail(code, _newEmail).Send();
			} else {
				ShowLog("Check Input Fields!");
			}
		}
		
		// Forgot Password
		//--------------------------------------

		public void OnSendCodeOnEmail() {

			if (_EmailInput.text.Length > 0 && _EmailInput.text.Contains("@") && _EmailInput.text.Contains(".")) {
				string login = _EmailInput.text;
				BTS_PlayerData.Instance.SaveLogin (login, BTS_PlayerData.Instance.StoredAuthToken, true);
				new BTS_ResetPassword(login).Send();
			} else {
				_EmailInput.text = "";
				_EmailInput.placeholder.color = Color.red;
				ShowLog("Input data is invalid!");
			}
		}

		public void OnConfirmPasswordButtonClick() {
			bool isInputValid = true;
			Action errorHandleAction;

			//CODE
			errorHandleAction = () => {
				isInputValid = false;

				_emailCodeInput.text = "";
				_emailCodeInput.placeholder.color = Color.red;
			};
			try {
				if (_emailCodeInput.text.Length >= codeLowLimit ) {
					_code = _emailCodeInput.text;
				}
			}
			catch (Exception exc) {
				errorHandleAction();
			}

			//PASSWORD
			errorHandleAction = () => {
				isInputValid = false;

				_newPasswordInput.text = "";
				_newPasswordInput.placeholder.color = Color.red;
				ShowLog("Password should be at least 8 characters!");
			};
			try {
				if (_newPasswordInput.text.Length >= passwordLowLimit) {
					_password = _newPasswordInput.text;
				} else {
					errorHandleAction ();
				}
			}catch (Exception exc) {
				errorHandleAction ();
			}
			
			
			//CONFIRM PASSWORD
			errorHandleAction = () => {
				isInputValid = false;

				_confirmPasswordInput.text = "";
				_confirmPasswordInput.placeholder.color = Color.red;
			};
			try {
				if (_confirmPasswordInput.text == _password) {
					;
				} else {
					errorHandleAction();
				}
			}
			catch (Exception exs) {
				errorHandleAction();
			}

			if (isInputValid) {
				Debug.Log("input valid ok");
				int code = Int32.Parse(_code);
				new BTS_ReassignPassword(code, _password).Send();
			}
		}

		public void OnBackFromEmailButtonClick() {
			_animator.SetTrigger(ANIMATOR_FORGOT_EMAIL_TRIGGER);
		}

		public void OnBackFromPasswordButtonClick() {
			_animator.SetTrigger(ANIMATOR_FORGOT_PASSWORD_TRIGGER);
		}

		// Input fields
		//--------------------------------------

		public void OnEmailCodeEndEdit() {
			_code = _emailCodeInput.text;
		}

		public void OnPhoneCodeEndEdit() {
			_code = _phoneCodeInput.text;
		}

		public void OnNewEmailEndEdit() {
			_newEmail = _newEmailInput.text;
		}

		public void OnConfirmEmailEndEdit() {
			_confirmEmail = _confirmEmailInput.text;
		}

		public void OnPasswordEndEdit() {
			_password = _newPasswordInput.text;
		}

		public void OnConfirmPasswordEndEdit() {
			_confirmPassword = _confirmPasswordInput.text;
		}


		// Forgot screen
		private void ResetForgotInputs() {
			_phoneNumberInput.text = "";
			_phoneCodeInput.text = "";
			_newEmailInput.text = "";
			_confirmEmailInput.text = "";

			_emailCodeInput.text = "";
			_newPasswordInput.text = "";
			_confirmPasswordInput.text = "";
		}

		public void OnPhoneNumberEndEdit() {
			
		}

		public void OnEmailEndEdit() {
		
		}

		private void OnEmailValueChangedHandler(string email) {
			_EmailInput.text = BTS_PlayerData.Instance.StoredLogin.Replace ("|","");
		}

		//------------------------------------------------------------
		//Private functions
		//------------------------------------------------------------
		
		private void ShowLog (string message) {

			BTS_Manager.Instance.Popup.ShowError (message);
		}
		
		//------------------------------------------------------------
		// Handler
		//------------------------------------------------------------
		
		private void OnResetEmailSuccessfulHandler() {
			ResetForgotInputs();
			_animator.SetTrigger(ANIMATOR_CONFIRM_EMAIL_TRIGGER);
		}

		private void OnResetEmailFailHandler(string error) {
			ResetForgotInputs();
		}

		private void OnReassignEmailSuccessfulHandler() {
			BTS_PlayerData.Instance.SaveLogin(_newEmail, true);
			BTS_Manager.OnResetEmail(_newEmail);
			_EmailInput.text = BTS_PlayerData.Instance.StoredLogin.Replace ("|","");
			_animator.SetTrigger(ANIMATOR_LOGIN_TRIGGER);
			ShowLog("Email successfuly changed!");

			ResetForgotInputs();
		}

		private void OnReassignEmailFailHAndler(string error) {
			ResetForgotInputs();
		}

		private void OnResetPasswordSuccessfulHandler() {
			ResetForgotInputs();
			_animator.SetTrigger(ANIMATOR_CONFIRM_PASSWORD_TRIGGER);
		}

		private void OnResetPasswordFailHanler(string error) {
			ResetForgotInputs();
		}

		private void OnReassignPasswordSuccessfulHandler() {
			ResetForgotInputs();
			_animator.SetTrigger(ANIMATOR_LOGIN_TRIGGER);
			ShowLog("Password successfuly changed!");
		}

		private void OnReassignPasswordFailHandler(string error) {
			ResetForgotInputs();
		}
	}
}