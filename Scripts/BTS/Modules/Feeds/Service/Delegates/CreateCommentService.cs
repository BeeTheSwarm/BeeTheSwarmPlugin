using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class CreateCommentService : BaseNetworkService<NoDataResponse>, ICreateCommentService {

        [Inject]
        private IFeedsModel m_model;
        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IUserProfileService m_userService;
        [Inject]
        private IPopupsModel m_popupsModel;
        private Action<bool> m_callback;
        private int m_postId;
        private string m_text;
 

        public void Execute(int postId, string text) {
            m_postId = postId;
            m_text = text;
            SendPackage(new BTS_CreatePostComment(postId, text));
        }

        public override void OnError(BTS_Error error) {
            base.OnError(error);
            if (error.Code == ApiErrors.USER_ALREADY_EXISTS) {
                m_popupsModel.AddPopup(new ErrorPopupItemModel(error.Description));
            }
        }

        protected override void HandleSuccessResponse(NoDataResponse data) {
            m_model.UpdatePostAddComment(m_postId, new CommentModel(m_userModel.User, m_text));
        }

    }
}
