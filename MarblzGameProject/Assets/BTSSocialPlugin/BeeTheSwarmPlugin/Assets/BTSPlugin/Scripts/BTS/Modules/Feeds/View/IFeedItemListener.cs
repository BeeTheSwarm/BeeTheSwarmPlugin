using UnityEngine;
using System.Collections;

public interface IFeedItemListener
{
    void OnComment(int postId, string text); 
}
