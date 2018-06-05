using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS
{
    public interface IRequestsService : IService {
        void GetRequests(int offset, int limit, Action<List<InvitationModel>> receiver) ;
    }
}