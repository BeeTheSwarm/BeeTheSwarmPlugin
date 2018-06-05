using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {
    public class TutorialPostsContainer: BaseFeedsList {
        private RectTransform m_itemTransform;
        private RectTransform m_rectTransform;

        private void Awake() {
            m_rectTransform = GetComponent<RectTransform>();
            
        } 

        protected override FeedsListItem AddItem(PostViewModel postModel) {
            var item = base.AddItem(postModel);
            var fitter = item.gameObject.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            m_itemTransform = item.gameObject.GetComponent<RectTransform>();
            item.DisableComments();
            return item;
        }

        private void Update() {
            if (m_itemTransform != null) {
                if (m_rectTransform.rect.height < m_itemTransform.rect.height) {
                    var scale = m_rectTransform.rect.height / m_itemTransform.rect.height;
                    transform.localScale = new Vector3(scale, scale, 1); 
                }
            }
        }
    }
}