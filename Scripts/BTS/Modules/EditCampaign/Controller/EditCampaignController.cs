using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditCampaignController: BasePopupController<IEditCampaignView>, IEditCampaignViewListener, IEditCampaignController
{
    public EditCampaignController()
    {
    }
    private Action<EditMenuResponce> m_callback;
    public void OnDeleteClick()
    {
        Hide();
        SendResponce(EditMenuResponce.DeleteCampaign);
    }

    private void SendResponce(EditMenuResponce responce)
    {
        if (m_callback != null)
        {
            m_callback.Invoke(responce);
            m_callback = null;
        } else
        {
            throw new NullReferenceException("Callback cannot be null");
        }
    }

    public void OnEditClick()
    {
        Hide();
        SendResponce(EditMenuResponce.EditCampaign);
    }

    public void OnOutOfViewClick()
    {
        Hide();
        SendResponce(EditMenuResponce.NoAction);
    }

    public void OnEditPostClick() {
        Hide();
        SendResponce(EditMenuResponce.EditPost);
    }

    public void Show(Action<object> callback, object options)
    {
        
    }

    public void Show(Action<EditMenuResponce> callback, Vector3 position) {
        m_callback = callback;
        m_view.ShowAtPosition((Vector3)position);
    }
}
