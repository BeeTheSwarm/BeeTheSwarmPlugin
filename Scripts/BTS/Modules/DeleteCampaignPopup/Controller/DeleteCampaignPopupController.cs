using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCampaignPopupController: BasePopupController<IDeleteCampaignPopupView>, IDeleteCampaignPopupViewListener, IDeleteCampaignPopupController 
{
    private Action<DeleteCampaignPopupResponce> m_callback;
    public DeleteCampaignPopupController()
    {
    }
    
    public void OnCancel()
    {
        Hide();
        if (m_callback != null)
        {
            m_callback.Invoke(DeleteCampaignPopupResponce.Cancel);
        }
    }

    public void OnDelete()
    {
        Hide();
        if (m_callback != null)
        {
            m_callback.Invoke(DeleteCampaignPopupResponce.Delete);
        }
    }

    public void Show(Action<DeleteCampaignPopupResponce> callback)
    {
        m_callback = callback;
        base.Show();
    }
    
}
