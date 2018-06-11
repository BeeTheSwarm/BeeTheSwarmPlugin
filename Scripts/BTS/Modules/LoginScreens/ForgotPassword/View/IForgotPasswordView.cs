using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    public interface IForgotPasswordView : IControlledView {
        void ShowPasswordsPage();
        void Clear();
    }
}
