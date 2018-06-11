using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class RequestsModel : IRequestsModel {
        public int NewRequests
        {
            get { return m_newRequests; }
        }
        public event Action<int> RequestsCountUpdated = delegate { };
        public event Action<InvitationModel> RealtimeRequestAdded = delegate { };
        public event Action<InvitationModel> OnRequestRemoved = delegate { };


        private List<InvitationModel> m_requests;
        private int m_totalRequests = -1;
        private int m_newRequests = -1;

        public int LoadedRequests
        {
            get { return m_requests.Count; }
        }

        public int TotalRequests
        {
            get { return m_totalRequests; }
        }

        public RequestsModel() {
            m_requests = new List<InvitationModel>();
        }

        public void AddRequests(List<InvitationModel> notifications) {
            m_requests.AddRange(notifications);
        }

        public void InsertRequest(InvitationModel request) {
            m_requests.Insert(0, request);
            RealtimeRequestAdded.Invoke(request);
        }

        public bool HasLoadedAllRequests() {
            return m_requests.Count == m_totalRequests;
        }

        public InvitationModel GetRequest(int requestId) {
            return m_requests.Find(r => r.Id == requestId);
        }

        public void RemoveRequest(InvitationModel invitation) {
            var request = GetRequest(invitation.Id);
            if (request != null) {
                m_requests.Remove(request);
                SetNewRequestsCount(NewRequests - 1);
                OnRequestRemoved.Invoke(request);
            }
        }

        public void SetNewRequestsCount(int count) {
            m_newRequests = count;
            RequestsCountUpdated.Invoke(count);
        }

        public void SignOut() {
            m_requests.Clear();
            SetNewRequestsCount(0);
        }

        public List<InvitationModel> GetRequests(int offset, int limit) {
            if (offset > m_requests.Count) {
                return new List<InvitationModel>();
            }

            limit = Math.Min(limit, m_requests.Count - offset);
            return m_requests.GetRange(offset, limit);
        }

    }
}