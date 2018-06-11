using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class CampaignCategoriesService : BaseService, ICampaignCategoriesService {
        [Inject]
        private INetworkService m_networkService;
        [Inject]
        private ICampaignCategoriesModel m_model;
        [Inject]
        private IUserProfileModel m_userProfileModel;
        [Inject]
        private IGetCampaignCategoriesService m_getCampaignCategoriesService;

        public CampaignCategoriesService() {

        }

        public override void PostInject() {
        /*    base.PostInject();
            if (m_userProfileModel.IsLoggedIn) {
                GetCampaignCategories();
            }
            else {
                m_userProfileModel.OnUserLoggedIn += GetCampaignCategories;
            }
        */
        }


        public void GetCampaignCategories() {
            m_userProfileModel.OnUserLoggedIn -= GetCampaignCategories;
            m_getCampaignCategoriesService.Execute();
        }

    }
}