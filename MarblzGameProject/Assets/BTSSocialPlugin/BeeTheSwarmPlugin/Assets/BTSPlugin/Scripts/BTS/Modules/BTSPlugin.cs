using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace BTS {


    public static class BTSPlugin {
        private static BTSPluginContext s_context;
        private static Action m_initCallback;
        public static void Init(string gameId, Action callback) {
            if (!IsInited) {
                Platform.Init();
#if UNITY_EDITOR
                s_context = (BTSPluginContext)PrefabUtility.InstantiatePrefab(Resources.Load<BTSPluginContext>("BTS_Social"));
#else
                BTSPluginContext contextOrigin = Resources.Load<BTSPluginContext>("BTS_Social");
                s_context = GameObject.Instantiate(contextOrigin);
#endif
                s_context.SetGameId(gameId);
                s_context.SetNotificationsId("2f69d1d1-1004-4e47-a045-1ff3a89fb4c5");
                s_context.StartPlugin(callback);
            }
            else {
                callback();
            }
        }

        internal static void AddBees(int count) {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }
            s_context.AddBees(count);
        }

        internal static void Event(string levelId, int score) {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }
            s_context.Event(levelId, score);
        }

        internal static void GetEvents(Action<List<EventModel>> callback) {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }
            s_context.GetEvents(callback);
        }

        public static bool IsInited {
            get {
                return s_context != null;
            }
        }

#if UNITY_EDITOR
        public static BTSPluginContext GetPluginContext() {
            return s_context;
        }
#endif
        public static void Show() {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }
            s_context.Show();
        }
    }
}