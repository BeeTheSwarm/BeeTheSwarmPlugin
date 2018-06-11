using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class DeletePostCommand : BaseNetworkService<NoDataResponse>, IDeletePostService {
        [Inject] private IFeedsModel m_feedsModel;
        private int m_postId;
        public void Execute(int postId, Action<bool> callback) {
            m_postId = postId;
            SendPackage(new BTS_DeletePost(postId));
        }

        protected override void HandleSuccessResponse(NoDataResponse data) {
            m_feedsModel.DeletePost(m_postId);
        }

    }
}
