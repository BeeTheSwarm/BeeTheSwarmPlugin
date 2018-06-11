using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserLoginPopupView : MonoBehaviour {
    [SerializeField]
    private Text m_username;
    public void Setup(UserLoginPopupItemModel model) {
        m_username.text = "Welcome back! " + model.UserName.ToString();
    }
}
