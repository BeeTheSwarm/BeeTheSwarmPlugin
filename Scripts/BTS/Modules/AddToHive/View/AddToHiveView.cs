using UnityEngine;
using UnityEngine.UI;

public class AddToHiveView : TopPanelScreen<IAddToHiveViewListener>, IAddToHiveView
{
    [SerializeField]
    private Text m_code;
    
    public void OnCopyCodeClicked()
    {
        m_controller.CopyCodeClicked();
    }

    public void OnSendByMailClicked()
    {
        m_controller.SendByMailClicked();
    }

    public void OnSendByPhoneClicked()
    {
        m_controller.SendByPhoneClicked();
    }

    public void SetCode(string code)
    {
        m_code.text = code;
    }
}
