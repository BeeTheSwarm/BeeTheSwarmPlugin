using UnityEngine;
using UnityEditor;
using System;

public class UpdateCampaignViewModel : TopPanelViewModel {
    public string Title;
    public string Website;
    public string Description;
    public Texture2D Image;
    public Sprite ImageSprite;
    public Action OnSelectImagePressed;
    public Action<string> OnDescriptionSet;
    public Action<string> OnTitleSet;
    public Action<string> OnWebsiteSet;
    public Action OnNextPressed;
    public Action OnStartPressed;
}