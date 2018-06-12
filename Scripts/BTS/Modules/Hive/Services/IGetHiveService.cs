using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface IGetHiveService : IService {
        void Execute(int hiveId, Action<List<UserModel>, int, UserModel> callback, int offset = 0, int limit = 0);
    }
}
