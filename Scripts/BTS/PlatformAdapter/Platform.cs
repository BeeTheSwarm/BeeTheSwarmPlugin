using UnityEngine;
using System.Collections;

public static class Platform
{
    public static IPlatformAdapter Adapter;
    public static void Init()
    {
        #if UNITY_EDITOR
        Adapter = new EditorPlatformAdapter();
        #elif UNITY_ANDROID
        Adapter = new AndroidPlatformAdapter();
        #elif UNITY_IOS
        Adapter = new IosPlatformAdapter();
        #endif
    }
}
