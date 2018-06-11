using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestsUIInGameController : ChestsUIController {

    protected override void Start() {
        base.Start();
        // GameManager.Instance.OnStartGame += RefreshOnStartGame;
        RefreshOnStartGame();
    }

    protected override void OnEnable() {
        RefreshOnStartGame();    
    }

    protected override void OnChestReceivedHandler(int value) {
        int val = value;
        int valueToShow = val - PlayerProgress.Instance.Player.ChestSet *3;
        for (int i = 0; i < m_chests.Length; i++) {
            if (valueToShow == 0 && value > 0)
            {
                m_chests[i].OnchestStateChanged(m_openChest);
            }
            else {
                if (i < valueToShow)
                {
                    m_chests[i].OnchestStateChanged(m_openChest);
                }
                else {
                    m_chests[i].OnchestStateChanged(m_closedChest);
                }
            }
        }
    }

    private void RefreshOnStartGame() {
        base.OnChestReceivedHandler(PlayerProgress.Instance.Player.Chests);
    }

   }
