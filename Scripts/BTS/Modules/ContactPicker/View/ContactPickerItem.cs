using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContactPickerItem : MonoBehaviour
{
    [SerializeField]
    private Text m_name;
    [SerializeField]
    private Text m_contactInfo;
    [SerializeField]
    private Toggle m_checked;
    
    private ContactPickerItemViewModel m_viewModel;

    public void SetContent(ContactPickerItemViewModel viewModel)
    {
        m_viewModel = viewModel;
        m_name.text = viewModel.UserName;
        m_contactInfo.text = viewModel.ContactInfo;
        m_checked.isOn = m_viewModel.IsChecked;
    }
    
    public void OnToggleClicked()
    {
        m_viewModel.IsChecked = m_checked.isOn;
    }

}
