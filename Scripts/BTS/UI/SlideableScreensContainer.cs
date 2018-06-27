using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

namespace BTS {
    [RequireComponent(typeof(RectTransform))]
    public class SlideableScreensContainer : MonoBehaviour {
        private RectTransform m_rectTransform;
        private Vector2 m_minAchors;
        private Vector2 m_maxAchors;
        private float m_rectHeight = -1f; 

        private void Awake() {
            m_rectTransform = GetComponent<RectTransform>();
            m_minAchors = m_rectTransform.anchorMin;
            m_maxAchors = m_rectTransform.anchorMax;
            InputFieldsHandler.SetView(this);
        }

        public void Restore() {
            m_rectTransform.anchorMin = m_minAchors;
            m_rectTransform.anchorMax = m_maxAchors;
        }

        public void Slide(Rect rect) {
            if (TouchScreenKeyboard.isSupported) {
                StartCoroutine(SlideKeyboard(rect));
            }
        }

        public void UpdatePosition(RectTransform getComponent, Rect getComponentRect) {
            if (TouchScreenKeyboard.isSupported) {
                var keyboardHeight = TouchScreenKeyboard.area.height / Screen.height;
                var inputFieldComponent = getComponent.GetComponentInChildren<BTS_InputField>();
                float componentHeight;
                if (inputFieldComponent != null) {
                    componentHeight = inputFieldComponent.GetComponent<RectTransform>().rect.height / Screen.height;
                } else {
                    componentHeight = getComponentRect.height;
                }

                if (m_rectHeight < 0)
                    m_rectHeight = componentHeight;
                
                if (componentHeight > m_rectHeight) {
                    var delta = componentHeight - m_rectHeight;
                    if ((getComponent.anchorMin.y + delta) <= keyboardHeight) {
                        m_rectTransform.anchorMin = new Vector2(m_minAchors.x, m_rectTransform.anchorMin.y + delta);
                        m_rectTransform.anchorMax = new Vector2(m_maxAchors.x, m_rectTransform.anchorMax.y + delta);
                    } else {
                        Transform parent = getComponent.parent;
                        while (parent != null) {
                            ScrollRect scrollRect = parent.GetComponentInParent<ScrollRect>();
                            if (scrollRect != null) {
                                    RectTransform contentRect = scrollRect.content.GetComponent<RectTransform>();
                                    Vector3 lerpTarget = new Vector3(contentRect.localPosition.x, contentRect.localPosition.y + (delta * Screen.height), 0);
                                    contentRect.localPosition = Vector3.Lerp(contentRect.localPosition, lerpTarget, 27.5f * Time.deltaTime);
                                    break;
                            }
                            parent = parent.transform.parent;
                        }
                    }
                }
                m_rectHeight = componentHeight;
            }
        }

        private IEnumerator SlideKeyboard(Rect rect) {
            float delta = 0f;
        #if UNITY_IOS
            yield return new WaitUntil(() => TouchScreenKeyboard.visible);
            yield return new WaitUntil(() => TouchScreenKeyboard.area.height > 0);
            delta = TouchScreenKeyboard.area.height / Screen.height; 
        #elif UNITY_ANDROID
            yield return new WaitForSeconds(0.5f);
            delta = (Screen.height / 2f) / Screen.height;
            if (!TouchScreenKeyboard.visible) {
                yield break;
            }
#else 
            yield break;
#endif

            if (delta  > rect.y) {
                m_rectTransform.anchorMin = new Vector2(m_minAchors.x, delta - rect.y);
                m_rectTransform.anchorMax = new Vector2(m_maxAchors.x, delta - rect.y + 1);
            }

        }
    }
}