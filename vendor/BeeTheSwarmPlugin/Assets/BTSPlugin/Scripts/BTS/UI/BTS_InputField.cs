using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using BTS;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class BTS_InputField : InputField {

    private RectTransform m_rectTransform;
    private ScrollRect m_scrollRect;
    protected override void Awake() {
        base.Awake();
        m_rectTransform = GetComponent<RectTransform>();
        onEndEdit.AddListener(OnEndEdit);
        shouldHideMobileInput = true;
        m_scrollRect = GetComponentInParent<ScrollRect>();
    }

    private void OnEndEdit(string arg0) {
        InputFieldsHandler.InputFieldEndEdit();
    }

    public override void OnPointerClick(PointerEventData eventData) {
        base.OnPointerDown(eventData);
        InputFieldsHandler.InputFieldStartsEdit(m_rectTransform);
    }

    public override void OnBeginDrag(PointerEventData eventData) {
        if (m_scrollRect != null) {
            m_scrollRect.OnBeginDrag(eventData);
        }
        
    }

    public override void OnDrag(PointerEventData eventData) {
        if (m_scrollRect != null) {
            m_scrollRect.OnDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData) {
        if (m_scrollRect != null) {
            m_scrollRect.OnEndDrag(eventData);
        }
    }

    public override void OnPointerDown(PointerEventData eventData) {
        
    }

}
