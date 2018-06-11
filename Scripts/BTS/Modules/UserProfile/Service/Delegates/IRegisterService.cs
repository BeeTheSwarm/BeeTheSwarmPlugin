using UnityEngine;
using System.Collections;
using System;

namespace BTS {
    internal interface IRegisterService: IService {
        void Execute(string name, string email, string password, string referalCode, Action<bool> callback);
    }
}
