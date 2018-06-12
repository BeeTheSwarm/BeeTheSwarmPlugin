using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
namespace BTS {

public class AddPostViewModel : TopPanelViewModel {
    public string Title;
    public string Website;
    public string Description;
    public int Category;
    public readonly ObservableList<CategoryItemViewModel> Categories = new ObservableList<CategoryItemViewModel>();
    public Texture2D Image;
    public Action OnSelectImagePressed;
    public Action<string> OnDescriptionSet;
    public Action<string> OnTitleSet;
    public Action<string> OnWebsiteSet;
    public Action OnNextPressed;
    public Action OnStartPressed;

        public void Clear() {
            Title = string.Empty;
            Website = string.Empty;
            Description = string.Empty;
            Category = 0;
            Image = null;
        }
    }
}