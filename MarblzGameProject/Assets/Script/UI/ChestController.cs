using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChestController : MonoBehaviour {

    public static event Action OnChestClicked = delegate { };

    private Button button;
    private Image image;

     void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(OnClick);
    }

    public void OnchestStateChanged(Sprite sprite) {
        if (image)
            image.sprite = sprite;
    }

    private void OnClick() {
        OnChestClicked();
    }

    public void OnRewardReceived() {

    }
}
