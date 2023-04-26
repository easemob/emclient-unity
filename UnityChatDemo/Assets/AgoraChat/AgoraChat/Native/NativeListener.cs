using AgoraChat.SimpleJSON;
using System;
using System.Runtime.InteropServices;
#if !_WIN32
using UnityEngine;
#endif

namespace AgoraChat
{
#if _WIN32
    internal delegate void NativeListenerEvent(string listener, string method, [MarshalAs(UnmanagedType.LPTStr)] string jsonString);
#else
    internal delegate void NativeListenerEvent(string listener, string method, string jsonString);
#endif

    internal delegate void ManagerHandle(string method, JSONNode jsonNode);


    internal class NativeListener
    {

        internal NativeListenerEvent nativeListenerEvent;

        internal event ManagerHandle ChatManagerEvent;

        internal event ManagerHandle ContactManagerEvent;

        internal event ManagerHandle GroupManagerEvent;

        internal event ManagerHandle RoomManagerEvent;

        internal event ManagerHandle ChatThreadManagerEvent;

        internal event ManagerHandle PresenceManagerEvent;

        internal event ManagerHandle ConnectionEvent;

        internal event ManagerHandle MultiDeviceEvent;

        internal UnityHelper unityHelper;

        internal CallbackManager callbackManager;

        internal CallbackQueue_Worker queue_worker;

        internal int COMPILE_TYPE_MONO = 0;
        internal int COMPILE_TYPE_IL2CPP = 1;

        internal NativeListener()
        {
            unityHelper = UnityHelper.Instance();

            callbackManager = new CallbackManager();

            queue_worker = CallbackQueue_Worker.Instance();

            nativeListenerEvent = (string listener, string method, string jsonString) =>
            {
                if (string.IsNullOrEmpty(method) || string.IsNullOrEmpty(listener))
                    return;

                JSONNode jsonNode = null;
                try
                {
                    jsonNode = JSON.Parse(jsonString);
                }
                catch(Exception e)
                {
                    // Suggest to print log into file
                    return;
                }

                queue_worker.EnQueue(() =>
                {

                    LogPrinter.Log($"nativeListenerEvent listener: {listener}  method: {method}  jsonString: {jsonString}");

                    switch (listener)
                    {
                        case SDKMethod.chatListener:
                            ChatManagerEvent(method, jsonNode);
                            break;
                        case SDKMethod.contactListener:
                            ContactManagerEvent(method, jsonNode);
                            break;
                        case SDKMethod.groupListener:
                            GroupManagerEvent(method, jsonNode);
                            break;
                        case SDKMethod.chatRoomListener:
                            RoomManagerEvent(method, jsonNode);
                            break;
                        case SDKMethod.connectionListener:
                            ConnectionEvent(method, jsonNode);
                            break;
                        case SDKMethod.multiDeviceListener:
                            MultiDeviceEvent(method, jsonNode);
                            break;
                        case SDKMethod.presenceListener:
                            PresenceManagerEvent(method, jsonNode);
                            break;
                        case SDKMethod.chatThreadListener:
                            ChatThreadManagerEvent(method, jsonNode);
                            break;
                        case SDKMethod.callback:
                            callbackManager.CallAction(method, jsonNode);
                            break;
                        case SDKMethod.callbackProgress:
                            callbackManager.CallActionProgress(method, jsonNode);
                            break;
                        default:
                            LogPrinter.Log("no listener handle");
                            break;
                    }
                });
            };
        }

#if !_WIN32
        [AOT.MonoPInvokeCallback(typeof(NativeListenerEvent))]
        public static void OnRunCallback(string listener, string method, string jsonString)
        {
            LogPrinter.Log($"OnRunCallback listener: {listener},  method: {method}, jsonString: {jsonString}");
            SDKClient.Instance._clientImpl.nativeListener.nativeListenerEvent?.Invoke(listener, method, jsonString);
        }
#endif
        ~NativeListener()
        {
            queue_worker.ClearQueue();
            CWrapperNative.UnInit();
            nativeListenerEvent = null;
        }

        internal void AddNaitveListener()
        {
            LogPrinter.Log($"AddNaitveListener run --- {nativeListenerEvent}");

#if _WIN32
            CWrapperNative.Init(0, COMPILE_TYPE_MONO, nativeListenerEvent);
#else
#if ENABLE_IL2CPP
            CWrapperNative.Init(0, COMPILE_TYPE_IL2CPP, OnRunCallback);
#else
            CWrapperNative.Init(0, COMPILE_TYPE_MONO, OnRunCallback);
#endif
#endif
            LogPrinter.Log($"AddNaitveListener end --- {nativeListenerEvent}");
        }

        internal void RemoveNativeListener()
        {
            CWrapperNative.UnInit();
        }
    }
}
