using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SA.Analytics.Google;

namespace BTS {
	internal class BTS_PanelController : SA_Singleton<BTS_PanelController> {

		Animator _animator;

		[SerializeField] Text _message;

		[SerializeField] GameObject _signInBtn;
		[SerializeField] GameObject _signOutBtn;

		private const string SIGN_IN_TEXT =
				"Bee The Swarm is a crowdfunding platform like GoFundMe, but where all the revenue comes from people like YOU playing video games!"
			;

		private const string SIGN_OUT_TEXT = "Would you like to log out?";

		void Awake() {
			DontDestroyOnLoad (this.gameObject);
			_animator = GetComponent<Animator>();

			BTS_Manager.OnLoginConnectionSuccessful += OnLoginConnectionSuccessfullHandler;
			BTS_Manager.OnDisconnected += OnDisconnectedHandler;
		}

		void OnDisable() {
			BTS_Manager.OnLoginConnectionSuccessful -= OnLoginConnectionSuccessfullHandler;
			BTS_Manager.OnDisconnected -= OnDisconnectedHandler;
		}

		public void Show() {
			if (BTS_Manager.Instance.IsConnected) {
				_message.text = SIGN_OUT_TEXT;
				_signInBtn.SetActive(false);
				_signOutBtn.SetActive(true);
			} else {
				_message.text = SIGN_IN_TEXT;
				_signInBtn.SetActive(true);
				_signOutBtn.SetActive(false);
			}

			_animator.SetTrigger("FadeIn");
			
//			GA_Manager.Client.SendScreenHit("Panel Screen"); //TODO
		}

		public void Hide() {
			_animator.SetTrigger("FadeOut");
		}

		public void SignInButtonClick() {
			BTS_Manager.Instance.Connect();
			Hide();
		}

		public void SignOutButtonClick() {
			BTS_Manager.Instance.Disconnect();
			Hide();
		}

		void OnLoginConnectionSuccessfullHandler() {
			Debug.Log("OnLoginConnectionSuccessfullHandler");
			if (BTS_Manager.Instance.IsConnected) {  
				_message.text = SIGN_OUT_TEXT;
				_signInBtn.SetActive(false);
				_signOutBtn.SetActive(true);
			} else {
				_message.text = SIGN_IN_TEXT;
				_signInBtn.SetActive(true);
				_signOutBtn.SetActive(false);
			}

//			Hide();
		}

		void OnDisconnectedHandler() {
			Debug.Log("OnDisconnectedHandler");
			if (BTS_Manager.Instance.IsConnected) {
//				_message.text = SIGN_OUT_TEXT;
				_signInBtn.SetActive(false);
				_signOutBtn.SetActive(true);
			} else {
//				_message.text = SIGN_IN_TEXT;
				_signInBtn.SetActive(true);
				_signOutBtn.SetActive(false);
			}
			
//			Hide();
		}
	}
}
