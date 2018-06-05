using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface IGetCampaignsPostsService : IService {
        void Execute(int campaignId, int offset, int limit, Action<List<PostModel>> callback);
    }
}
