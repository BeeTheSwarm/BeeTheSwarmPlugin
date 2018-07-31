using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AndroidPlatformAdapter : IPlatformAdapter {
    public void GetAddressBookEmails(Action<List<ContactInfo>> callback) {
        new EmailLoader().Load(callback);
    }

    public void GetAddressBookPhones(Action<List<ContactInfo>> callback) {
        new PhoneLoader().Load(callback);
    }

    public void GetGalleryImage(Action<Texture2D> callback) {
        new ImageLoader().Load(callback);
    }

    public void SendEmail(string title, string message, List<string> mails, Action<bool> callback) {
        
        AndroidSocialGate.OnShareIntentCallback += (success, data) => {
            callback.Invoke(success);
        };

        string mailsStr = string.Empty;
        foreach(var m in mails) {
            mailsStr += m + ",";
        }

        if(mailsStr.Length > 1) {
            mailsStr = mailsStr.Remove(mailsStr.Length - 1);
        }
        AndroidSocialGate.SendMail(title, message, title, mailsStr);
      
    }

    private string JoinTargets(List<string> targetList) {
        var result = targetList[0];
        for (var i = 1; i < targetList.Count; i++) {
            result += ", " + targetList[i];
        }
        return result;
    }

    public void SendMessages(string message, List<string> phones, Action<bool> callback) {
        AndroidSocialGate.OnShareIntentCallback += (success, data) => {
            callback.Invoke(success);
        };
        AndroidSocialGate.SendTextMessage(message, phones);
    }

    public void CopyToClipboard(string content)
    {
        AndroidNative.CopyToClipboard(content);
    }

    private abstract class ContactLoader {
        Action<List<ContactInfo>> m_callback;
        public void Load(Action<List<ContactInfo>> callback) {
            m_callback = callback;
            if (AddressBookController.isLoaded) {
                m_callback.Invoke(FilterContacts(AddressBookController.Instance.contacts));
            }
            else {
                AddressBookController.OnContactsLoadedAction += OnLoaded;
                AddressBookController.Instance.LoadContacts();
            }
        }

        private void OnLoaded() {
            AddressBookController.OnContactsLoadedAction -= OnLoaded;
            m_callback.Invoke(FilterContacts(AddressBookController.Instance.contacts));
        }

        protected abstract List<ContactInfo> FilterContacts(List<AndroidContactInfo> contacts);
    }
    
    private class EmailLoader : ContactLoader {
        protected override List<ContactInfo> FilterContacts(List<AndroidContactInfo> contacts) {
            var result = new List<ContactInfo>();
            contacts.ForEach(contact => {
                try {
                    if (contact.email != null && contact.email.email.Contains("@")) {
                        ContactInfo contactInfo = new ContactInfo();
                        contactInfo.Name = contact.name;
                        contactInfo.Email = contact.email.email;
                        contactInfo.Phone = contact.phone;
                        result.Add(contactInfo);
                    }
                }
                catch (Exception e) {
                    if (contact != null) {
                        Debug.Log(e.Message + " (contact " + contact.ToString());
                    } else {
                        Debug.Log(e.Message + "contact == null ");
                    }
                }
            });
            return result;
        }
    }
    
    private class PhoneLoader : ContactLoader {
        protected override List<ContactInfo> FilterContacts(List<AndroidContactInfo> contacts) {
            var result = new List<ContactInfo>();
            contacts.ForEach(contact => {
                if (!string.IsNullOrEmpty(contact.phone)) {
                    ContactInfo contactInfo = new ContactInfo();
                    contactInfo.Name = contact.name;
                    contactInfo.Email = contact.email.email;
                    contactInfo.Phone = contact.phone;
                    result.Add(contactInfo);
                }
            });
            return result;
        }
    }

    private class ImageLoader {
        private Action<Texture2D> m_callback;
        public void Load(Action<Texture2D> callback) {
            
            m_callback = callback;
            AndroidCamera.Instance.OnImagePicked += OnImagePicked;
            AndroidCamera.Instance.GetImageFromGallery();
        }
        
        private void OnImagePicked(AndroidImagePickResult obj) {
            AndroidCamera.Instance.OnImagePicked -= OnImagePicked;
            if (obj.IsSucceeded) {
                m_callback.Invoke(obj.Image);
            }
            else {
                m_callback.Invoke(null);
            }
        }
    }
}
