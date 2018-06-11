using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public abstract class Counter : MonoBehaviour {

    protected Text value;

    protected virtual void Start() {
        value = GetComponent<Text>();
    }

    protected virtual void OnValueChanged(int value) {
        this.value.text = value.ToString();
    }

}