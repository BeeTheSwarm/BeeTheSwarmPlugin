using UnityEngine;
using System.Collections;

public interface IScreenTopPanelViewListener : IViewEventListener
{
    void OnBackPressed();
    void OnTopPanelAvatarPressed();
}
