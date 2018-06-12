using UnityEngine;
using System.Collections;
using System;

namespace BTS {
    internal interface IUpdateUserService : IService {
        void Execute(string name, Texture2D avatar, string password, string newPassword, string confirmNewPassword, Action<string> callback);
    }
}
