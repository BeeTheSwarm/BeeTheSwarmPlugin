using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISignUpView : IControlledView {
    void ClearInput();
    void Setup(bool isStandalone);
}
