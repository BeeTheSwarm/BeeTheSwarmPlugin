using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISearchReffererViewListener : IViewEventListener {
    void SearchFieldChanged(string text);
    void OnScrolledToTheEnd();
}
