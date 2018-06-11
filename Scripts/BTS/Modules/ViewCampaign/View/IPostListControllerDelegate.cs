using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

public interface IPostListControllerDelegate : IViewController {
    void SetView(IPostlistContainer postlistContainer);
    void SetMaxItems(int v);
    bool PostsClickable { get; set; }
    bool PostsEditable { get; set; }
    void SetItemsSource(Action<int, int, Action<List<PostModel>>> getTopCampaigns);
    void Update();
    void Clear();
}
