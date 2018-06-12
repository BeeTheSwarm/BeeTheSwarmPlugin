using UnityEngine;
using System.Collections;
using System;

namespace BTS
{
    public interface INetworkService : IService
    {
        string AuthToken { get; set; }

        event Action<NetworkState> OnStateChanged;
        void SendPackage(IBasePackage pack);
        void SetGameId(string gameId);
        void SignOut();
    }
}
