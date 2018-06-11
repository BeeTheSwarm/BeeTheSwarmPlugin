using UnityEngine;
using UnityEditor;

public class CommentViewModel
{
    public readonly Observable<Sprite> UserAvatar = new Observable<Sprite>();
    public string UserName { get; private set;}
    public string Text { get; private set;}

    public CommentViewModel(string userName, string text)
    {
        UserName = userName;
        Text = text;
    }
}