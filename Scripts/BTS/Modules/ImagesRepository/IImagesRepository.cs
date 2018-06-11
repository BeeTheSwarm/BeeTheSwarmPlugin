using UnityEngine;
using System.Collections;

public interface IImagesRepository: IModel
{
    Sprite GetSprite(string url);
    void SaveImage(string url, Texture2D texture);
}
