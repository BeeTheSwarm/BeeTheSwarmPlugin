using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class TiledInputField : MonoBehaviour {
    private void Awake() {
        var inputField = GetComponent<InputField>();
        var tileHeight = inputField.image.sprite.texture.height;
        var rectTransform = GetComponent<RectTransform>();
        var offset = rectTransform.rect.height - (Mathf.Floor(rectTransform.rect.height / tileHeight) * tileHeight);
        var offsetMin = rectTransform.offsetMin;
        offsetMin.y = offset + inputField.textComponent.fontSize * inputField.textComponent.lineSpacing / 2;
        rectTransform.offsetMin = offsetMin;
    }
}
