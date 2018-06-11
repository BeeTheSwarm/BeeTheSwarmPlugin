using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditCampaignView : BaseControlledView<IEditCampaignViewListener>, IEditCampaignView
{
    [SerializeField]
    private Transform popupTransform;

    public void OnEditClick()
    {
        m_controller.OnEditClick();    
    }
    public void OnEditPostClick()
    {
        m_controller.OnEditPostClick();
    }
    
    public void OnDeleteClick()
    {
        m_controller.OnDeleteClick();
    }

    public void OnOutOfViewClick()
    {
        m_controller.OnOutOfViewClick();
    }

    public void ShowAtPosition(Vector3 position)
    {
        Show();
        popupTransform.position = position;
    }
}
