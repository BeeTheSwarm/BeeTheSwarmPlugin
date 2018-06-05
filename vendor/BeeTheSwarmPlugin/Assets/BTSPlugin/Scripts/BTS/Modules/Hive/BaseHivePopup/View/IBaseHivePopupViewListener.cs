using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    internal interface IBaseHivePopupViewListener : IViewEventListener {
        void OnYesClick();
        void OnNoClick();
        void OnOutOfViewClick();
    }
}