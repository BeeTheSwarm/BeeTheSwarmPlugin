using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform m_mask;
    [SerializeField]
    private Text m_text;

    public void SetProgress(int progress, int target)
    {
        if (target == -1) {
            m_mask.anchorMax = new Vector2(1, 1);
            m_text.text = "100% Funded";
        }
        else {
            progress = Mathf.Clamp(progress, 0, 100);
            m_mask.anchorMax = new Vector2(progress / 100f, 1);
            m_text.text = progress.ToString() + "% Funded of " + target + "$";
        }
    }
}
