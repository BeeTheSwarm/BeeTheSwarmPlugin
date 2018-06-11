using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeleteCampaignPopupController : IPopupController {
    void Show(Action<DeleteCampaignPopupResponce> callback);
}
