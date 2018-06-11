using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using BTS.BeeAnimation.View;

public class SwarmedBeeAnimation : BaseControlledView<IViewEventListener>, ISwarmedBeeAnimation {



    [SerializeField] private BeeAnimation m_beeOrigin;
    [SerializeField] private Transform m_beeParent;


    public void SwarmBeeAnimate() {
        var newBee = Instantiate(m_beeOrigin, m_beeParent, false);
        newBee.gameObject.SetActive(true);
        newBee.Play();
        //m_swarmBeeAnimator.SetTrigger(ANIMATOR_SHOW_SWARMBEES);
    }
}
