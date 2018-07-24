using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BTS {
    public static class BTSPlugin {
        private static BTSPluginContext s_context;
        private static Action m_initCallback;
        public static event Action OnUserLoggedIn
        {
            add
            {
                if (s_context != null) {
                    s_context.OnUserLoggedIn += value;
                }
            }
            remove
            {
                if (s_context != null) {
                    s_context.OnUserLoggedIn -= value;
                }
            }
        }
        
        public static event Action OnUserLoggedOut
        {
            add
            {
                if (s_context != null) {
                    s_context.OnUserLoggedOut += value;
                }
            }
            remove
            {
                if (s_context != null) {
                    s_context.OnUserLoggedOut -= value;
                }
            }
        }

        public static event Action OnViewShown {
            add {
                if (s_context != null) {
                    s_context.OnShown += value;
                }
            }
            remove {
                if (s_context != null) {
                    s_context.OnShown -= value;
                }
            }
        }
        public static event Action OnHideStarted {
            add {
                if (s_context != null) {
                    s_context.OnHidingStarted += value;
                }
            }
            remove {
                if (s_context != null) {
                    s_context.OnHidingStarted -= value;
                }
            }
        }
        public static event Action OnHideFinished {
            add {
                if (s_context != null) {
                    s_context.OnHidingFinished += value;
                }
            }
            remove {
                if (s_context != null) {
                    s_context.OnHidingFinished -= value;
                }
            }
        }
        public static void ShowUserBeesCount() {
            if (!IsInited) {
                throw new Exception("BTS Plugin is not inited");
            }
            s_context.ShowBeesPopup();
        }

        public static void Init(string gameId, Action callback, bool standalone = false) {
            if (!IsInited) {
                Platform.Init();
                BTSPluginContext contextOrigin = Resources.Load<BTSPluginContext>("BTS_Social");
                s_context = GameObject.Instantiate(contextOrigin);                
                s_context.StartPlugin(gameId, callback, standalone);
            }
            else {
                callback();
            }
        }
        


        public static void AddChest(Action<int> callback) {
            if (!IsInited) {
                throw new Exception("BTS Plugin is not inited");
            }
            s_context.AddChest(callback);
        }
        
        public static void GetChest(Action<int> callback) {
            if (!IsInited) {
                throw new Exception("BTS Plugin is not inited");
            }
            s_context.GetChest(callback);
        }
        
        public static void OpenChest(Action<List<ChestReward>,int> callback) {
            if (!IsInited) {
                throw new Exception("BTS Plugin is not inited");
            }
            s_context.OpenChest(callback);
        }

        public static void AddBees(int count) {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }

            s_context.AddBees(count);
        }

        public static bool IsUserLoggedIn() {
            if (!IsInited) {
                throw new Exception("BTS Plugin is not inited");
            }
            return s_context.InUserLoggedIn();
        }

        
        public static void RegisterPushId(string id) {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }
            s_context.RegisterPushId(id);
        }
        
        public static void ProcessNotification() {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }
            s_context.ProcessNotification();
        }

        public static void Event(string levelId, int score, Action<int> callback) {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }

            s_context.Event(levelId, score, callback);
        }

        public static void GetEvents(Action<List<EventModel>> callback) {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }

            s_context.GetEvents(callback);
        }

        public static bool IsInited
        {
            get { return s_context != null; }
        }

        public static void Show() {
            if (!IsInited) {
                Debug.Log("BTS Plugin not inited");
                return;
            }

            s_context.Show();
        }
    }
}