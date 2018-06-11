using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRequestsScreenViewListener : IViewEventListener {
    void OnScrolledToEnd();
    void OnBackPressed();
}
