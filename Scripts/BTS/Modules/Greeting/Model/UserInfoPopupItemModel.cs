using UnityEngine;
using System.Collections;

public class UserInfoPopupItemModel : PopupItemModel {
    public override PopupTypes Type {
        get {
            return PopupTypes.UserInfo;
        }
    }
    public int Bees { get; private set; }
    public int Level { get; private set; }
    public UserInfoPopupItemModel(int bees, int level) {
        Bees = bees;
        Level = level;
    }
}
