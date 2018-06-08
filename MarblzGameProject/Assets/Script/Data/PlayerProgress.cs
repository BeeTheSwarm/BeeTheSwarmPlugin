using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BlockSmash;

public class PlayerProgress :SA.Common.Pattern.Singleton<PlayerProgress> {

    public Player m_player;

    public Player Player {
        get {
            return m_player;
        }
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Init();
    }

    public void Init()
    {
        m_player = new Player();
        Debug.Log("Player created");
    }

}
