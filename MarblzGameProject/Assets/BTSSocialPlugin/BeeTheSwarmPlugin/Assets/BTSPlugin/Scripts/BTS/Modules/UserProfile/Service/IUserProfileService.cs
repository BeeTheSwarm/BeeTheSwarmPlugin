using UnityEngine;
using System.Collections;
using System;

namespace BTS
{
    public interface IUserProfileService : IService
    {
        event Action OnUserConnected;
    }
}