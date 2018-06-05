using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStartCampaignViewListener : IViewEventListener
{
    void OnSelectImagePressed();
    void OnBackPressed();
    void CreateCampaign();
    void OnSelectCategoryClicked();
}

