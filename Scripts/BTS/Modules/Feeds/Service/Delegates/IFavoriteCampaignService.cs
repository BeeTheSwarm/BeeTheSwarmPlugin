using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;
namespace BTS {
    internal interface IFavoriteCampaignService : IService {
        void Execute(int postId);

    }
}
