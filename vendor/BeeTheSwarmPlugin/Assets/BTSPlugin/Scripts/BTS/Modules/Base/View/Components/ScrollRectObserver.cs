using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(EventTrigger))]
public class ScrollRectObserver :  MonoBehaviour, IDragHandler {
    private ScrollRect m_scroll;
    [SerializeField]
    public UnityEvent ScrolledToEnd;
    private void Awake() {
        m_scroll = GetComponent<ScrollRect>();
    }

    public void OnDrag(PointerEventData data) {
        if (data.delta.y > 0 && m_scroll.verticalNormalizedPosition <= 0) {
            if (ScrolledToEnd != null) {
                ScrolledToEnd.Invoke();
            }
        }
    }
}
