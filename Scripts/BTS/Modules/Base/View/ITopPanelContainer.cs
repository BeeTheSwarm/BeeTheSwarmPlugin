using UnityEngine;
using System.Collections;
using System;

public interface ITopPanelContainer: IControlledView {
    ITopPanelComponent GetTopPanel();
}
