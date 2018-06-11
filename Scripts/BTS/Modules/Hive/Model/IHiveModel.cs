using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS {
    internal interface IHiveModel: IModel {
        event Action OnInvitationAdded;
        event Action OnInvitationRemoved;
        event Action OnHiveUpdated;
        List<InvitationModel> GetInvitations();
        int InvitationsCount { get; set;}
        void AddInvitation(List<InvitationModel> invitations);
        void RemoveInvitation(InvitationModel invitation);
        void SignOut();
        void UpdateHive();
    }
}