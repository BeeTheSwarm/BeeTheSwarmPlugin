using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEditProfileViewListener : IViewEventListener {
    void OnSaveChanges();
    void OnEditAvatar();
    void OnPopupButtonClicked();
}
