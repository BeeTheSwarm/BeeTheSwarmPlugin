using System;
using System.Collections;
using System.Collections.Generic;
using BTS;
using UnityEngine;

public interface IViewCampaignView : IControlledView, ITopPanelContainer {
    IPostlistContainer GetPostlistContainer();
}
