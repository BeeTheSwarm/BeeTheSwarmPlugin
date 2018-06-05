using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class EditorUtils {

    [MenuItem("GameObject/EditorUtils/Find Texts", false, 0)]
    static void FindTexts() {
        GameObject selected = (GameObject)Selection.activeObject;
        if (selected != null) {
            var texts = selected.GetComponentsInChildren<Text>(true);
            Font font = null;
            for (var i = 0; i < texts.Length; i++) {
                Debug.Log(texts[i].font.name, texts[i].gameObject);
                if (texts[i].font.name.Equals("AvantgardeITCbyBT")) {
                    font = texts[i].font;
                    break;
                }
            }
            if (font == null) {
                return;
            }
            for (var i = 0; i < texts.Length; i++) {
                if (texts[i].font.name.Equals("AvantgardeITC")) {
                    texts[i].font = font;
                }
            }
        }
    }

    [MenuItem("GameObject/EditorUtils/CheckAnchors", false, 0)]
    static void CheckAnchors() {
        GameObject selected = (GameObject)Selection.activeObject;
        if (selected == null) {
            return;
        }
        RectTransform[] transforms = selected.GetComponentsInChildren<RectTransform>(true);
        for (var i = transforms.Length - 1; i >= 0; i--) {
            var t = transforms[i];
            Debug.Log(" " + t.sizeDelta + " " + t.anchoredPosition + " " + t.anchorMin + " " + t.anchorMax + " " + t.offsetMin + " " + t.offsetMax + " " + t.rect, t.gameObject);
            if (t.anchoredPosition.y != 0) {
                Debug.Log("RectTransform has absolut vertical aligment", t.gameObject);
            }
            if (t.sizeDelta.y != 0) {
                Debug.Log("RectTransform has absolut vertical sizedelta", t.gameObject);
            }
        }
    }

    [MenuItem("GameObject/EditorUtils/Find InputFields To Fix", false, 0)]
    static void FindInputFieldsToFix() {
        GameObject selected = (GameObject)Selection.activeObject;
        if (selected == null) {
            return;
        }
        InputField[] inputFields = selected.GetComponentsInChildren<BTS_InputField>(true);
        for (var i = inputFields.Length - 1; i >= 0; i--) {
            var t = inputFields[i];
                Debug.Log("Found inputfield with shouldHideMobileInput == false ", t.gameObject);
        }
    }

    [MenuItem("GameObject/EditorUtils/Seek redundant raycast target", false, 0)]
    static void CheckRaycastTarget() {
        GameObject selected = (GameObject)Selection.activeObject;
        if (selected == null) {
            return;
        }

        Graphic[] graphics = selected.GetComponentsInChildren<Graphic>(true);
        for (var j = graphics.Length - 1; j >= 0; j--) {
            if (graphics[j].raycastTarget) {
                if (graphics[j].gameObject.GetComponent<Button>() == null) {
                    Debug.Log("Redundant raycastTarget", graphics[j].gameObject);
                }
            }
        }
    }
}
