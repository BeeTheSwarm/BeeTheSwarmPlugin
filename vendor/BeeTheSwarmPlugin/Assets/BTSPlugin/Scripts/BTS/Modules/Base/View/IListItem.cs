using UnityEngine;
using System.Collections;

public interface IListItem {
    GameObject gameObject { get; }
    Transform transform { get; }
    void SetViewModel(IListItemViewModel viewmodel);
}
