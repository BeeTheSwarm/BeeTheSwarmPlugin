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

    private Vector3 m_dragBeginPoint;
    
    public void OnDragBegin(BaseEventData eventData)
    {
        m_dragBeginPoint = Input.mousePosition;
    }
    
}
