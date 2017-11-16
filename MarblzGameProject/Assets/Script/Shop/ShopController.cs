using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

	private static ShopController _instance;

	[SerializeField] Animator _animator;

	bool _isShowing = false;
	//////
	/////Get/Set
	//////


	public static ShopController Instance{

		get { 
			return _instance;
		}
	}


	private void Awake(){
		
		DontDestroyOnLoad (this.gameObject);
		_instance = this;
	}

	public void Show(){
		if (_isShowing == true)
			return;
		_isShowing = true;
		_animator.SetTrigger ("FadeIn");

	}

	public void Hide(){

		if (_isShowing == false)
			return;

		_isShowing = false;
		_animator.SetTrigger ("FadeOut");
	}

	public void OnBackButtonClick(){
		Hide ();
	}
}
