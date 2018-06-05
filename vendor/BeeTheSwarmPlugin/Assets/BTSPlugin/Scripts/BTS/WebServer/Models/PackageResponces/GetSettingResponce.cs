using System.Collections.Generic;

namespace BTS {
    public class GetSettingResponce: PackageResponse {
        public List<SettingValue> Settings = new List<SettingValue>();
        public override void Parse(Dictionary<string, object> data) {
            var settingsSource = (List<object>)data["settings"];
            foreach (var setting in settingsSource) {
                SettingValue settingValue = new SettingValue();
                settingValue.ParseJSON((Dictionary<string, object>)setting);
                Settings.Add(settingValue);
            }
        }
    }
}