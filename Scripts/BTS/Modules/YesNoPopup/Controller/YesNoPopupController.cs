using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoPopupController: BasePopupController<IYesNoPopupView>, IYesNoPopupViewListener, IYesNoPopupController 
{
    private Action<YesNoPopupResponce> m_callback;
    public YesNoPopupController()
    {
    }
    
    public void OnCancel()
    {
        Hide();
        if (m_callback != null)
        {
            m_callback.Invoke(YesNoPopupResponce.Canceled);
        }
    }

    public void OnDelete()
    {
        Hide();
        if (m_callback != null)
        {
            m_callback.Invoke(YesNoPopupResponce.Confirmed);
        }
    }

    public void Show(string message, string yesButtonLabel, string noButtonLabel, Action<YesNoPopupResponce> callback) {
        m_callback = callback;
        m_view.SetMessage(message);
        m_view.SetYesButtonLabel(yesButtonLabel);
        m_view.SetNoButtonLabel(noButtonLabel);
        base.Show();
    }
}
