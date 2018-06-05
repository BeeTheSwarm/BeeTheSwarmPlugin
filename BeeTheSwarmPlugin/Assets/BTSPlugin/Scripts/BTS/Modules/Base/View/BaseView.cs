using UnityEngine;
using System.Collections;
using System;

public abstract class BaseView: MonoBehaviour, IView 
{

    private bool m_inited;
    private Action m_initCallback = delegate {  };
    
    protected void Start() {
        InitSubview();
        m_inited = true;
        if (m_initCallback != null) {
            m_initCallback.Invoke();
        }
    }

    protected virtual void InitSubview() {

    }

    public void SetInitCallback(Action callback) {
        if (m_inited) {
            callback.Invoke();
        } else {
            m_initCallback += callback;
        }
    }
}
