using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {

	[SerializeField] 
	Animator _menusAnimatorController;
	[SerializeField]
	Toggle _soundToggle;
	[SerializeField]
	Button _logoffButton;
	[SerializeField]
	Button _logInButton;


	//////////////////// Unity Functions


	void Start (){
	
	//	_soundToggle.isOn = !AudioManager.Instance.IsVolumeEnabled;
	//	AudioManager.Instance.SetToggleComponent(_soundToggle);

	//	soundToggle.OnValueChanged.AddListener (OnSoundButtonClick);

		//BTS_Manager.OnLoginConnectionSuccessfull 	+= OnLoginConnectionSuccessfullHandler;
		//BTS_Manager.OnDisconnected 					+= OnDisconnectedHandler;
	}

	void OnEnable(){
	/*	if (BTS_Manager.Instance.State == BTS.ConnectionState.Connected) {
			_logOffButton.targetGraphic.enabled = true;
			_logInButton.targetGraphic.enabled = false;
		} else {
			_logOffButton.targetGraphic.enabled = false;
			_logInButton.targetGraphic.enabled = true;
		}	*/
	}

	void OnDisable(){ 
	//	BTS_Manager.OnLoginConnectionSuccessfull 	-= OnLoginConnectionSuccessfullHandler;
	//	BTS_Manager.OnDisconnected 					-= OnDisconnectedHandler;
	}

	//////////////////Public functions
	 
	public void OnNewGameButtonClick(){
		//GameManager.

	}
}
