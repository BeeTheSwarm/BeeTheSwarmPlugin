using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BTS {
    public class RequestItemView : MonoBehaviour {
        [SerializeField]
        private Avatar m_avatar;
        [SerializeField]
        private Text m_username;
        [SerializeField]
        private RequestItemViewModel m_viewModel;

        internal virtual void SetViewModel(RequestItemViewModel viewModel) {
            m_viewModel = viewModel;
            m_username.text = m_viewModel.Username;
            m_viewModel.UserAvatar.Subscribe(m_avatar.SetAvatar);
        }

        public int Id {
            get { return m_viewModel.Id; }
        }

        public void OnAcceptClick() {
            m_viewModel.Accept();
        }

        public void OnDeclineClick() {
            m_viewModel.Decline();
        }
    }
}