using UnityEngine;

namespace BTS.BeeAnimation.View {
    public class BeeAnimation: MonoBehaviour {
        [SerializeField] private Animator m_animator;
        public void OnAnimationFinish() {
            Destroy(gameObject);
        }
        
        private const string ANIMATOR_SHOW_SWARMBEES = "ShowSwarmBees";
        private const string ANIMATOR_HIDE_SWARMBEES = "HideSwarmBees";
        
        public void Play() {
            m_animator.SetTrigger(ANIMATOR_SHOW_SWARMBEES);
        }
    }
}