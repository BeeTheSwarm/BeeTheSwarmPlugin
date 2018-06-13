using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PluginContentView : BaseControlledView<IPluginContentViewListener>, IPluginContentView
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private RectTransform m_transform;

    private Vector3 m_dragBeginPoint;
    
    private bool m_dragEnabled;
    private bool m_animationEnabled;
    
    public void OnDragBegin(BaseEventData eventData)
    {
        if (!m_dragEnabled) {
            return;
        }
        m_dragBeginPoint = Input.mousePosition;
    }
    
    public void OnDrag(BaseEventData eventData)
    {
        if (!m_dragEnabled) {
            return;
        }
        m_dragBeginPoint = Input.mousePosition;
    }

    public override void Show() {
        base.Show();
        if (m_animationEnabled) {
            m_animator.enabled = true;
            m_animator.SetBool("Opened", true);
        }
        else {
            m_transform.anchorMax = new Vector2(1f, 1f);
            m_transform.anchorMin = new Vector2(0f, 0f);
        }
    }

    public override void Hide() {
        base.Hide();
        if (m_animationEnabled) {
            m_animator.enabled = true;
            m_animator.SetBool("Opened", false);
        }
        else {
            m_transform.anchorMax = new Vector2(2f, 1f);
            m_transform.anchorMin = new Vector2(1f, 0f);
        }
    }
    
    public void Setup(bool animationEnabled, bool dragEnabled) {
        m_dragEnabled = dragEnabled;
        m_animationEnabled = animationEnabled;
    }

}
