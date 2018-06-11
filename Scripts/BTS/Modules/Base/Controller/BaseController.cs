using UnityEngine;
using System.Collections;
using System;

public abstract class BaseController<View>: IViewEventListener where View: IControlledView
{
    protected View m_view;
    
    public void SetView(IView view) {
        m_view = (View) view;
        m_view.SetListener(this);
        PostSetView();
    }

    protected virtual void PostSetView() {

    }

    public virtual void Show()
    {
        m_view.Show();
    }

    public virtual void Hide()
    {
        m_view.Hide();
    }
    
    public virtual void PostInject() {

    }
}
