using UnityEngine;
using System.Collections;

namespace BTS {
    internal interface ILocalDataModel : IModel {
        void SaveToken(string authToken);
        void SaveUserId(int userId);
        string GetToken();
        int? GetUserId();
    }
}
