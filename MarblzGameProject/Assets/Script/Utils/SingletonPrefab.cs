using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonPrefab<T>: MonoBehaviour where T: MonoBehaviour {

	protected static T _Instance = null;

	public static T Instance {

		get { 
			if (_Instance == null) {
				_Instance = FindObjectOfType (typeof(T)) as T;
				if (_Instance == null) {
				
					var prefab = Resources.Load (typeof(T).Name) as GameObject;
					var gameObject = Instantiate (prefab) as GameObject;
					_Instance = gameObject.GetComponent<T> ();
					DontDestroyOnLoad (gameObject);
				}
			}
			return _Instance;
		}
	}

	public static bool HasInstance{

		get{ 
			return _Instance != null;
		}
	}
}
