using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image m_fill;
    [SerializeField]
    private Text m_text;

    public void SetProgress(int progress, int target)
    {
        if (target == -1) {
            m_fill.fillAmount = 1;
            m_text.text = "100% Funded";
        }
        else {
            progress = Mathf.Clamp(progress, 0, 100);
            m_fill.fillAmount = progress / 100f;
            m_text.text = progress.ToString() + "% Funded of " + target + "$";
        }
    }
}
