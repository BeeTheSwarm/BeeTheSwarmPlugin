using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreView : BaseControlledView<IStoreViewListener>, IStoreView
{
    [SerializeField]
    private Image m_storeItem;

    public void OnCloseClicked()
    {

    }

    public void OnBuyClicked()
    {

    }
}
