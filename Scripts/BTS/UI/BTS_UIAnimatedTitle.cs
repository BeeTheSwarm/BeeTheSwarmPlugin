using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BTS_UIAnimatedTitle : MonoBehaviour {

	[SerializeField] private string _newTitleValue;

	[Space(5)] 
	[SerializeField] private Text _newTitleText;
	[SerializeField] private string _triggerName;

	private Animator _animator;

	void Awake () {
		_animator = GetComponent <Animator> ();
	}

	public void StartAnimation () {
		_animator.SetTrigger (_triggerName);
	}

	public void SetTitle (string value) {
		_newTitleText.text = value;
	}

	public void SetNewTitle (string newValue) {
		_newTitleValue = newValue;
	}

	private void OnTimeToUpdateTitle () {
		SetTitle (_newTitleValue);
	}
}
