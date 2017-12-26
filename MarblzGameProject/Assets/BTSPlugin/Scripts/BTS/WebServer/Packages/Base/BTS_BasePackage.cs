using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BTS {

internal abstract class BTS_BasePackage  {


	private string _Id;
	private Int32 _TimeStamp;


	//--------------------------------------
	//  Initialization
	//--------------------------------------


	public BTS_BasePackage(string id) {
		_Id = id;
		_TimeStamp = BTS_WebServer.CurrentTimeStamp;
	}


	//--------------------------------------
	// Get / SET
	//--------------------------------------


	public string Id {
		get {
			return _Id;
		}
	}

	public Int32 TimeStamp {
		get {
			return _TimeStamp;
		}
	}

	public virtual Int32 Timeout {
		get {
			return 10;
		}
	}

	public virtual bool AuthenticationRequired {
		get {
			return true;
		}
	}

	//--------------------------------------
	// Public Methods
	//--------------------------------------

	public virtual void Send() {
		BTS_WebServer.Instance.Send(this);
	}

	public abstract Dictionary<string, object> GenerateData();
}
}