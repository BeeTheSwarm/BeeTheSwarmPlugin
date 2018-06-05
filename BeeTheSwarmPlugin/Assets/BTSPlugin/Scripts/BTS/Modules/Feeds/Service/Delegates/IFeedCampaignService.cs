using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface IFeedCampaignService : IService {
        void Execute(int campaingId, int count);
    }
}
