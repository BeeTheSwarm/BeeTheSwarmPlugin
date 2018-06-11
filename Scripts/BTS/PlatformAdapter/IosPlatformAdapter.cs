using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SA.IOSNative.Contacts;

public class IosPlatformAdapter : IPlatformAdapter {
    public void GetAddressBookEmails(Action<List<ContactInfo>> callback) {
        new EmailLoader().Load(callback);
    }

    public void GetAddressBookPhones(Action<List<ContactInfo>> callback) {
        new PhoneLoader().Load(callback);
    }

    public void GetGalleryImage(Action<Texture2D> callback) {
        new ImageLoader().Load(callback);
    }

    public void SendMessages(string message, List<string> phones, Action<bool> callback) {
        IOSSocialManager.Instance.SendTextMessage(message, phones, (TextMessageComposeResult result) => {
            callback.Invoke(result == TextMessageComposeResult.Sent);
        });
    }
    
    private string JoinTargets(List<string> targetList) {
        var result = targetList[0];
        for (var i= 1; i<targetList.Count; i++) {
            result += ", " +targetList[i];
        }
        return result;
    }

    public void SendEmail(string title, string message, List<string> mails, Action<bool> callback) {
        IOSSocialManager.OnMailResult += result => {
            callback.Invoke(result.IsSucceeded);
        }; 
        IOSSocialManager.Instance.SendMail(title, message, JoinTargets(mails));
    }

    private string EscapeURL(string url) {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }

    public void CopyToClipboard(string content)
    {
        IOSNativeUtility.CopyToClipboard(content);
    }

    private abstract class ContactLoader {
        Action<List<ContactInfo>> m_callback;
        public void Load(Action<List<ContactInfo>> callback) {
            m_callback = callback;
            SA.IOSNative.Contacts.ContactStore.Instance.RetrievePhoneContacts(OnLoaded);
        }

        private void OnLoaded(ContactsResult contactsResult) {
            if (contactsResult.IsSucceeded) {
                m_callback.Invoke(FilterContacts(contactsResult.Contacts));
            }
            else {
                m_callback.Invoke(new List<ContactInfo>());
            }
        }

        protected abstract List<ContactInfo> FilterContacts(List<Contact> contacts);
    }

    private class EmailLoader : ContactLoader {
        protected override List<ContactInfo> FilterContacts(List<Contact> contacts) {
            List<ContactInfo> result = new List<ContactInfo>();
            contacts.ForEach(contact => {
                if (contact.Emails.Count > 0) {
                    foreach (string email in contact.Emails) {
                        ContactInfo contactInfo = new ContactInfo();
                        contactInfo.Name = contact.GivenName + " " + contact.FamilyName;
                        contactInfo.Email = email;
                        contactInfo.Phone = contact.PhoneNumbers.Count > 0 ? contact.PhoneNumbers[0].Digits : string.Empty;
                        result.Add(contactInfo);
                    }
                }

            });
            return result;
        }
    }

    private class PhoneLoader : ContactLoader {
        protected override List<ContactInfo> FilterContacts(List<Contact> contacts) {
            List<ContactInfo> result = new List<ContactInfo>();
            contacts.ForEach(contact => {
                if (contact.PhoneNumbers.Count > 0) {
                    foreach (var phone in contact.PhoneNumbers) {
                        ContactInfo contactInfo = new ContactInfo();
                        contactInfo.Name = contact.GivenName + " " + contact.FamilyName;
                        contactInfo.Email = string.Empty;
                        contactInfo.Phone = phone.Digits;
                        result.Add(contactInfo);
                    }
                }
            });
            return result;
        }
    }

    private class ImageLoader {
        private Action<Texture2D> m_callback;
        public void Load(Action<Texture2D> callback) {
            m_callback = callback;
            IOSCamera.OnImagePicked += OnImagePicked;
            IOSCamera.Instance.PickImage(ISN_ImageSource.Library);
        }

        private void OnImagePicked(IOSImagePickResult result) {
            IOSCamera.OnImagePicked -= OnImagePicked;
            if (result.IsSucceeded) {
                m_callback.Invoke(result.Image);
            }
            else {
                m_callback.Invoke(null);
            }
        }
    }
}
