using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using SA.Analytics.Google;

namespace BTS {

	public class BTS_InterstitialController : MonoBehaviour {

		public static Action OnClosed = delegate { };
		public static Action<string> onEmailValueChanged = delegate {};

		private const string ANIMATOR_FADE_IN_TRIGGER = "FadeIn";
		private const string ANIMATOR_FADE_OUT_TRIGGER = "FadeOut";
		private const string ANIMATOR_LOGIN_TRIGGER = "LoginMenuFadeIn";
		private const string ANIMATOR_FORGOTEMAIL_TRIGGER = "ForgotEmail";
		private const string ANIMATOR_FORGOTPASSWORD_TRIGGER = "ForgotPassword";
		

		[Header("SignUp")] 
		[SerializeField] private InputField _sloginInputField;

		[Header("LogIn")] 
		[SerializeField] private InputField _loginInputField;
		[SerializeField] private InputField _passwordInputField;
		[SerializeField] private GameObject _loginMenuGO;
		[SerializeField] private GameObject _forgotMenuGO;
		[SerializeField] private GameObject _loaderGO;
		
		Animator _animator;
		private const int _passwordLowLimit 		= 8;

		private bool _isValidLoginPresent = false;
		private string _login = "";
		private string _password 	= "";
		private string _prevLoginInput = "";
		private bool isAutoLogin = true;

		private string _code = "";
		private string _newEmail = "";
		private string _confirmPassword = "";
		private string _confirmEmail = "";

		private int _abTest = 2;

		void Awake () {
			DontDestroyOnLoad (gameObject);
			_animator = GetComponent<Animator> ();
//			_loginInputField.inputType = InputField.InputType.Standard;
//			_sloginInputField.inputType = InputField.InputType.Standard;
			_passwordInputField.inputType = InputField.InputType.Password;
			_loaderGO.SetActive (false);
			_forgotMenuGO.SetActive(false);
		}

		void OnEnable () {
			BTS_Manager.OnLoginConnectionSuccessful 	      += OnConnectionSuccessfullHandler;
			BTS_Manager.OnLoginConnectionFail 			      += OnConnectionFailHandler;

			BTS_WebServerManager.OnAuthCodeSuccessful 	      += OnAuthCodeSuccessfulHandler;
			BTS_WebServerManager.OnAuthCodeFail 		      += OnAuthCodeFailHandler;
			BTS_WebServerManager.OnRegistrationFail 	      += OnRegisterFailHandler;
			BTS_WebServerManager.OnReassignEmailSuccessful    += OnReassignEmailSuccessfulHandler;
			BTS_WebServerManager.OnReassignPasswordSuccessful += OnReassignPasswordSuccessfulHandler;
			BTS_Manager.OnResetEmail                          += OnResetEmailSuccessfulHandler;

			onEmailValueChanged                               += OnEmailValueChangedHandler;
		}

		void OnDisable () {
			BTS_Manager.OnLoginConnectionSuccessful 	      -= OnConnectionSuccessfullHandler;
			BTS_Manager.OnLoginConnectionFail 			      -= OnConnectionFailHandler;

			BTS_WebServerManager.OnAuthCodeSuccessful         -= OnAuthCodeSuccessfulHandler;
			BTS_WebServerManager.OnAuthCodeFail 		      -= OnAuthCodeFailHandler;
			BTS_WebServerManager.OnRegistrationFail 	      -= OnRegisterFailHandler;
			BTS_WebServerManager.OnReassignEmailSuccessful    -= OnReassignEmailSuccessfulHandler;
			BTS_WebServerManager.OnReassignPasswordSuccessful -= OnReassignPasswordSuccessfulHandler;
			BTS_Manager.OnResetEmail                          -= OnResetEmailSuccessfulHandler;
			
			onEmailValueChanged                               -= OnEmailValueChangedHandler;
		}

		void Start () {
			if (_abTest == 2) {
				_abTest = UnityEngine.Random.Range(0, 2);
			}
			Show();
				try {
					_login = BTS_PlayerData.Instance.StoredLogin.Replace ("|","");
					_sloginInputField.text = _login;
					_loginInputField.text = _login;
				} catch (IndexOutOfRangeException e) {
					Debug.LogError (e);
				}
			
			OnLoginValueChanged ();
		}

		//------------------------------------------------------------
		// Public Methods
		//------------------------------------------------------------

		internal void Show () {
//			if (_abTest == -1) 
			if (_abTest == 0) {
				ShowSignUpScreen();
			} else {
				ShowLoginMenu();
			}

		}

		internal void Hide () {
			_animator.SetTrigger (ANIMATOR_FADE_OUT_TRIGGER);
		}

		private void ShowSignUpScreen() {
			_animator.SetTrigger (ANIMATOR_FADE_IN_TRIGGER);
//			GA_Manager.Client.SendScreenHit("Sign Up Screen"); //TODO
		}

		private void ShowLoginMenu() {
			_animator.SetTrigger (ANIMATOR_LOGIN_TRIGGER);
//			GA_Manager.Client.SendScreenHit("Login Menu Screen"); //TODO
		}

		// Login Menu
		//--------------------------------------
		
		public void OnSignInButtonClick () {
			_login = BTS_PlayerData.Instance.StoredLogin.Replace ("|","");

			if (IsLoginValid (_login, _password)) {
				BTS_Manager.Instance.Connect (_login, _password);
				_loaderGO.SetActive(true);
			} else {
				
				ShowLog("Check input fields!");
				Debug.LogAssertion ("Input data is invalid! Please, check login and password.");
			}
		}


		public void OnRegistrationButtonClick() {
			SaveLogin(_login);
			Debug.Log(_abTest + "		ab test");
			if (_abTest == 0) {
				ShowSignUpScreen();
			}
			else {
				BTS_Manager.Instance.ShowRegistration ();
				SaveLogin(_sloginInputField.text);
			}
		}
		
		public void OnSignUpButtonClick () {
			SaveLogin(_sloginInputField.text);
//			_animator.SetTrigger(ANIMATOR_LOGIN_TRIGGER);
			BTS_Manager.Instance.ShowRegistration ();
		}

		public void GoToLoginMenu() {
			SaveLogin(_sloginInputField.text);
			ShowLoginMenu();
		}

		public void OnBackButtonClick () {
			Hide ();
		}

		public void GoToBeeTheSwarm() {
			Debug.Log("Open URL");
			Application.OpenURL("http://www.beetheswarm.com/");
		}

		public void OnForgotEmailClick() {
			SaveLogin(_login);

			_animator.SetTrigger (ANIMATOR_FORGOTEMAIL_TRIGGER);
//			GA_Manager.Client.SendScreenHit("Forgot Email Screen"); //TODO
		}

		public void OnForgotPasswordClick() {
			SaveLogin(_login);

			_animator.SetTrigger(ANIMATOR_FORGOTPASSWORD_TRIGGER);
//			GA_Manager.Client.SendScreenHit("Forgot Password Screen"); //TODO
		}

		public void BackFromForgotEmail() {
			ShowLoginMenu();
		}

		public void BackFromForgotPassword() {
			ShowLoginMenu();
		}

		// Input fields
		//--------------------------------------

		public void OnSignLoginValueChanged() {
			_login = _sloginInputField.text;
		}
		
		public void OnLoginValueChanged () {
			_login = _loginInputField.text;
		}

		public void OnSignLoginEndEdit() {
			SaveLogin(_sloginInputField.text);
		}

		public void OnLoginEndEdit () {
			SaveLogin(_loginInputField.text);
		}

		public void OnPasswordEndEdit () {
			_password = _passwordInputField.text;
		}

		//------------------------------------------------------------
		// Private Methods
		//------------------------------------------------------------

		private bool IsLoginValid (string login, string password) {
			if (login == string.Empty && (!login.Contains("@") && !login.Contains("."))) {
				//show _INPUT_DATA_WARNING
				return false;
			}
			if (password == string.Empty || password.Length < _passwordLowLimit) {
				//show _INPUT_DATA_WARNING
				_passwordInputField.text = "";
				return false;
			}

			return true;
		}

		private void SaveLogin(string login) {
			_login = login;
			_loginInputField.MoveTextEnd (false);
			_sloginInputField.MoveTextEnd(false);
			if (login.Length > 0 && login.Contains("@") && login.Contains(".")) {
				BTS_PlayerData.Instance.SaveLogin (_login, BTS_PlayerData.Instance.StoredAuthToken, true);
				onEmailValueChanged(login);
			}
		}

		private void OnEmailValueChangedHandler(string email) {
			_loginInputField.text = email;
			_sloginInputField.text = email;
		}

		private void ShowLog (string message) {

			BTS_Manager.Instance.Popup.ShowError (message);
		}

		private void OnDestroy () {
			Destroy (this.gameObject);
		}

		//------------------------------------------------------------
		// Handler
		//------------------------------------------------------------

		private void OnConnectionSuccessfullHandler () {
			Debug.Log("connection successful");
			_loaderGO.SetActive (false);

			if (_loginMenuGO.activeSelf == true) {
				string authToken = BTS_PlayerData.Instance.StoredAuthToken;
				BTS_PlayerData.Instance.SaveLogin(_login, authToken, isAutoLogin);
			}

//			GA_Manager.Client.SendEventHit("Conection", "Succesful", "", -1, false); //TODO

			_loginMenuGO.SetActive (false);

			gameObject.SetActive (false);
			Destroy (this.gameObject);

			OnClosed ();
		}

		private void OnConnectionFailHandler (string error) {
			_loaderGO.SetActive (false);
			_passwordInputField.text = "";

			if (error.Contains("Account is unconfirmed ")) {
				
//				GA_Manager.Client.SendEventHit("Conection", "Fail", error, -1, false); //TODO

				
				BTS_Manager.Instance.ShowRegistration();
				BTS_RegistrationController.Instance.ShowVerificationScreen();
				if (_login.Length > 0 && _login.Contains("@") && _login.Contains(".")) {
					new BTS_ResendCode(_login).Send();
				}
			}
		}

		private void OnAuthCodeSuccessfulHandler () {
			Hide ();
		}

		private void OnAuthCodeFailHandler (string error) {

		}

		private void OnResetEmailSuccessfulHandler(string login) {
			_loginInputField.text = login;
			_login = login;
		}

		private void OnReassignEmailSuccessfulHandler() {
			_login = BTS_PlayerData.Instance.StoredLogin.Replace ("|","");
			_loginInputField.text = _login;
			_passwordInputField.text = "";
			
			Hide();
//			ShowLoginMenu();
		}

		private void OnReassignPasswordSuccessfulHandler() {
			_passwordInputField.text = "";
			
			Hide();
//			ShowLoginMenu();
		}

		private void OnRegisterFailHandler (string error) {
			
//			GA_Manager.Client.SendEventHit("Registartion", "Fail", error, -1, false); //TODO

			if (error.Contains ("already in use")) {
				_animator.SetTrigger (ANIMATOR_LOGIN_TRIGGER);
				
//				GA_Manager.Client.SendScreenHit("Login Menu Screen"); //TODO
			}
		}
	}
}
