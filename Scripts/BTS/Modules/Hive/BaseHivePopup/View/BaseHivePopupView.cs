using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    internal class BaseHivePopupView : BaseControlledView<IBaseHivePopupViewListener>, IBaseHivePopupView {
        [SerializeField]
        private Text m_username;
        [SerializeField]
        private Image m_avatar;

        public void Setup(Sprite image, string description) {
            m_avatar.overrideSprite = image;
            m_username.text = description;
        }

        public void OnYesClicked() {
            m_controller.OnYesClick();
        }

        public void OnNoClicked() {
            m_controller.OnNoClick();
        }

        public void OnOutOnViewClicked() {
            m_controller.OnOutOfViewClick();
        }
    }

}