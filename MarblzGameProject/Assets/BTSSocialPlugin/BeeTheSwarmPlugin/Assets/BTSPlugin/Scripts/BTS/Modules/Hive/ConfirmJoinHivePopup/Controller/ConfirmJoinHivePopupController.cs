using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {

    internal class ConfirmJoinHivePopupController : BaseHivePopupController, IConfirmJoinHivePopupController {
        [Inject] private IRequestsModel m_requestsModel;
        [Inject] private IInvitationResponseService m_invitationResponseService;
        [Inject] private IImagesService m_imageService;
        private const int REQUEST_DECLINED = 0;
        private const int REQUEST_ACCEPTED = 1;

        public override void PostInject() {
            base.PostInject();
            m_requestsModel.RealtimeRequestAdded += RequestAddedHandler;
        }

        private InvitationModel m_invitation;
        private void RequestAddedHandler(InvitationModel obj) {
            m_invitation = obj;
            var userViewModel = new UserViewModel(obj.User.Id, obj.User.Name);
            m_imageService.GetImage(obj.User.Avatar, userViewModel.Avatar.Set);
            Show(userViewModel, RealtimeRequestHandler);
        }

        private void RealtimeRequestHandler(bool result) {
            m_invitationResponseService.Execute(m_invitation.Id, result?REQUEST_ACCEPTED:REQUEST_DECLINED);
        }
    }
}
