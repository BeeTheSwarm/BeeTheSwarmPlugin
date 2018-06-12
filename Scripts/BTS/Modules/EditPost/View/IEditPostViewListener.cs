using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEditPostViewListener : IViewEventListener
{
    void OnEditClick();
    void OnDeleteClick();
    void OnOutOfViewClick();
}
