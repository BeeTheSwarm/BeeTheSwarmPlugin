using System;
using UnityEngine;

namespace BTS {
    public static class TextureResize {
        public static Texture2D FitTextureToRectangle(this Texture2D texture, int width, int height) {
            if (width < 1 || height < 1) {
                throw new ArgumentException("texture width and height must be greater than 0");
            }
            if (texture.width > width || texture.height > height) {
                var widthScale = (float) width / texture.width;
                var heightScale = (float) height / texture.height;
                var scale = Math.Min(widthScale, heightScale);
                texture.Resize((int) (texture.width * scale), (int) (texture.height * scale));
                texture.Apply();
            }
            return texture;
        }

        public static Texture2D MakeRectangle(this Texture2D texture) {
            if (texture.height == texture.width) {
                return texture;
            }
            int targetTextureSide = Math.Min(texture.height, texture.width);
            Texture2D result = new Texture2D(targetTextureSide, targetTextureSide);
            int x = texture.width > targetTextureSide ? (texture.width - targetTextureSide) / 2 : 0;
            int y = texture.height > targetTextureSide ? (texture.height - targetTextureSide) / 2 : 0;
            result.SetPixels(texture.GetPixels(x, y, targetTextureSide, targetTextureSide));
            result.Apply();
            return result;
        }
    }
}