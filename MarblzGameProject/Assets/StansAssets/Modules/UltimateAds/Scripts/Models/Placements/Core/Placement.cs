using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Placement {

	[SerializeField]
	private string _id = string.Empty;

	public string ID {
		get {
			return _id;
		}
		set {
			_id = value;
		}
	}
}
