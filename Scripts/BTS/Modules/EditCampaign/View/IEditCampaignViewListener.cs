using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEditCampaignViewListener : IViewEventListener
{
    void OnEditClick();
    void OnDeleteClick();
    void OnOutOfViewClick();
    void OnEditPostClick();
}
