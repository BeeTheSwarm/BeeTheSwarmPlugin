using UnityEngine;
using System.Collections;
using System;

public class TopPanelScreen<Controller> : BaseControlledView<Controller>, ITopPanelContainer where Controller : IViewEventListener {
    [SerializeField]
    private TopPanelComponent m_topPanelOrigin;
    [SerializeField]
    private Transform m_topPanelParent;

    protected override void InitSubview() {
        base.InitSubview();
        m_topPanelOrigin = GameObjectInstatiator.InstantiateFromObject(m_topPanelOrigin);
        m_topPanelOrigin.transform.SetParent(m_topPanelParent, false);
    }

    public ITopPanelComponent GetTopPanel() {
        return m_topPanelOrigin;
    }

}
