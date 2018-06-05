using UnityEngine;
using System.Collections;

namespace BTS {
    internal interface IRemoveUserService : IService {
        void Execute(string email);
    }
}
