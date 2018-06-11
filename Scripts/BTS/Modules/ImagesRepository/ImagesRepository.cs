using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ImagesRepository : IImagesRepository
{
    private Dictionary<string, Sprite> m_spritesCache = new Dictionary<string, Sprite>();
    private readonly Sprite m_defaultImage;
    public ImagesRepository() {
        m_defaultImage = Resources.Load<Texture2D>("DefaultImage").ToSprite();
    }

    public Sprite GetSprite(string url)
    {
        if (m_spritesCache.ContainsKey(url))
        {
            return m_spritesCache[url];
        } else
        {
            return null;
        }
    }

    public void SaveImage(string url, Texture2D texture)
    {
        if (!m_spritesCache.ContainsKey(url))
        {
            m_spritesCache.Add(url, texture==null? m_defaultImage:texture.ToSprite());
        }  
    }
}
