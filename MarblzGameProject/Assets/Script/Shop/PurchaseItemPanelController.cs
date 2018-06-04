using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PurchaseItemPanelController : MonoBehaviour {

    [SerializeField] Animator m_purchaseItemAnimatorController;

	// Use this for initialization
	void Start () {
        ShopController.OnShowCongratsPopup += ShowPurchaseItemPopup;
 	}

    public void ShowPurchaseItemPopup() {
        m_purchaseItemAnimatorController.SetTrigger("ShowPurchaseItemPopup");
    }

    public void HideCongratsPopup() {
       
        m_purchaseItemAnimatorController.SetTrigger("HidePurchaseItemPopup");
    }

    

}
