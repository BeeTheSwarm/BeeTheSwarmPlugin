﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour {

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);    
    }

    void OnClick() {
        AudioManager.Instance.PlayMenuButtonClickClip();
    }
}
