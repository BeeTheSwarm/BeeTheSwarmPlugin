﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    internal interface IConfirmJoinHivePopupView : IControlledView {
        void Setup(Sprite image, string description);
    }
}
