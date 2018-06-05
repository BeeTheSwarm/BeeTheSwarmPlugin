using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Avatar = BTS.Avatar;

public class PostCommentItem : MonoBehaviour
{
    [SerializeField]
    private Sprite m_oddItemBg;
    [SerializeField]
    private Sprite m_evenItemBg;
    [SerializeField]
    private Image m_background;
    [SerializeField]
    private Avatar m_avatar;
    [SerializeField]
    private Text m_commentText;

    private const string COMMENT_TEMPLATE = "<color=#a3c3ff>{0}</color> {1}";
    internal void SetViewModel(CommentViewModel comment)
    {
        comment.UserAvatar.Subscribe(m_avatar.SetAvatar);
        m_commentText.text = string.Format(COMMENT_TEMPLATE, comment.UserName, comment.Text);
    }

    internal void SetBackground(bool v) {
        m_background.overrideSprite = v? m_oddItemBg: m_evenItemBg;
    }
}
