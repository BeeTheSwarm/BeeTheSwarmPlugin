using System;
using System.Collections.Generic;
using UnityEngine;


public static class BTS_Extensions_Texture2D  {

   


    public static Sprite ToSprite(this Texture2D texture) {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }


   


}
