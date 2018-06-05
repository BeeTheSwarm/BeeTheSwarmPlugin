using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEditProfileView : IControlledView, ITopPanelContainer{
    void SetViewModel(EditProfileViewModel m_viewModel);
    void Reset();
    void ShowPopup(string error);
    void ClosePopup();
}
