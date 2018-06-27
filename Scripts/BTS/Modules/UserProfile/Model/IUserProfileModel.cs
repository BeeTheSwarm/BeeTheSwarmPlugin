using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS
{
    public interface IUserProfileModel : IModel
    {
        UserState State { get; set;}
        event Action<UserState> OnUserStateChanged;
        event Action<int> OnLevelUpdated;
        event Action<int, int> OnBeesCountUpdated;
        event Action<float> OnImpactChanged;
        event Action OnUserLoggedIn;
        event Action OnUserLoggedOut;
        event Action OnUserUpdated;
        bool IsLoggedIn { get; }
        int UserId { get; }
        string GetName();
        void SetUserId(int id);
        void SetUserModel(UserModel user);
        UserModel User { get; }

        void SetBees(int bees);
        void SetLevel(int level, int progress);
        void SetImpact(float impact);
    }
}
