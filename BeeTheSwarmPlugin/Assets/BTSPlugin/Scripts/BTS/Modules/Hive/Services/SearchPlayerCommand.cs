using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class SearchPlayerCommand : BaseNetworkService<SearchHiveResponse>,ISearchPlayerService {
        private Action<List<UserModel>> m_callback;
        

        public void Execute(string keyword, int offset, int limit, Action<List<UserModel>> callback) {
            m_callback = callback;
            SendPackage(new BTS_SearchHive(keyword, offset, limit));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(new List<UserModel>());
        }

        protected override void HandleSuccessResponse(SearchHiveResponse data) {
            m_callback.Invoke(data.Users);
        }
    }
}
