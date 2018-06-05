using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ClearPrefs : ScriptableObject {
    [MenuItem("Utils/EditorPrefs/Clear all")]
    static void DeleteEditorPrefs() {
        if (EditorUtility.DisplayDialog("Delete all editor preferences.",
                "Are you sure you want to delete all the editor preferences? " +
                "This action cannot be undone.", "Yes", "No")) {
            EditorPrefs.DeleteAll();

        }
    }

    [MenuItem("Utils/PlayerPrefs/Clear all")]
    static void DeletePlayerPrefs() {
        if (EditorUtility.DisplayDialog("Delete all player prefs preferences.",
                "Are you sure you want to delete all the editor preferences? " +
                "This action cannot be undone.", "Yes", "No")) {
            PlayerPrefs.DeleteAll();
        }
    }
}
