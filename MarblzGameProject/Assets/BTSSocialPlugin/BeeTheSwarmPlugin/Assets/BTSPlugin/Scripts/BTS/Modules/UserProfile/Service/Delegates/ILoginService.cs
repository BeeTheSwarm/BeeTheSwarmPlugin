using UnityEngine;
using System.Collections;
using System;

namespace BTS {

    internal interface ILoginService: IService {
        void Execute(string email, string password, Action<bool> callback);
    }

}