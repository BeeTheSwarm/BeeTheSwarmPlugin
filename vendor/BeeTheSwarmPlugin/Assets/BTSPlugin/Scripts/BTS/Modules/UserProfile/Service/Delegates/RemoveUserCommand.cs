using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class  RemoveUserCommand : BaseNetworkService<NoDataResponse>, IRemoveUserService {
    private Action<bool> m_callback;

    public RemoveUserCommand() {
    }

    public void Execute(string email) {
#if UNITY_EDITOR
        BTS_RemoveUser removePack = new BTS_RemoveUser(email);
        removePack.Handler = this;
        m_networkService.SendPackage(removePack);
#endif
    }


        protected override void HandleSuccessResponse(NoDataResponse data) {
    }

    }
}
