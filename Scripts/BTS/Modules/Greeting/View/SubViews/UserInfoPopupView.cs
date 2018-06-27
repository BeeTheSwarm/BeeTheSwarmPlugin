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

        private bool m_firstRun = true;
        private UserInfoPopupItemModel m_model;
        private const float FILL_SPEED = 0.33f;
        public void Setup(UserInfoPopupItemModel model) {
            m_model = model;
            m_bees.text = model.Bees.ToString();
            if (m_firstRun) {
                m_level.text = m_model.Level.ToString();
            }
        }

        public override IEnumerator Animate() {
            if (!m_level.text.Equals(m_model.Level.ToString())) {
                yield return AnimateToFill(1);
                yield return AnimateShine();
                m_level.text = m_model.Level.ToString();
                m_progress.fillAmount = 0;
                yield return AnimateShine();
            }
            yield return AnimateToFill(m_model.Progress / 100f);
            yield return new WaitForSecondsRealtime(1f);
            m_firstRun = false;
        }

        public IEnumerator AnimateToFill(float target) {
            while (m_progress.fillAmount < target) {
                m_progress.fillAmount += FILL_SPEED * Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
            }
            m_progress.fillAmount = target;
        }

        public void Clear() {
            m_firstRun = true;
            m_progress.fillAmount = 0 ;
        }

        public IEnumerator AnimateShine() {
            float a = 0;
            while (a < Mathf.PI) {
                SetShineAlpha(Mathf.Sin(a));
                a += ( Mathf.PI ) * Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
            }
            SetShineAlpha(0f);
        }

        private void SetShineAlpha(float alpha) {
            Color c = m_shine.color;
            Debug.Log("alpha" + alpha);
            c.a = alpha;
            m_shine.color = c;
        }
    }

}