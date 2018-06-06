using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    internal abstract class BaseHivePopupController : BasePopupController<IBaseHivePopupView>, IBaseHivePopupViewListener, IBaseHivePopupController {
        
        private Action<bool> m_callback;
        public void OnNoClick() {
            Hide();
            m_callback.Invoke(false);
        }

        public void OnOutOfViewClick() {
            
        }

        public void OnYesClick() {
            Hide();
            m_callback.Invoke(true);
        }

        public void Show(UserViewModel userViewModel, Action<bool> callback) {
            m_callback = callback;
            m_view.Setup(userViewModel.Avatar.Get(), userViewModel.Username);
            base.Show();
        }
    }
}