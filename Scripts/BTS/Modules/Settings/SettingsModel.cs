using System.Collections.Generic;

namespace BTS {
    public class SettingsModel: ISettingsModel {
        private Dictionary<SettingType, int> m_settings = new Dictionary<SettingType, int>();

        public SettingsModel() {
            m_settings.Add(SettingType.InvitedUser, 20);
            m_settings.Add(SettingType.RefferedUser, 20);
            m_settings.Add(SettingType.UploadPhoto, 20);
        }

        public void SaveSetting(SettingType type, int value) {
            if (m_settings.ContainsKey(type)) {
                m_settings[type] = value;
            }
            else {
                m_settings.Add(type, value);
            }
        }

        public int GetSettingValue(SettingType type) {
            return m_settings.ContainsKey(type) ? m_settings[type] : 0;
        }
    }
}