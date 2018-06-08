using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BlockSmash;

public class Player {

    public Action<int> OnChestsValueChanged      = delegate { };
    public Action<int> OnChestsSetValueChanged   = delegate { };
    public Action<int> OnRewardIdRecieved        = delegate { };  //used or not?

    private int m_chests;
    private int m_rewardId;

    private RewardData[] m_rewardData;

    public int RewardID {
        get {
            return m_rewardId;
        }
        set {
            m_rewardId = value;
            OnRewardIdRecieved(value);
        }
    }

    public RewardData[] UserRewardData {
        get {
            return m_rewardData;
        }
        set {
            m_rewardData = value;
        }
    }

    public int Chests
    {
        get
        {
            return m_chests;
        }
        set
        {
            if (value != m_chests)
            {
                m_chests = value;

                OnChestsValueChanged(m_chests);
                OnChestsSetValueChanged(ChestSet);
            }
        }
    }

    public int ChestSet {
        get {
            return (int)Math.Floor(m_chests / 3f);
        }
    }


    internal void StatusUpdate(int chests) {
        Chests = chests;
    }

    internal void SetReward(int rewardId, RewardData[] reward) {
        RewardID = rewardId;
        UserRewardData = reward;
    }





}
