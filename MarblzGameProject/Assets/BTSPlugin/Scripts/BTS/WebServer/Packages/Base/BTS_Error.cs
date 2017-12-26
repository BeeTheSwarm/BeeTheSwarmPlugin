using UnityEngine;
using System.Collections;

public class BTS_Error  {

	private string _Describtion;

	public BTS_Error(string describtion) {
		_Describtion = describtion;
	}

	public string Describtion {
		get {
			return _Describtion;
		}
	}
}
