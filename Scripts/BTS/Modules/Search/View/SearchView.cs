using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchView : BaseControlledView<ISearchViewListener>, ISearchView
{
    [SerializeField]
    private GameObject m_itemOrigin;
    [SerializeField]
    private Transform m_itemsParent;

    public void OnInputFieldChanged(string text)
    {

    }
}
