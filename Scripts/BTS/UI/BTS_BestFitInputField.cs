using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BTS_BestFitInputField : MonoBehaviour {

	private InputField _inputField;
	private Text _text;
	
	void Start () {
		_inputField = GetComponent<InputField>();
		_text = _inputField.textComponent;
//		_inputField.onValueChanged.AddListener(FitValue);
	}

	private void FitValue() {
		_text.fontSize = 2;
//		return _text.text;
	}
}
