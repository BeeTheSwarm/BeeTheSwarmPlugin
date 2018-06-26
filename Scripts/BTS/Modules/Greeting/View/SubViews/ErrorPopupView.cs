using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace BTS {
    public class ErrorPopupView : BaseGreetingPopupView {
        [SerializeField]
        private Text m_text;

        public void Setup(ErrorPopupItemModel model) {
            m_text.text = model.Message;
        }
    }
}