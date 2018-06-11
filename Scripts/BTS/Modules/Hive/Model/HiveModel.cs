using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS {
    public class HiveModel : IHiveModel {
        private List<InvitationModel> m_invitations = new List<InvitationModel>();
        private int m_invitationsCount;
        public event Action OnInvitationAdded = delegate { };
        public event Action OnInvitationRemoved = delegate { };
        public event Action OnHiveUpdated = delegate {  };

        public int InvitationsCount
        {
            get { return m_invitationsCount; }
            set { m_invitationsCount = value; }
        }

        
        public List<InvitationModel> GetInvitations() {
            return m_invitations.GetRange(0, m_invitations.Count);
        }

        public void RemoveInvitation(InvitationModel invitation) {
            if (m_invitations.Contains(invitation)) {
                m_invitations.Remove(invitation);
                m_invitationsCount--;
                OnInvitationRemoved.Invoke();
            }
        }

        public void SignOut() {
        }

        public void UpdateHive() {
            OnHiveUpdated.Invoke();
        }

        public void AddInvitation(List<InvitationModel> invitations) {
            if (invitations.Count > 0) {
                m_invitations.AddRange(invitations);
                OnInvitationAdded.Invoke();
            }
        }
    }
}