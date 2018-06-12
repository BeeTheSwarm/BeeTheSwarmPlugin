using UnityEngine;
using System.Collections;
using System;

namespace BTS {
    internal interface IConfirmRegistrationService : IService {
        void Execute(int verificationCode, Action<bool> callback);
    }
}
