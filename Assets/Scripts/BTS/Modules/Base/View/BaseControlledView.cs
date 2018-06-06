using UnityEngine;
using System.Collections;
using System;

public abstract class BaseControlledView<TController> : BaseView, IControlledView
    where TController : IViewEventListener {
    protected TController m_controller;
    private Action m_initCallback;

    public virtual void SetListener(IViewEventListener baseController) {
        m_controller = (TController) baseController;
    }

    public virtual void Show() {
        gameObject.SetActive(true);
    }

    public virtual void Hide() {
        gameObject.SetActive(false);
    }
}