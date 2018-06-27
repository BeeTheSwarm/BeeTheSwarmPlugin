using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EditorPlatformAdapter : IPlatformAdapter {
    public void GetAddressBookEmails(Action<List<ContactInfo>> callback) {
        callback.Invoke(GetMockupContactList());
    }

    private List<ContactInfo> GetMockupContactList() {
        List<ContactInfo> result = new List<ContactInfo>();
        ContactInfo contactInfo;

        for (var i = 0; i < 100 ; i++) {
            contactInfo = new ContactInfo();
            contactInfo.Name = string.Format("Test name {0}", i);
            contactInfo.Email = string.Format("mail{0}@mailprovider.com", i);
            contactInfo.Phone = string.Format("+380671122{0}", i.ToString("D3"));
            result.Add(contactInfo);
        }
        return result;
    }

    public void GetAddressBookPhones(Action<List<ContactInfo>> callback) {
        callback.Invoke(GetMockupContactList());
    }


    public void GetGalleryImage(Action<Texture2D> callback) {
        Texture2D texture = new Texture2D(1024, 1024);
        Color fill = Random.ColorHSV();
        var pixels = texture.GetPixels();
        for (var i = 0; i < pixels.Length; ++i) {
            pixels[i] = fill;
        }
        texture.SetPixels(pixels);
        texture.Apply();
        callback.Invoke(texture);
    }

    public void SendMessages(string message, List<string> phones, Action<bool> callback) {
        Debug.Log("Send sms " + message);
        callback.Invoke(true);
    }

    public void SendEmail(string title, string message, List<string> mails, Action<bool> callback) {
        Debug.Log("Send mail " + title + " " + message);
        callback.Invoke(true);
    }

    public void CopyToClipboard(string content)
    {
        Debug.Log("Copied to clipboard " + content);
    }
}