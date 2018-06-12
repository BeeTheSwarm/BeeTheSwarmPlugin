using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICampaignCategoriesController : IPopupController {
    void Show(Action<int> callback);
}
