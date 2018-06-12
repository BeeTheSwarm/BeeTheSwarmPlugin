using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public static class GameObjectInstatiator {
    public static T InstantiateFromResources<T>(string path) where T : Object {
        T result;
#if UNITY_EDITOR
        result = (T)PrefabUtility.InstantiatePrefab(Resources.Load<T>(path));
#else
        result = GameObject.Instantiate(Resources.Load<T>(path));
#endif
        return result;
    }

    public static T InstantiateFromObject<T>(T origin) where T : Object {
        T result;
#if UNITY_EDITOR
        var type = PrefabUtility.GetPrefabType(origin);
        if (type == PrefabType.Prefab) {
            result = PrefabUtility.InstantiatePrefab(origin) as T;
            return result;
        }
        else {
            result = GameObject.Instantiate<T>(origin);
        }
#else
        result = GameObject.Instantiate<T>(origin);
#endif
        return result;
    }
    
}
