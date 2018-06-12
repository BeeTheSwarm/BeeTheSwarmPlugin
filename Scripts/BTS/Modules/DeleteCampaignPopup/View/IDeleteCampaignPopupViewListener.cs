using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeleteCampaignPopupViewListener : IViewEventListener
{
    void OnDelete();
    void OnCancel();
}
