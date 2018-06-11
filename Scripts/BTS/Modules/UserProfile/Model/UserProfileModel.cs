using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class UserProfileModel : IUserProfileModel {
        public event Action<int> OnLevelUpdated = delegate { };
        public event Action<int> OnBeesCountUpdated = delegate { };
        public event Action<float> OnImpactChanged = delegate {  };
        public event Action OnUserUpdated = delegate { };
        public event Action OnUserLoggedIn = delegate { };
        public event Action OnUserLoggedOut = delegate { };
        public event Action<UserState> OnUserStateChanged = delegate { };
        private UserState m_userState;

        public UserModel User { get; private set; }
        public int UserId { get; private set; }

        public bool IsLoggedIn {
            get {
                return User != null;
            }
        }

        public UserProfileModel() {
            State = UserState.Unknown;
        }
        public UserState State {
            get { return m_userState; }
            set
            {
                m_userState = value;
                OnUserStateChanged.Invoke(value);
            }
        }
        
        public string GetName() {
            return User.Name;
        }

        public void SetUserId(int id) {
            UserId = id;
        }

        public void SetUserModel(UserModel user) {
            if (user == null) {
                State = UserState.Unknown;
                UserId = 0;
                User = null;
                OnUserLoggedOut.Invoke();
            } else {
                bool isNewUser = !IsLoggedIn;
                UserId = user.Id;
                User = user;
                State = UserState.LoggedIn;
                if (isNewUser) {
                    OnUserLoggedIn.Invoke();
                }
                OnUserUpdated.Invoke();
            }
        }

        public void SetBees(int bees) {
            if (IsLoggedIn) {
                User.Bees = bees;
                OnBeesCountUpdated.Invoke(bees);
            }
        }

        public void SetImpact(float impact) {
            if (IsLoggedIn) {
                User.Impact = impact;
                OnImpactChanged.Invoke(impact);
            }
        }
    }
}