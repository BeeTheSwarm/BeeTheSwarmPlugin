using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    internal class UpdatePostService : BaseNetworkService<GetPostResponse>, IUpdatePostService {

        [Inject] private IFeedsModel m_feedModel;

        public void Execute(int postId, string title, string description, Texture2D image) {
            SendPackage(new BTS_UpdatePost(postId, title, description, image));
        }
        
        protected override void HandleSuccessResponse(GetPostResponse data) {
            m_feedModel.UpdatePosts(new List<PostModel>() { data.Post});
        }
    }
}