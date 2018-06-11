using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetPostComments : BaseNetworkService<GetPostCommentsResponse>,IGetPostCommentsService {

        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IFeedsModel m_model;
        [Inject]
        private IUserProfileService m_userService;
        private Action<List<CommentModel>, int> m_callback; 

        public GetPostComments() {
        }

        public void Execute(int postId, int limit, int offset, Action<List<CommentModel>, int> callback) {
            m_callback = callback;
            SendPackage(new BTS_GetPostComments(postId, offset, limit));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            m_callback.Invoke(null, 0);
        }

        protected override void HandleSuccessResponse(GetPostCommentsResponse data) {
            m_callback.Invoke(data.Comments, data.CommentsCount);
        }
    }
}
