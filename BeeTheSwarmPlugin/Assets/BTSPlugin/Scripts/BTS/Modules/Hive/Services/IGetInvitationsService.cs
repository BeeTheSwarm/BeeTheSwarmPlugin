using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface IGetInvitationsService : IService {
        void Execute(int offset, int limit, Action<List<InvitationModel>, int> callback);
    }
}
