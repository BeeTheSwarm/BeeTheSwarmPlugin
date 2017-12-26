using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS {

internal class BTS_BaseServerPackage  {

	private string _Id;
	private BTS_Error _Error = null;
	private Dictionary<string, object> _Data = null;

	private string _RawResponce;
	
	//--------------------------------------
	//  Initialization
	//--------------------------------------
	
	
	public BTS_BaseServerPackage(string rawResponce) {
			
		Debug.Log(rawResponce);
		_RawResponce = rawResponce;
		Dictionary<string, object>  dict = Json.Deserialize(rawResponce) as Dictionary<string, object>;

		/*foreach (KeyValuePair<string, object> data in dict) {
			Debug.Log (data.Key + ":" + data.Value + ":" + data.Value.GetType().ToString());
		}*/

		_Id = (string) dict["method"];


		Dictionary<string, object> errorData = (Dictionary<string, object>) dict["error"];
		bool HasError = (bool) errorData["status"];
		if(HasError) {
			_Error =  new BTS_Error((string) errorData["message"]);
			Debug.Log(BTS_WebServer.WEBSERVER_LOG_HEADER + "Package " + _Id + " Failed: " + _Error.Describtion);
		} else  {
			if( dict["data"] is Dictionary<string, object>) {
				_Data = (Dictionary<string, object>) dict["data"];

				foreach (KeyValuePair<string, object> d in _Data) {
					//Debug.Log ("!!!" + d.Key + ":" + d.Value + ":" + d.Value.GetType().ToString());
				}
			}
			Debug.Log(BTS_WebServer.WEBSERVER_LOG_HEADER + "Package " + _Id + " Received ");
		}
	}



	//--------------------------------------
	//  Public Methods
	//--------------------------------------

	public T GetDataField<T>(string key) {

		T value = default(T);

		if(_Data != null && _Data.ContainsKey(key)) {
			object data = _Data[key];
            try {
                value = (T)System.Convert.ChangeType(data, typeof(T));
            } catch (Exception ex) {
                Debug.Log("[GetDataField] ChangeType Exception: " + ex.Message);
            }			
		}

		return value;
	}

	//--------------------------------------
	//  Get / Set
	//--------------------------------------


	public bool IsFailed {
		get {
			return Error != null;
		}
	}

	public string RawResponce {
		get {
			return _RawResponce;
		}
	}

	public BTS_Error Error {
		get {
			return _Error;
		}
	}

	public string Id {
		get {
			return _Id;
		}
	}
	
	public Dictionary<string, object> Data {
		get {
			return _Data;
		}
	}
}
}
