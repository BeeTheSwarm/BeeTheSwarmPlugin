using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public interface IPlatformAdapter
{
    void GetAddressBookPhones(Action<List<ContactInfo>> callback);
    void GetAddressBookEmails(Action<List<ContactInfo>> callback);
    void GetGalleryImage(Action<Texture2D> callback);
    void SendMessages(string message, List<string> phones, Action<bool> callback);
    void SendEmail(string title, string message, List<string> mails, Action<bool> callback);
    void CopyToClipboard(string content);
}
