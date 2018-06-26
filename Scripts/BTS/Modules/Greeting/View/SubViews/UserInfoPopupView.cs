using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BTS {

    public class UserInfoPopupView : BaseGreetingPopupView {
        [SerializeField]
        private Text m_level;
        [SerializeField]
        private Text m_bees;
        [SerializeField]
        private Image m_progress;
        [SerializeField]
        private Image m_shine;

        private UserInfoPopupItemModel m_model;
        private const float FILL_SPEED = 10;
        public void Setup(UserInfoPopupItemModel model) {
            m_model = model;
            m_bees.text = model.Bees.ToString();
        }

        public override IEnumerator Animate() {
            if (m_progress.fillAmount > m_model.Progress / 100f) {
                yield return AnimateToFullFill(1);
                yield return AnimateShine();
                m_level.text = m_model.Level.ToString();
            }
            yield return AnimateToFullFill(m_model.Progress / 100f);
        }

        public IEnumerator AnimateToFullFill(float target) {
            while (m_progress.fillAmount < target) {
                m_progress.fillAmount += FILL_SPEED * Time.unscaledDeltaTime;
                yield return null;
            }
            m_progress.fillAmount = target;
        }

        public IEnumerator AnimateShine() {
            float a = 0;
            while (a < Mathf.PI) {
                SetShineAlpha(Mathf.Cos(a));
                a += ( Mathf.PI / 2 ) * Time.unscaledDeltaTime;
                yield return null;
            }
            SetShineAlpha(0f);
        }

        private void SetShineAlpha(float alpha) {
            Color c = m_shine.color;
            c.a = alpha;
            m_shine.color = c;
        }
    }

}