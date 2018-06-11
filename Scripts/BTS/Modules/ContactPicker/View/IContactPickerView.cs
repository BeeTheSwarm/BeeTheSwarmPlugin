using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContactPickerView : IControlledView
{
    void SetViewModel(ContactPickerViewModel m_viewModel);
    void SetTexts(string header, string button);
}
