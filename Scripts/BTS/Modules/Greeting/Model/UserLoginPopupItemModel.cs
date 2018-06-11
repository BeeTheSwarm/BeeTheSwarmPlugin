using UnityEngine;
using System.Collections;

public class UserLoginPopupItemModel : PopupItemModel {
    public override PopupTypes Type {
        get {
            return PopupTypes.UserLogin;
        }
    }
    public string UserName { get; private set; }
    public UserLoginPopupItemModel(string userName) {
        UserName = userName;
    }
}
