using UnityEngine;
using System.Collections;
namespace BTS {

    public class ContactUsButtonHandler : MonoBehaviour {
        public void ClickHandler() {
            Platform.Adapter.SendEmail(string.Empty, string.Empty, new System.Collections.Generic.List<string> { "info@beetheswarm.com" }, null);
        }
    }

}