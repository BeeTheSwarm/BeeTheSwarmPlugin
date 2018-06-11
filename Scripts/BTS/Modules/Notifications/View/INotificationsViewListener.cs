using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INotificationsViewListener : IViewEventListener {
    void OnScrolledToEnd();
    void OnBackPressed();
}
