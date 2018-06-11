using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddToHiveViewListener : IViewEventListener
{
    void CopyCodeClicked();
    void SendByMailClicked();
    void SendByPhoneClicked();
}
