using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(LayoutElement))]
public class SizeObserver : MonoBehaviour {
    [SerializeField]
    private RectTransform m_observable;
    [SerializeField]
    private int m_paddingTop;
    [SerializeField]
    private int m_paddingBottom;
    private LayoutElement m_layoutElement;

    private void Awake()
    {
        m_layoutElement = GetComponent<LayoutElement>();
    }

    void Update () {
        m_layoutElement.preferredHeight = m_observable.sizeDelta.y + m_paddingTop + m_paddingBottom;
    }
}
