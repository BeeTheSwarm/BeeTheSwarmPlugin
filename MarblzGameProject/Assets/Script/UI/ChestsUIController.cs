using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestsUIController : MonoBehaviour {

    [SerializeField] protected ChestController[] m_chests;

    [SerializeField] protected Sprite m_openChest;
    [SerializeField] protected Sprite m_closedChest;

    [SerializeField] private GameObject m_noReadyToOpenTreasureTitle;
    [SerializeField] private GameObject m_unlockTreasureTitle;

    protected virtual void Start() {
        PlayerProgress.Instance.Player.OnChestsValueChanged += OnChestReceivedHandler;
        PlayerProgress.Instance.Player.OnChestsSetValueChanged += OnValueChanged;

        OnChestReceivedHandler(PlayerProgress.Instance.Player.Chests);
        OnValueChanged(PlayerProgress.Instance.Player.ChestSet);
    }

    protected virtual void OnEnable() {
        OnChestReceivedHandler(PlayerProgress.Instance.Player.Chests);
        OnValueChanged(PlayerProgress.Instance.Player.ChestSet);
    }

    private void OnDestroy()
    {
        if (PlayerProgress.Instance != null) {
            PlayerProgress.Instance.Player.OnChestsValueChanged -= OnChestReceivedHandler;
            PlayerProgress.Instance.Player.OnChestsSetValueChanged -= OnValueChanged;
        }
    }


    protected virtual void OnChestReceivedHandler(int value) {
        int val = value;
        int valueToShow = val - PlayerProgress.Instance.Player.ChestSet * 3;
        for (int i = 0; i < m_chests.Length; i++) {
            if (i < valueToShow)
            {
                m_chests[i].OnchestStateChanged(m_openChest);
            }
            else {
                m_chests[i].OnchestStateChanged(m_closedChest);
            }
        }
    }

    private void OnValueChanged(int value) {
        if (value > 0)
        {
            if (m_unlockTreasureTitle)
            {
                m_unlockTreasureTitle.SetActive(true);
                m_noReadyToOpenTreasureTitle.SetActive(false);
            }
        }
        else {
            if (m_noReadyToOpenTreasureTitle) {
                m_noReadyToOpenTreasureTitle.SetActive(true);
                m_unlockTreasureTitle.SetActive(false);
            }
        }
    }

}
