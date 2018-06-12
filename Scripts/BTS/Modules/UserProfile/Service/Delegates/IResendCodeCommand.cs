using UnityEngine;
using System.Collections;

namespace BTS {
    internal interface IResendCodeService : IService {
        void Execute(string email);
    }
}
