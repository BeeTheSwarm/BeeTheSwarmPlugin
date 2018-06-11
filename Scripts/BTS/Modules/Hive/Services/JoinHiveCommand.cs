using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

namespace BTS {
    internal class JoinHiveCommand : BaseNetworkService<GetUserResponce>, IJoinHiveService {
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IPopupsModel m_popupsModel;
        [Inject] private IUpdateHiveService m_updateHiveService;
        public void Execute(int userId) {
            SendPackage(new BTS_JoinHive(userId));
        }

        protected override void HandleSuccessResponse(GetUserResponce data) {
            if (data.User != null) {
                m_userModel.User.UpdateInfo(data.User);
                if (data.User.HiveId != 0) {
                    m_popupsModel.AddPopup(new ErrorPopupItemModel("Welcome to hive"));
                    m_updateHiveService.Execute();
                }
            }
        }
    }
}
