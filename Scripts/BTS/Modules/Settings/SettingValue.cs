using System;
using System.Collections.Generic;

namespace BTS {
    public class SettingValue: DataModel {
        public SettingType Type { get; private set; }
        public int Value { get; private set; }
        public override void ParseJSON(Dictionary<string, object> responseData) {
            Type = (SettingType) Enum.Parse(typeof(SettingType), responseData["type"].ToString());
            Value = int.Parse(responseData["value"].ToString());
        }
    }
}