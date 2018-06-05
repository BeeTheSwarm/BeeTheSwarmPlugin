using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS
{
    public interface IUpdateCampaignService : IService
    { 
        void Execute(string title, int category, string website);
    }
}