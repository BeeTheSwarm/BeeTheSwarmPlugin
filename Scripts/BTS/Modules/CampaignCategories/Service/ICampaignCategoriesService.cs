using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS
{
    public interface ICampaignCategoriesService : IService
    { 
        void GetCampaignCategories();
        
    }
}