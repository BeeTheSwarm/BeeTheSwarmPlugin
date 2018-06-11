using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderView : BaseControlledView<IViewEventListener>, ILoaderView {
    [SerializeField] private Text m_message;

    public void SetText(string text) {
        m_message.text = text;
    }
}
