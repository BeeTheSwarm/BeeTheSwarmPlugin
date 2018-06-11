using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class RequestsService : BaseService, IRequestsService {

        [Inject]
        private IRequestsModel m_model;
        [Inject]
        private IUserProfileModel m_userModel;
        [Inject]
        private IGetRequestsCommand m_getRequestsService;
        [Inject]
        private ILoadRequestsCommand m_loadRequestsService;
        
        public override void PostInject() {
            m_userModel.OnUserLoggedIn += OnUserLoggedIn;
        }

        private void OnUserLoggedIn() {
            m_userModel.OnUserLoggedIn -= OnUserLoggedIn;
        //    m_getRequestsService.Execute(0, 10);
        }

        public void GetRequests(int offset, int limit, Action<List<InvitationModel>> receiver) {
            if (m_model.HasLoadedAllRequests()) {
                receiver.Invoke(new List<InvitationModel>());
                return;
            }
            if (offset + limit > m_model.LoadedRequests) {
                m_loadRequestsService.Execute(m_model.LoadedRequests, offset + limit - m_model.LoadedRequests, receiver);
            }
        }
    }
}