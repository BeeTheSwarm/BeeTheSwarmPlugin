using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPluginContentController : IScreenController
{
    event Action OnHideStarted;
    event Action OnHideFinished ;
    event Action OnShown;
}
