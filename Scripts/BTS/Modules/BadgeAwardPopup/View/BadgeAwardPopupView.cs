using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeAwardPopupView : BaseControlledView<IBadgeAwardPopupViewListener>, IBadgeAwardPopupView
{
    [SerializeField]
    private Text m_badgeDescription;
    [SerializeField]
    private Image m_badgeImage;

    public void Setup(Sprite image, string description)
    {
        m_badgeImage.overrideSprite = image;
        m_badgeDescription.text = description;
    }

    public void OnFeedClicked()
    {
        m_controller.OnFeedClick();
    }

    public void OnCloseClicked()
    {
        m_controller.OnCloseClick();
    } 
}
