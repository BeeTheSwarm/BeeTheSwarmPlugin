using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

namespace BTS {
internal class AntiCheatToolkitPrefs : IPlayerDataLoader {

	public int GetInt (string key) {

		return ObscuredPrefs.GetInt (key);
	}

	public void SetInt (string key, int value) {

		ObscuredPrefs.SetInt (key, value);
	}

	public string GetString (string key) {

		return ObscuredPrefs.GetString (key);
	}

	public void SetString (string key, string value) {

		ObscuredPrefs.SetString (key, value);
	}

	public float GetFloat(string key) {

		return ObscuredPrefs.GetFloat (key);
	}

	public void SetFloat(string key, float value) {

		ObscuredPrefs.SetFloat (key, value);
	}

	public bool HasKey(string value) {

		return ObscuredPrefs.HasKey (value);
	}

	public int[] GetIntArray(string key) {
		if (HasKey(key)) {
			string[] stringArray = GetString(key).Split("|"[0]);
			int[] intArray = new int[stringArray.Length];
			for (int i = 0; i < stringArray.Length; i++)
				intArray[i] = Convert.ToInt32(stringArray[i]);
			return intArray;
		}
		return new int[0];
	}
	
	public void SetIntArray(string key, int[] data) {
		if (data.Length == 0) return;
		
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		for (int i = 0; i < data.Length - 1; i++)
			sb.Append(data[i]).Append("|");
		sb.Append(data[data.Length - 1]);
		
		try {
			SetString(key, sb.ToString());
		} catch (Exception e) {
			Debug.Log(e.Message);
		}
	}


	
	private void SavePrefString(string key, string value) {
		string encoded =  Convert.ToBase64String(Encoding.UTF8.GetBytes(key + value));
		SetString(key, encoded);

	}

	
	public string GetPrefString(string key) {
		if(HasKey(key)) {

			string str = GetString(key);
			if(str == string.Empty || str == "") {
				return string.Empty;
			}

			byte[] dec =  Convert.FromBase64String(str);
			string decoded = Encoding.UTF8.GetString(dec);
			decoded = decoded.Substring(key.Length, decoded.Length - key.Length);

			return decoded;
		} else {
			return string.Empty;
		}
	}
}
}	


