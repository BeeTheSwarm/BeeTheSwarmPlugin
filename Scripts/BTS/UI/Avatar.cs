using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(AspectRatioFitter))]
    public class Avatar: MonoBehaviour {
        [SerializeField] private Image m_image;
        [SerializeField] private AspectRatioFitter m_aspectRatioFitter;
        private void Awake() {
            m_aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        }

        public void SetAvatar(Sprite image) {
            m_image.overrideSprite = image;
            if (image != null) {
                m_aspectRatioFitter.aspectRatio = (float) image.texture.width / image.texture.height;
            }
        }
    }
}