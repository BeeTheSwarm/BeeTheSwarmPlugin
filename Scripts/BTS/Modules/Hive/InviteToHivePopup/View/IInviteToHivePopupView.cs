using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInviteToHivePopupView : IControlledView {
    void Setup(Sprite image, string description);
}
