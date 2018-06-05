using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEditPostController : IPopupController
{ 
    void Show(Action<EditMenuResponce> callback, Vector3 position);
}
