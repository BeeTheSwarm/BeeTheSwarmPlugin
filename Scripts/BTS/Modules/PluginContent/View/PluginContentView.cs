using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PluginContentView : BaseControlledView<IPluginContentViewListener>, IPluginContentView, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform m_transform;

    private Vector3 m_dragBeginPoint;
    
    private bool m_dragEnabled;
    private bool m_dragFailed;
    private bool m_animationEnabled;
    private bool m_swipeAnimationActive;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!m_dragEnabled) {
            return;
        }
        m_dragBeginPoint = Input.mousePosition;
        m_dragFailed = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!m_dragEnabled) {
            return;
        }

        if (m_swipeAnimationActive) {
            return;
        }
        if (m_dragFailed) {
            return;
        }
        var point = Input.mousePosition;
        if (Mathf.Abs(point.y - m_dragBeginPoint.y) > 50) {
            m_dragFailed = true;
            SwipeShow();
            return;
        }

        if (point.x < m_dragBeginPoint.x) {
            SetAnchorMinX(0f);
            return;
        }
        if (Mathf.Abs(point.x - m_dragBeginPoint.x) > Screen.width / 3f) {
            SwipeHide();
        }
        else {
            SetAnchorMinX((point.x - m_dragBeginPoint.x) / Screen.width);
        }
    }

    private void SetAnchorMinX(float value) {
        m_transform.anchorMax = new Vector2(value + 1f, 1f);
        m_transform.anchorMin = new Vector2(value, 0f);
    }

    private void SwipeHide() {
        StartCoroutine(Animate(1f));
    }

    private IEnumerator Animate(float targetMinAnchor) {
        m_swipeAnimationActive = true;
        float diff;
        do {
            diff = targetMinAnchor - m_transform.anchorMin.x;
            float nextMinAnchorX;
            if (diff > 0) {
                nextMinAnchorX = m_transform.anchorMin.x + Math.Min(2f * Time.unscaledDeltaTime, diff);
            }
            else {
                nextMinAnchorX = m_transform.anchorMin.x - Math.Min(2f * Time.unscaledDeltaTime, -diff);
            }
            SetAnchorMinX(nextMinAnchorX);
            yield return null;
        } while (Mathf.Abs(diff) > 0f); 
        m_swipeAnimationActive = false;
    }

    private void SwipeShow() {
        StartCoroutine(Animate(0f));
    }

    public override void Show() {
        base.Show();
        if (m_animationEnabled) {
            SwipeShow();
        }
        else {
            SetAnchorMinX(0f);

        }
    }

    public override void Hide() {
        if (m_animationEnabled) {
            SwipeHide();
        }
        else {
            SetAnchorMinX(1f);
        }
    }
    
    public void Setup(bool animationEnabled, bool dragEnabled) {
        m_dragEnabled = dragEnabled;
        m_animationEnabled = animationEnabled;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!m_dragEnabled) {
            return;
        }

        if (m_swipeAnimationActive) {
            return;
        }
        SwipeShow();
        
    }
}
