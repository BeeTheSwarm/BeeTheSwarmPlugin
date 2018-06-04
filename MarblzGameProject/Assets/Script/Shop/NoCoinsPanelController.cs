using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCoinsPanelController : MonoBehaviour {

    [SerializeField] Animator m_noCoinsAnimatorController;
	// Use this for initialization
	void Start () {
        ShopController.OnShowNoCoinsPopup += ShowNoCoinsPopup;
    }

    public void ShowNoCoinsPopup()
    {
        m_noCoinsAnimatorController.SetTrigger("ShowNoCoinsPopup");
    }

    public void HideNoCoinsPopup()
    {
        m_noCoinsAnimatorController.SetTrigger("HideNoCoinsPopup");
    }
}
