using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteCampaignPopupView : BaseControlledView<IDeleteCampaignPopupViewListener>, IDeleteCampaignPopupView
{
    public void OnDeleteClick()
    {
        m_controller.OnDelete();
    }

    public void OnCancelClick()
    {
        m_controller.OnCancel();
    }

    public void OnOutOfViewClick()
    {
        m_controller.OnCancel();
    }
}
