using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmAnimationController : BasePopupController<ISwarmedBeeAnimation>, ISwarmAnimationController
{
    public override void Show()
    {
        base.Show();
        m_view.SwarmBeeAnimate();
    }
}
