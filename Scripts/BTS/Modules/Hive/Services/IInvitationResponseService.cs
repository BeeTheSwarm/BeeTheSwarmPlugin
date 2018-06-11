using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface IInvitationResponseService : IService {
        void Execute(int invitation, int status);
    }
}
