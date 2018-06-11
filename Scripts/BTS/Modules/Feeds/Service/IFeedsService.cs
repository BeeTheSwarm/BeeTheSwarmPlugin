using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public interface IFeedsService : IService {
        void FeedCampaign(int campaingId, int count);
        void GetCampaignsPosts(int postId, int offset, int limit, Action<List<PostModel>> callback);
        void UpdateCampaign(string title, string description, int category, Texture2D image, string address = "", string website = "", int charity = 0);
        void GetPostComments(int postId, int offset, int limit, Action<List<CommentModel>> callback);
    }
}