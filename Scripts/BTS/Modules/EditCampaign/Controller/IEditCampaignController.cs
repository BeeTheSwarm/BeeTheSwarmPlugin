using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEditCampaignController : IPopupController
{ 
    void Show(Action<EditMenuResponce> callback, Vector3 position);
}
