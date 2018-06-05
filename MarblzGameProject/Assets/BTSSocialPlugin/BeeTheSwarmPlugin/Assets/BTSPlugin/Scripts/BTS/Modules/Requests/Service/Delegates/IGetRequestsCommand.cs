using UnityEngine;
using System.Collections;

namespace BTS {
    internal interface IGetRequestsCommand : IService {
        void Execute(int offset, int limit);
    }
}
