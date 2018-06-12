using UnityEngine;
using System.Collections;
using System;

namespace BTS {
    public class UserProfileService : BaseService, IUserProfileService {
        [Inject]
        IUserProfileModel m_userModel;
        [Inject]
        private IPopupsModel m_popupsModel;
        [Inject]
        private ILocalDataModel m_localData;
        [Inject]
        private IGetUserService m_getUserService;
        
        
        public event Action OnUserConnected;
        
    }
}