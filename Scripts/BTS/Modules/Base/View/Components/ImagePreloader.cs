using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace BTS {
    public class ImagePreloader {
        private IImagesService m_service;
        private Action<Sprite> m_callback;
        private ImageUrlsModel m_urls;
        public ImagePreloader(IImagesService service) {
            m_service = service;
        }

        public void Load(ImageUrlsModel urls, Action<Sprite> callback) {
            m_callback = callback;
            m_urls = urls;
            Sprite image = m_service.GetImage(urls.ImageUrl);
            if (image != null) {
                m_callback.Invoke(image);
                return;
            }
            LoadThumbnail();
        }

        private void LoadThumbnail() {
            if (string.IsNullOrEmpty(m_urls.ThumbnailUrl)) {
                LoadImage();
            }
            else {
                m_service.GetImage(m_urls.ThumbnailUrl, loadedImage => {
                    m_callback.Invoke(loadedImage);
                    LoadImage();
                });
            }
        }

        private void LoadImage() {
            if (string.IsNullOrEmpty(m_urls.ImageUrl)) {
                return;
            }
            m_service.GetImage(m_urls.ImageUrl, m_callback);
        }

    }
}