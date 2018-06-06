using UnityEngine;
using System.Collections;
using System;

public interface IView {
    void SetInitCallback(Action callback);
}
