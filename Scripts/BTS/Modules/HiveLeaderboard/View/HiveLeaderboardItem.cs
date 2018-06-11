using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using BTS;
using System;
using System.Globalization;
namespace BTS {
    public class HiveLeaderboardItem : MonoBehaviour {
        [SerializeField]
        private Text m_username;
        [SerializeField]
        private Text m_beesCount;
        [SerializeField]
        private Text m_place;

        internal void SetViewModel(HiveLeaderboaderItemViewModel value, int place) {
            m_username.text = place.ToString() + " " +value.UserName;
            m_beesCount.text = value.Impact.ToString("C", CultureInfo.GetCultureInfo("en-us"));
        }
    }
}
