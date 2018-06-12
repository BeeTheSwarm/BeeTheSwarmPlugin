using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BTS {

	public class BTS_StatusIndicator : MonoBehaviour {

		[SerializeField]
		Image _icon;

		[SerializeField]
		Sprite _green;
		[SerializeField]
		Sprite _orange;
		[SerializeField]
		Sprite _red;

        internal void SetStatus(ConnectionState state) {
            SetSprite(GetSpriteByState(state));
        }

        ConnectionState _prevState = ConnectionState.None;
		ConnectionState _curState = ConnectionState.None;

		void Awake () {
			SetSprite (_red);
		}
        /*
		void Update () {
			_curState = BTS_Manager.Instance.State;
			if (_curState != _prevState) {
				Sprite newSprite = GetSpriteByState (_curState);
				SetSprite (newSprite);

				_prevState = _curState;
			}
		}*/

		private Sprite GetSpriteByState (ConnectionState state) {
			Sprite sprite = null;

			switch (state) {

			case ConnectionState.Connected:
				sprite = _green;
				break;

			case ConnectionState.Connecting:
				sprite = _orange;
				break;

			case ConnectionState.Disabled:
			case ConnectionState.Disconnected:
			case ConnectionState.None:
				sprite = _red;
				break;
			}

			return sprite;
		}

		private void SetSprite (Sprite sprite) {
			_icon.sprite = sprite;
		}
        
        public void Show () {
			_icon.enabled = true;
		}

		public void Hide () {
			_icon.enabled = false;
		}
	}
}
