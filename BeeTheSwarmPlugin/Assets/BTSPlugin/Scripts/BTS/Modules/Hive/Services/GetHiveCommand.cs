using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetHiveCommand : BaseNetworkService<GetHiveResponse>, IGetHiveService {
        private Action<List<UserModel>, int, UserModel> m_callback;
        public GetHiveCommand() {
        }

        public void Execute(int hiveId, Action<List<UserModel>, int, UserModel> callback, int offset = 0, int limit = 0) {
            m_callback = callback;
            SendPackage(new BTS_GetHive(hiveId, offset, limit));
        }


        protected override void HandleSuccessResponse(GetHiveResponse data) {
            m_callback.Invoke(data.Users, data.MembersCount, data.Parent);
        }
    }
}
