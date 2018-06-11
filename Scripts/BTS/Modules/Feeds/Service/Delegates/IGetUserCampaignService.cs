using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface IGetUserCampaignService: IService {
        void Execute(Action<List<PostModel>> callback);
    }
}
