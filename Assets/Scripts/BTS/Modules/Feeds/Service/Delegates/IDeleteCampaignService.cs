using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface IDeleteCampaignService : IService {

        void Execute(Action<bool> callback);
    }
}
