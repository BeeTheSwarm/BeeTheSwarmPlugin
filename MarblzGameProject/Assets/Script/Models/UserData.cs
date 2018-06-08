using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BlockSmash
{

    [Serializable]
    public class UserData
    {
        public User user;
    }

    [Serializable]
    public class UserReward
    {
        public User user;
        public RewardData[] rewards;
    }

    [Serializable]
    public class User
    {
        public int chests;
    }

    [Serializable]
    public class RewardData
    {
        public int type;
        public Reward reward;
    }

    [Serializable]
    public class UpdatedReward
    {
        public int type;
        public User user;
    }

    [Serializable]
    public class Reward
    {
        public int quality = 0;
        public int type = 0;
        public int count;
    }
}