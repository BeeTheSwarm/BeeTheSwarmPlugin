using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactPickerView : BaseControlledView<IContactPickerViewListener>, IContactPickerView
{
    [SerializeField]
    private ContactPickerItem m_itemOrigin;
    [SerializeField]
    private Transform m_itemsParent;
    [SerializeField] private Text m_header;
    [SerializeField] private Text m_buttonText;
    
    private ContactPickerViewModel m_viewModel;
    private List<ContactPickerItem> m_items = new List<ContactPickerItem>();

    public void SetTexts(string header, string button) {
        m_header.text = header;
        m_buttonText.text = button;
    }
    
    public void SetViewModel(ContactPickerViewModel viewModel)
    {
        m_viewModel = viewModel;
        m_viewModel.Contacts.OnAdd += OnAdd;
        m_viewModel.Contacts.OnClear += ClearList;
    }

    private void OnAdd(ContactPickerItemViewModel obj)
    {
        ContactPickerItem newItem = Instantiate(m_itemOrigin, m_itemsParent);
        newItem.SetContent(obj);
        m_items.Add(newItem);
    }

    public void OnSelectedClick()
    {
        m_controller.OnSelected();
    }

    public void OnCancelClick()
    {
        m_controller.OnCancel();
    }

    private void ClearList()
    {
        m_items.ForEach(item =>
        {
            Destroy(item.gameObject);
        });
        m_items.Clear();
    }
}
