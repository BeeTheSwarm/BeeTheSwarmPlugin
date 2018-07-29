using UnityEngine;
using System.Collections;
using BTS;
using UnityEngine.UI;

[RequireComponent(typeof(InputField), typeof(LayoutElement))]
public class ResizableInputField : MonoBehaviour {
    [SerializeField]
    private float m_padding = 10f;
    private InputField m_inputField;
    private LayoutElement m_layout;
    private TextGenerator m_textGenerator;

    private void Awake() {
    }

    protected void Start() {
        m_inputField = GetComponent<InputField>();
        m_layout = GetComponent<LayoutElement>();
        m_inputField.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<string>(ResizeInput));
    }

    private void ResizeInput(string text) {
        if (m_textGenerator == null) {
            m_textGenerator = new TextGenerator();
        }
        var fullText = m_inputField.text;

        Vector2 extents = m_inputField.textComponent.rectTransform.rect.size;
        var settings = m_inputField.textComponent.GetGenerationSettings(extents);
        settings.generateOutOfBounds = false;
        var prefheight = m_textGenerator.GetPreferredHeight(fullText, settings) + m_padding;
        if (prefheight > m_layout.preferredHeight) {
            m_layout.preferredHeight = prefheight;
        }
        else if (prefheight < m_layout.preferredHeight) {
            m_layout.preferredHeight = prefheight;
        }

        InputFieldsHandler.Update(GetComponent<RectTransform>());
    }
}
