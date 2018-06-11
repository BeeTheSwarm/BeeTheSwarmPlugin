using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    public abstract class DataModel {
        public abstract void ParseJSON(Dictionary<string, object> responseData);
    }
}