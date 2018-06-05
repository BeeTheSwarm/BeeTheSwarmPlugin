using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class GetCampaignCategoriesCommand : BaseNetworkService<GetCampaignCategoriesResponse>, IGetCampaignCategoriesService {
        [Inject]
        private ICampaignCategoriesModel m_model;
        public GetCampaignCategoriesCommand() {

        }

        public void Execute() {
            if (m_model.GetCategories().Count > 0)
            {
                return;
            }
            BTS_GetCampaignCategories pack = new BTS_GetCampaignCategories();
            SendPackage(pack);
        }

        protected override void HandleSuccessResponse(GetCampaignCategoriesResponse data) {
            m_model.SetCategories(data.Categories);
        }
    }
}
