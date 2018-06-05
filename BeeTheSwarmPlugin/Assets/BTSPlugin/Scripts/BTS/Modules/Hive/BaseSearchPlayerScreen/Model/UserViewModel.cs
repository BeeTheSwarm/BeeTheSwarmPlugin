using UnityEngine;
using System.Collections;
using System;

namespace BTS {
    internal class UserViewModel {
        public readonly Observable<Sprite> Avatar = new Observable<Sprite>();
        public string Username { get; private set;}
        public int UserId { get; private set; }
        public event Action<UserViewModel> OnClick = delegate { };

        public UserViewModel(int userId, string username) {
            UserId = userId;
            Username = username;
        }

        public void OnItemClicked() {
            OnClick.Invoke(this);
        }
    }
}