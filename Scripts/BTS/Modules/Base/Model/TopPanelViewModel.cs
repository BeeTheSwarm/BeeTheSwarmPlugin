using UnityEngine;
using System.Collections;
using System;

public class TopPanelViewModel
{
    public Observable<int> Level = new Observable<int>();
    public Observable<int> Bees = new Observable<int>();
    public Observable<int> Notifications = new Observable<int>();
    public Observable<Sprite> Avatar = new Observable<Sprite>();
    public Observable<bool> AvatarEnabled = new Observable<bool>();
    public Observable<bool> BackButtonEnabled = new Observable<bool>();
    public Action BackButtonCallback;
    public Action AvatarClickCallback;
}
