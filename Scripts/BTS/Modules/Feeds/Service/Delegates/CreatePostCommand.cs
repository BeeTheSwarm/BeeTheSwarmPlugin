using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class CreatePostCommand : BaseNetworkService<GetPostResponse>,ICreatePostService {

        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IFeedsModel m_feedsModel;

        private Action<bool> m_callback;
        public CreatePostCommand() {
        }

        public void Execute(string title, string description, Texture2D image, Action<bool> callback) {
            m_callback = callback;
            BTS_CreatePost pack = new BTS_CreatePost(title, description, image);
            pack.Handler = this;
            m_networkService.SendPackage(pack);
        }

        

        protected override void HandleSuccessResponse(GetPostResponse data) {
            m_feedsModel.AddUserPost(data.Post);
            m_callback.Invoke(true);
            
        }

    }
}
