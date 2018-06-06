using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetCampaingLevelsCommand : BaseNetworkService<GetCampaignLevelsResponce>,IGetCampaingLevelsService {

    [Inject]
        private IFeedsModel m_model;
   [Inject]
         private IUserProfileService m_userService;


    public GetCampaingLevelsCommand() {
    }

    public void Execute() {
            if (m_model.LevelsLoaded)
            {
                return;
            }
        SendPackage(new BTS_GetCampaignLevels());
    }
        
        protected override void HandleSuccessResponse(GetCampaignLevelsResponce data) {
        m_model.SetCampainLevels(data.Levels);
    }
        
    }
}
