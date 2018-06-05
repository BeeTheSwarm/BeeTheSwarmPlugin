using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPluginContentViewListener : IViewEventListener
{
    void OnDrag(float y);
}
