using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BTS {
    public class UserLoginPopupView : BaseGreetingPopupView {
        [SerializeField]
        private Text m_username;
        public void Setup(UserLoginPopupItemModel model) {
            m_username.text = "Welcome back! " + model.UserName.ToString();
        }

    }
}
