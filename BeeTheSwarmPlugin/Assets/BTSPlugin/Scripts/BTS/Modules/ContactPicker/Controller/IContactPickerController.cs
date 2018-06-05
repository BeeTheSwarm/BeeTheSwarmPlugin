using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContactPickerController : IPopupController {
    void ShowPhonesPicker(Action<List<string>> onPhonesPicked, string header, string button);
    void ShowEmailPicker(Action<List<string>> onEmailPicked, string header, string button);
}
