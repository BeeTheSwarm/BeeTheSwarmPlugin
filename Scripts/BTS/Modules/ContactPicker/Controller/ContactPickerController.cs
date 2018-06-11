using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BTS {

    internal class ContactPickerController : BasePopupController<IContactPickerView>, IContactPickerViewListener, IContactPickerController {
        private Action<List<string>> m_callback;
        private ContactPickerViewModel m_viewModel = new ContactPickerViewModel();
        [Inject] private ILoaderController m_loader;
        public ContactPickerController() {
        }

        protected override void PostSetView() {
            base.PostSetView();
            m_view.SetViewModel(m_viewModel);
        }

        public void OnCancel() {
            Hide();
            m_callback.Invoke(new List<string>());
        }

        public override void Hide() {
            m_viewModel.Contacts.Clear();
            base.Hide();
        }

        public void OnSelected() {
            List<string> data = m_viewModel.Contacts.Get().Where(item => { return item.IsChecked; }).Select(item => { return item.ContactInfo; }).ToList();
            m_callback.Invoke(data);
            Hide();
        }

        private void OnEmailsLoaded(List<ContactInfo> contacts) {
            Debug.Log("Got e-mails. Contacts count " + contacts.Count);
            contacts.ForEach(contact => {
                ContactPickerItemViewModel pickerItem = new ContactPickerItemViewModel();
                pickerItem.UserName = contact.Name;
                pickerItem.ContactInfo = contact.Email;
                pickerItem.IsChecked = false;
                m_viewModel.Contacts.Add(pickerItem);
            }
            );
            m_loader.Hide();
        }

        private void OnPhonesLoaded(List<ContactInfo> contacts) {
            Debug.Log("Got phones. Contacts count " + contacts.Count);
            contacts.ForEach(contact => {
                ContactPickerItemViewModel pickerItem = new ContactPickerItemViewModel();
                pickerItem.UserName = contact.Name;
                pickerItem.ContactInfo = contact.Phone;
                pickerItem.IsChecked = false;
                m_viewModel.Contacts.Add(pickerItem);
            }
            );
            m_loader.Hide();
        }

        public void ShowPhonesPicker(Action<List<string>> callback, string header, string button) {
            m_callback = callback;
            base.Show();
            m_view.SetTexts(header, button);
            m_loader.Show("Loading contacts");
            Debug.Log("Show contact picker. Contacts count " + m_viewModel.Contacts.Count());
            Platform.Adapter.GetAddressBookPhones(OnPhonesLoaded);
        }

        public void ShowEmailPicker(Action<List<string>> callback, string header, string button) {
            m_callback = callback;
            base.Show();
            m_view.SetTexts(header, button);
            m_loader.Show("Loading contacts");
            Debug.Log("Show contact picker. Contacts count " + m_viewModel.Contacts.Count());
            Platform.Adapter.GetAddressBookEmails(OnEmailsLoaded);
        }
    }

}