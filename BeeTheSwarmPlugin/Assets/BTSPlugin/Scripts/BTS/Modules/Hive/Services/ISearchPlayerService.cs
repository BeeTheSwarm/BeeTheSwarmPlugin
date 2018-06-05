using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface ISearchPlayerService : IService {
        void Execute(string keyword, int offset, int limit, Action<List<UserModel>> callback);
    }
}
