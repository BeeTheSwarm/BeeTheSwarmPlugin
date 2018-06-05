using UnityEngine;
using System.Collections;

public class EditProfileViewModel {
    public Observable<Sprite> Avatar = new Observable<Sprite>();
    public Observable<string> Username = new Observable<string>();
    public string NewUsername = string.Empty;
    public string OldPassword = string.Empty;
    public string NewPassword = string.Empty;
    public string NewPasswordConfirmation = string.Empty;
    public Texture2D NewAvatar;

    public void Reset() {
        NewUsername = string.Empty;
        OldPassword = string.Empty;
        NewPassword = string.Empty;
        NewPasswordConfirmation = string.Empty;
    }
}
