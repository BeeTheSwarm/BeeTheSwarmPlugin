using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS {
    public interface IRequestsModel : IModel {
        int LoadedRequests { get; }
        int NewRequests { get; }

        event Action<int> RequestsCountUpdated;
        event Action<InvitationModel> RealtimeRequestAdded;
        event Action<InvitationModel> OnRequestRemoved;
        
        void AddRequests(List<InvitationModel> notifications);
        void InsertRequest(InvitationModel request);
        bool HasLoadedAllRequests();
        InvitationModel GetRequest(int requestId);
        List<InvitationModel> GetRequests(int offset, int limit);
        void RemoveRequest(InvitationModel invitation);
        void SetNewRequestsCount(int count);
        void SignOut();
    }
}