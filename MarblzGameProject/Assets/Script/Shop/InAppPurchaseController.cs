using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InAppPurchaseType{

	None,
	NoAds
}

public class InAppPurchaseController : MonoBehaviour {

	[SerializeField] InAppPurchaseType _inAppType;
	[SerializeField] int _parameter;

	public void Init(InAppPurchaseType type, int parameter){
		this._inAppType = type;
		this._parameter = parameter;
	}

	public void OnShopButtonClick(){
		Debug.Log ("OnShopButtonClick");
		Debug.Log ("UM_InAppPurchaseManager.Instance.IsInited true");

		UM_InAppProduct boost;
		boost = UM_InAppPurchaseManager.GetProductById (_inAppType.ToString ());

		if (boost != null) {
			Debug.Log ("Purchase" + _inAppType.ToString () + " " + boost.ToString ());
			UM_InAppPurchaseManager.Client.OnPurchaseFinished += HandleOnPurchaseFlowFinishedAction;
			UM_InAppPurchaseManager.Client.Purchase (boost.id);
		} else {
			Debug.Log ("boost !=null");
		}
	}

	void HandleOnPurchaseFlowFinishedAction(UM_PurchaseResult result){
		UM_InAppPurchaseManager.Client.OnPurchaseFinished -= HandleOnPurchaseFlowFinishedAction;

		if (result.isSuccess) {
			if (_inAppType == InAppPurchaseType.None) {
				Debug.Log ("boost is None!");
				return;
			}
			///////for multiple purchases.
			switch (_inAppType) {
			case InAppPurchaseType.NoAds:
				AdsController.Instance.DisableAds ();
				break;
			}
		} else {
			Debug.Log ("PurchaseFlowFinished Error:" + result.ResponceCode);
		}
	}
}
