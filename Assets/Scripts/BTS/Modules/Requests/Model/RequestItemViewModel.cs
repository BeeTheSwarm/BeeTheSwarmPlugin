using UnityEngine;
using System.Collections;
using System;

namespace BTS {
    public class RequestItemViewModel {
        public readonly Observable<Sprite> UserAvatar = new Observable<Sprite>();
        public string Username { get; private set;}
        public int Id { get; private set; }
        public InvitationType RequestType { get; private set;}
        public event Action<RequestItemViewModel> OnAccept = delegate { };
        public event Action<RequestItemViewModel> OnDecline = delegate { };

        public RequestItemViewModel(int id, string username, InvitationType type) {
            Id = id;
            Username = username;
            RequestType = type;
        }

        public void Accept() {
            OnAccept.Invoke(this);
        }

        public void Decline() {
            OnDecline(this);
        }
    }
}