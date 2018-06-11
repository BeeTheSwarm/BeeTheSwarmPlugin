using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChestCounter : Counter {

    [SerializeField] private Image holder;

     void Start()
    {
        base.Start();

        PlayerProgress.Instance.Player.OnChestsSetValueChanged += OnValueChanged;
        OnValueChanged(PlayerProgress.Instance.Player.ChestSet);
    }

    private void OnDestroy()
    {
        if (PlayerProgress.Instance != null) {
            PlayerProgress.Instance.Player.OnChestsSetValueChanged -= OnValueChanged;
        }
    }

    protected override void OnValueChanged(int value)
    {
        if (value > 0)
        {
            holder.enabled = true;
            this.value.gameObject.SetActive(true);
            this.value.text = value.ToString();
        }
        else {
            holder.enabled = false;
            gameObject.SetActive(false);
        }
    }

}
