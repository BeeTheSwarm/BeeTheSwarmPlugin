using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using BTS;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {

    internal class MissedHivePlayersItem : MonoBehaviour {
        [SerializeField]
        private Image m_avatar;
        [SerializeField]
        private Text m_name;

        private MissedHivePlayersItemViewModel m_viewModel;
        internal void SetViewModel(MissedHivePlayersItemViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.Avatar.Subscribe(SetAvatar);
            m_name.text = m_viewModel.UserName;
        }

        private void SetAvatar(Sprite value) {
            m_avatar.overrideSprite = value;
        }
    }

}