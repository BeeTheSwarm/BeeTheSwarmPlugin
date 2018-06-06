using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BTS {
    public class ImagesService : BaseService, IImagesService {
        [Inject]
        private IImagesRepository m_model;
        private Dictionary<string, ImageLoader> m_loaders = new Dictionary<string, ImageLoader>();
        private IContext m_context;
        public ImagesService(IContext context) {
            m_context = context;
        }

        public void GetImage(string url, Action<Sprite> callback) {
            Sprite sprite = m_model.GetSprite(url);
            if (sprite != null) {
                callback.Invoke(sprite);
            }
            else {
                if (m_loaders.ContainsKey(url)) {
                    m_loaders[url].OnLoad += () => { callback.Invoke(m_model.GetSprite(url)); };
                    return;
                }
                ImageLoader loader = new ImageLoader(url, texture => { m_model.SaveImage(url, texture); });
                loader.OnLoad += () => {
                    callback.Invoke(m_model.GetSprite(url));
                };
                m_loaders.Add(url, loader);
                m_context.StartCoroutine(loader.Load());
            }
        }

        public Sprite GetImage(string url) {
            return m_model.GetSprite(url);
        }

        public void GetImage(ImageUrlsModel urls, Action<Sprite> callback) {
            if (urls == null) {
                return;
            }
            new ImagePreloader(this).Load(urls, callback);
        }

        private class ImageLoader {
            private string m_url;
            private Action<Texture2D> m_callback;
            public event Action OnLoad = delegate { };

            public ImageLoader(string url, Action<Texture2D> callback) {
                m_url = url;
                m_callback = callback;
            }

            public IEnumerator Load() {
                WWW www = new WWW(m_url);
                yield return www;
                if (string.IsNullOrEmpty(www.error)) {
                    m_callback.Invoke(www.texture);
                }
                else {
                    m_callback.Invoke(null);
                }
                OnLoad.Invoke();
            }
        }

    }
}