using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal interface ICreateCampaignService : IService {
        void Execute(string campaignTitle, int category, string website, string postTitle, string postDescription, Texture2D image, Action<bool> callback);
    }
}
