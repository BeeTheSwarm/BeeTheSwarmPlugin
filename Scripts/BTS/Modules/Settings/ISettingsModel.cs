namespace BTS {
    public interface ISettingsModel: IModel {
        int GetSettingValue(SettingType type);
        void SaveSetting(SettingType type, int value);
    }
}