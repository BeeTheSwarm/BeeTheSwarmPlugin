using UnityEditor;
using UnityEngine;

namespace BTS.Testing {
    public class TestEntryPoint {
#if UNITY_EDITOR
        [MenuItem("BTSPluginTests/Test Hive Update/Add new notification", false, 0)]
        static void TestHiveUpdateCase1() {
            var service = new FakeGetCounters(1);
            BTSPlugin.GetPluginContext().ReplaceService<IGetCountersService>(service);
            service.Execute();
        }

        [MenuItem("BTSPluginTests/Test Hive Update/Add new request", false, 0)]
        static void TestHiveUpdateCase2() {
            var service = new FakeGetCounters(2);
            BTSPlugin.GetPluginContext().ReplaceService<IGetCountersService>(service);
            service.Execute();
        }

        [MenuItem("BTSPluginTests/Test Hive Update/Add new notification and request", false, 0)]
        static void TestHiveUpdateCase3() {
            var service = new FakeGetCounters(3);
            BTSPlugin.GetPluginContext().ReplaceService<IGetCountersService>(service);
            service.Execute();
        }
        
        [MenuItem("BTSPluginTests/OneSignal/Emulate push notification", false, 0)]
        static void TestPushNotification() {
            
            BTSPlugin.GetPluginContext().EmulateOneSignal();
        }
#endif
    }
}