using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBaseHivePopupView : IControlledView {
    void Setup(Sprite image, string username);
}
