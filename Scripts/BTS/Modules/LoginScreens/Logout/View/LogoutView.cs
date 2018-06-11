using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoutView : BaseControlledView<IStoreViewListener>, ILogoutView
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
