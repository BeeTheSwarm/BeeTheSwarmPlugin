using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddToHiveView : IControlledView, ITopPanelContainer {
    void SetCode(string code);
}
