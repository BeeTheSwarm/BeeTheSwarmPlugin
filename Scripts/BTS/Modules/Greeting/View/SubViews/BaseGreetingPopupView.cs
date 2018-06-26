using UnityEngine;
using System.Collections;
namespace BTS {
    public abstract class BaseGreetingPopupView : MonoBehaviour {
        public virtual IEnumerator Animate() {
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}