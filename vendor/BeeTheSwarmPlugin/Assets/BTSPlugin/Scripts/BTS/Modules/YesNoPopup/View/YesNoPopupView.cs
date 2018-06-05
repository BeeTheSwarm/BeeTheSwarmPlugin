using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesNoPopupView : BaseControlledView<IYesNoPopupViewListener>, IYesNoPopupView {

    [SerializeField] private Text m_message;
    [SerializeField] private Text m_yesButtonText;
    [SerializeField] private Text m_noButtonText;
    
    public void OnYesClick()
    {
        m_controller.OnDelete();
    }

    public void OnNoClick()
    {
        m_controller.OnCancel();
    }

    public void OnOutOfViewClick()
    {
        m_controller.OnCancel();
    }

    public void SetYesButtonLabel(string yesButtonLabel) {
        m_yesButtonText.text = yesButtonLabel;
    }

    public void SetMessage(string message) {
        m_message.text = message;
    }

    public void SetNoButtonLabel(string noButtonLabel) {
        m_noButtonText.text = noButtonLabel;
    }
}
