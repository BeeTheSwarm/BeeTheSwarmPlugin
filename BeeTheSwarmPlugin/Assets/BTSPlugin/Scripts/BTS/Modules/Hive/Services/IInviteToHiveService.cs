using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;
namespace BTS {
    internal interface IInviteToHiveService : IService {
        void Execute(int userId);
    }
}
