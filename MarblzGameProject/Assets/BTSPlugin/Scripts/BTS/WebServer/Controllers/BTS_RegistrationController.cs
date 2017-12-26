using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using SA.Analytics.Google;
using SA.Manifest;

namespace BTS {

	internal class BTS_RegistrationController : SA_Singleton<BTS_RegistrationController> {

		[SerializeField]
		Animator _animator;

		//Registration
		[Header("Registration")] 
		[SerializeField] private GameObject _registrationScreen;
		[SerializeField] private InputField _Name;
		[SerializeField] private InputField _password;
		[SerializeField] private InputField _confirmPassword;
		[SerializeField] private InputField _email;
		[SerializeField] private InputField _referalCode;
		[SerializeField] private GameObject _rights;

		//Confirmation
		[Header("Verification Screen")] 
		[SerializeField] private GameObject _verificationScreen;
		[SerializeField] private InputField _verificationCode;
		[SerializeField] private InputField _changeEmailInputfield;
		[SerializeField] private Button _resendEmailButton;
		[SerializeField] private Button _changeEmailButton;
		[SerializeField] private GameObject _changeEmailGroup;
		

		[SerializeField] private GameObject _loader;
		private bool _isWaiting = false;
		private float _responceDelay = 3f;
		private float _responceDelayTime = 0f;

		private float _timeShowed = 0f;
		private float _showTime = 2f;
		private bool _isLogShowing = false;

		private const int _passwordLowLimit 		= 8;
		private const int _referalCodeLimit         = 5;
		private const int _verificationodeLengthLimit = 4;

		private bool _isShow = false;
		private bool _isFieldsCached = false;

		//Cache
		private string name; 			    //= split[0];
		private string email;				//= _email.text;
		private string password;			//= _password.text;
		private string ref_token;

		//--------------------------------------
		//Get/Set
		//--------------------------------------

		//--------------------------------------
		//Built-in UNITY functions
		//--------------------------------------

		void Awake () {
			DontDestroyOnLoad (gameObject);
			
			_password.inputType = InputField.InputType.Password;
			_confirmPassword.inputType = InputField.InputType.Password;
		}

		void Start () {
			//FillTestValues ();
		}

		void Update () {
			if (_isLogShowing) {
				_timeShowed += Time.deltaTime;

				if (_timeShowed >= _showTime) {
					_isLogShowing = false;
				}
			}

			if (_isWaiting) {
				_responceDelayTime += Time.deltaTime;

				if (_responceDelayTime >= _responceDelay) {
					_isWaiting = false;
				}
			}
		}
			
		void OnEnable () {
			BTS_WebServerManager.OnRegistrationSuccessful 	+= OnRegistrationCompleteHandler;
			BTS_WebServerManager.OnRegistrationFail 		+= OnRegistrationFailhandler;
			BTS_WebServerManager.OnAuthCodeSuccessful 		+= OnAuthCodeSuccessfulHandler;
			BTS_WebServerManager.OnAuthCodeFail 			+= OnAuthCodeFailHandler;
			BTS_WebServerManager.OnResendCodeSuccessful 	+= OnResendCodeSuccessfulHandler;	
			BTS_WebServerManager.OnResendCodeFail 			+= OnResendCodeFailHandler;	
			
			BTS_InterstitialController.onEmailValueChanged  += OnEmailValueChangedHandler;
		}

		void OnDisable () {
			BTS_WebServerManager.OnRegistrationSuccessful 	-= OnRegistrationCompleteHandler;
			BTS_WebServerManager.OnRegistrationFail 		-= OnRegistrationFailhandler;
			BTS_WebServerManager.OnAuthCodeSuccessful 		-= OnAuthCodeSuccessfulHandler;
			BTS_WebServerManager.OnResendCodeSuccessful 	-= OnResendCodeSuccessfulHandler;	
			BTS_WebServerManager.OnResendCodeFail 			-= OnResendCodeFailHandler;	
			
			BTS_InterstitialController.onEmailValueChanged  -= OnEmailValueChangedHandler;
		}

		//--------------------------------------
		//Public functions
		//--------------------------------------

		public void ShowRegistration () {
			FadeIn ();

			_registrationScreen.SetActive (true);
			_verificationScreen.SetActive (false);
			
			_email.text =  BTS_PlayerData.Instance.StoredLogin.Replace ("|","");

			StopWaiting ();
			ResetInputs();
//			GA_Manager.Client.SendScreenHit("Registration Screen"); //TODO

		}

		public void ShowVerificationScreen () {
			FadeIn ();

			if (_verificationScreen.activeSelf)
				return;
			
			_registrationScreen.SetActive (false);
			_verificationScreen.SetActive (true);
			_changeEmailGroup.SetActive(false);
			
			_changeEmailButton.targetGraphic.enabled = true;
			_resendEmailButton.targetGraphic.enabled = true;

			StopWaiting ();
			
//			GA_Manager.Client.SendScreenHit("Verification Screen"); //TODO
		}

		public void Close () {
			if (!_rights.activeInHierarchy && !_verificationScreen.activeInHierarchy) {
				FadeOut ();
				ResetInputs();
			} else if (_rights.activeInHierarchy) {
				_rights.SetActive (false);
			} else if (_verificationScreen.activeInHierarchy) {
				_registrationScreen.SetActive(true);
				_verificationScreen.SetActive(false);
				ResetInputs();
			}
		}

		public void OnTermsButtonClick () {
			_rights.SetActive (true);
			
//			GA_Manager.Client.SendScreenHit("Terms Screen"); //TODO
		}
		
		public void OnRegistrationButtonClick () {

			if (_isWaiting)
				return;

			bool isInputValid = true;
			Action errorHandleAction;

			//USERNAME
			errorHandleAction = () => {
				isInputValid = false;

				_Name.text = "";
				_Name.placeholder.color = Color.red;
			};
			try {
				if (_Name.text.Length > 0 && _Name.text.Any(char.IsLetter)) {
					name = _Name.text;
				} else {
					errorHandleAction ();
				}
			} catch (Exception exc) {
				errorHandleAction ();
			}
			
			//PASSWORD
			errorHandleAction = () => {
				isInputValid = false;

				_password.text = "";
				_password.placeholder.color = Color.red;
			};
			try {
				if (_password.text.Length >= _passwordLowLimit) {
					password = _password.text;
				} else {
					errorHandleAction ();
				}
			}catch (Exception exc) {
				errorHandleAction ();
			}
			
			//CONFIRM PASSWORD
			errorHandleAction = () => {
				isInputValid = false;

				_confirmPassword.text = "";
				_confirmPassword.placeholder.color = Color.red;
			};
			try {
				if (_confirmPassword.text == password) {
					;
				} else {
					errorHandleAction();
				}
			}
			catch (Exception exs) {
				errorHandleAction();
			}

			//EMAIL
			errorHandleAction = () => {
				isInputValid = false;

				_email.text = "";
				_email.placeholder.color = Color.red;
			};
			try {
				if (_email.text.Length > 0 && _email.text.Contains ("@") && _email.text.Contains (".")) {
					email = _email.text;
				} else {
					errorHandleAction ();
				}
			}catch (Exception exc) {
				errorHandleAction ();
			}
			
			//REF TOKEN
			errorHandleAction = () => {
				isInputValid = false;

				_referalCode.text = "";
				_referalCode.placeholder.color = Color.red;
			};
			try {
				if (_referalCode.text.Length >= _referalCodeLimit ) {
					ref_token = _referalCode.text;
				}
			}
			catch (Exception exc) {
				errorHandleAction();
			}

			if (isInputValid) {
				Debug.Log("input valid ok");
				ulong offset = BTS_PlayerData.Instance.DateOffset;
				new BTS_Register(name, email, password, offset, ref_token).Send();
			} else {
				ShowLog ("Check Input Fields!");
			}
		}

		public void OnSubmitButtonClick () {
			if (_isWaiting)
				return;

			if (isConfirmInputCorrect () == false)
				return;

			int user_id = BTS_PlayerData.Instance.Player.BtsID;
			int code = int.Parse (_verificationCode.text);

			new BTS_AuthCode (user_id, code).Send ();

			Wait ();
		}

		public void OnResendEmailButtonClick () {

			if (_email.text.Length > 0 && _email.text.Contains("@") && _email.text.Contains(".")) {
				new BTS_ResendCode (email).Send ();
				Wait ();
				return;
			}
			
			string storedEmail = BTS_PlayerData.Instance.StoredLogin;
			if (storedEmail.Length > 0){
				new BTS_ResendCode (storedEmail).Send ();
				Wait ();
				return;
			}

			ShowChangeEmail();
			ShowLog ("Input the email address.");
		}

		public void OnChangeEmailButtonClick () {
			if (_isWaiting)
				return;

			_changeEmailInputfield.text = "";
			ShowChangeEmail ();
		}

		public void OnChangeButtonClick () {
			try {
				if (_changeEmailInputfield.text.Length > 0 && _changeEmailInputfield.text.Contains("@") && _changeEmailInputfield.text.Contains(".")) {
					email = _changeEmailInputfield.text;
					string emailAddress = email.Replace(" ", "");
					ulong offset = BTS_PlayerData.Instance.DateOffset;

					new BTS_Register(name, emailAddress, password, offset, ref_token).Send();
					BTS_PlayerData.Instance.LastInputedEmailAddress = emailAddress;
				}
			} catch (Exception exc) {
				HideChangeEmail ();
				ShowLog ("Invalid email address");
			}
		}

		public void OnCancelChangeButtonClick () {
			HideChangeEmail ();
		}

		public void OnCancelButtonClick () {
			if (_isWaiting)
				return;

			Close ();
		}
		
		//--------------------------------------
		//Private functions
		//--------------------------------------

		private void ResetInputs() {
			_password.text = "";
			_confirmPassword.text = "";
			_Name.text = "";
			_referalCode.text = "";
			_verificationCode.text = "";
		}

		private bool isConfirmInputCorrect () {
			if (_verificationCode.text.Length != _verificationodeLengthLimit)
				return false;

			return true;
		}

		private void FadeIn () {
			_animator.SetTrigger ("FadeIn");
		}

		private void FadeOut () {
			_animator.SetTrigger ("FadeOut");
		}

		private void ShowLog (string message) {
			BTS_Manager.Instance.Popup.ShowError (message);
		}

		private void ShowChangeEmail () {
			_changeEmailButton.targetGraphic.enabled = false;
			_resendEmailButton.targetGraphic.enabled = false;
			
			_changeEmailGroup.SetActive(true);
			
//			GA_Manager.Client.SendScreenHit("Change Email Screen"); //TODO
		}

		private void HideChangeEmail () {
			_changeEmailGroup.SetActive(false);

			_changeEmailButton.targetGraphic.enabled = true;
			_resendEmailButton.targetGraphic.enabled = true;
			
//			GA_Manager.Client.SendScreenHit("Verification Screen"); //TODO
		}

		private void Wait () {
			_loader.SetActive (true);
			_responceDelayTime = 0f;
			_isWaiting = true;
		}

		private void StopWaiting () {
			_loader.SetActive (false);
			_responceDelayTime = 0f;
			_isWaiting = false;
		}

		//--------------------------------------
		//Handlers
		//--------------------------------------

		public void OnEmailValueChanged () {
			_email.placeholder.color = Color.grey;
		}
		
		private void OnEmailValueChangedHandler (string email) {
			_email.text = email;
		}
		
		//Web server handlers
		void OnRegistrationCompleteHandler (BTS_Player player) {
			if (_changeEmailGroup.activeSelf) {
				HideChangeEmail();
			}
			Debug.Log("registration complete");
			StopWaiting ();

			BTS_PlayerData.Instance.SaveLogin (email, BTS_PlayerData.Instance.StoredAuthToken, true);
			
			ShowVerificationScreen ();
		}

		void OnRegistrationFailhandler (string error) {
			StopWaiting ();

			Debug.Log(error);
			
			if (error.Contains("Acount is unconfirmed")) {
				ShowVerificationScreen();
				new BTS_ResendCode (email).Send ();
			} 
			if (error.Contains("already exist")) {
				_animator.SetTrigger ("FadeOut");
			}

//			GA_Manager.Client.SendEventHit("Registration", "Fail", error, -1, false); //TODO
		}

		void OnAuthCodeSuccessfulHandler () {
			StopWaiting ();

			_animator.SetTrigger ("FadeOut");
			
			BTS_Manager.Instance.ShowInvite();
//			GA_Manager.Client.SendEventHit("Registration", "Succesful", "", -1, false); //TODO

		}

		void OnAuthCodeFailHandler (string error) {
			StopWaiting ();

			ShowLog (error); 
		}

		void OnResendCodeSuccessfulHandler (string message) {
			StopWaiting ();
		}

		void OnResendCodeFailHandler (string error) {
			StopWaiting ();

			ShowLog (error); 
		}
	}
}
