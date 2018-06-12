using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBadgeAwardPopupView : IControlledView {
    void Setup(Sprite image, string description);
}
