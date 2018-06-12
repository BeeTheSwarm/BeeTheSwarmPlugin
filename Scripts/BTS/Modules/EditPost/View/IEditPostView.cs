using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEditPostView : IControlledView
{
    void ShowAtPosition(Vector3 position);
}
