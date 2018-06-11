using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    internal interface IBaseHivePopupController : IPopupController {
        void Show(UserViewModel userViewModel, Action<bool> callback);
    }
}
