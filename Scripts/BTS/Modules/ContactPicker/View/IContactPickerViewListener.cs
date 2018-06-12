using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContactPickerViewListener : IViewEventListener
{
    void OnSelected();
    void OnCancel();
}
