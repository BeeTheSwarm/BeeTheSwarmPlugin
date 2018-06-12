using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TopPanelView:MonoBehaviour
{
    [SerializeField]
    private Image m_avatar;

    [SerializeField]
    private Button m_avatarBtn;

    [SerializeField]
    private Text m_beesCount;

    [SerializeField]
    private Text m_level;

    public void SetBees(int bees)
    {
        m_beesCount.text = bees.ToString();
    }

    public void SetLevel(int level)
    {
        m_level.text = "Level " + level.ToString();
    }

    public void OnAvatarClick()
    {
        
    }
    
    public void SetAvatar(Sprite avatar)
    {
        m_avatar.overrideSprite = avatar;
    }
}
