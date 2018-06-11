using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using BTS;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {

    internal class HiveMember : MonoBehaviour {
        [SerializeField]
        private Avatar m_avatar;
        [SerializeField]
        private Text m_name;
        [SerializeField]
        private Text m_place;
        [SerializeField]
        private Text m_impact;

        private HiveMemberViewModel m_viewModel;
        internal void SetViewModel(HiveMemberViewModel viewModel) {
            m_viewModel = viewModel;
            m_viewModel.Avatar.Subscribe(SetAvatar);
            m_name.text = m_viewModel.UserName;
            m_place.text = m_viewModel.Place > 0 ? m_viewModel.Place.ToString() : string.Empty;
            m_impact.text = "IMPACT " + m_viewModel.Impact.ToString("C", CultureInfo.GetCultureInfo("en-us"));
        }

        private void SetAvatar(Sprite value) {
            m_avatar.SetAvatar(value);
        }
    }

}