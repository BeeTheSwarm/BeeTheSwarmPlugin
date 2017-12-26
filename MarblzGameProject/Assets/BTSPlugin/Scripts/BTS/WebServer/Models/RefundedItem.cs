using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {

internal class RefundedItem  {

	private int _Id;
	private string _ProdcuctId;
	private long _TimeStamp;


	public RefundedItem(Dictionary<string, object> itemInfo) {
		_Id =  System.Convert.ToInt32(itemInfo["id"]) ;
		_ProdcuctId =  System.Convert.ToString(itemInfo["product_id"]) ;
		_TimeStamp =  System.Convert.ToInt64(itemInfo["time_created"]) ;
	}

	public int Id {
		get {
			return _Id;
		}
	}

	public string ProdcuctId {
		get {
			return _ProdcuctId;
		}
	}

	public long TimeStamp {
		get {
			return _TimeStamp;
		}
	}
}
}
