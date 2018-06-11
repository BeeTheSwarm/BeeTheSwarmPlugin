using UnityEngine;
using System.Collections;

namespace BTS {
    public class LocalDataModel : ILocalDataModel {
        IPlayerDataLoader m_playerDataLoader ;

        public LocalDataModel() {
            m_playerDataLoader = new ObfuscatedPrefs();
        }

        public void SaveToken(string authToken) {
            m_playerDataLoader.SetString(PlayerPrefsConstants.SAVED_TOKEN, authToken);
        }

        public void SaveUserId(int userId) {
            m_playerDataLoader.SetInt(PlayerPrefsConstants.SAVED_USER_ID, userId);
        }

        public string GetToken() {
            if (m_playerDataLoader.HasKey(PlayerPrefsConstants.SAVED_TOKEN)) {
                return m_playerDataLoader.GetString(PlayerPrefsConstants.SAVED_TOKEN);
            }
            return null;
        }

        public int? GetUserId() {
            if (m_playerDataLoader.HasKey(PlayerPrefsConstants.SAVED_USER_ID)) {
                return m_playerDataLoader.GetInt(PlayerPrefsConstants.SAVED_USER_ID);
            }
            return null;
        }
    }
}