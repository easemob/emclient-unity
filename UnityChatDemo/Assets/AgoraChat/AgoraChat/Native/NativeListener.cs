using AgoraChat.SimpleJSON;


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

        internal CallbackManager callbackManager;

        internal CallbackQueue_Worker queue_worker;

        internal NativeListener()
        {

            callbackManager = new CallbackManager();

            queue_worker = CallbackQueue_Worker.Instance();
            queue_worker.StartRun();

            nativeListenerEvent = (string listener, string method, string jsonString) =>
            {
                if (string.IsNullOrEmpty(method) || string.IsNullOrEmpty(listener))
                    return;

                string json = Tools.GetUnicodeStringFromUTF8(jsonString);
                JSONNode jsonNode = JSON.Parse(json);

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
                        case SDKMethod.roomManager:
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

#if UNITY_IPHONE
        [AOT.MonoPInvokeCallback(typeof(NativeListenerEvent))]
        public static void OnRunCallback(string listener, string method, string jsonString)
        {
            LogPrinter.Log($"OnRunCallback listener: {listener},  method: {method}, jsonString: {jsonString}");
            SDKClient.Instance._clientImpl.nativeListener.nativeListenerEvent?.Invoke(listener, method, jsonString);
        }
#endif
        ~NativeListener()
        {
            queue_worker.Stop();
            CWrapperNative.UnInit();
            nativeListenerEvent = null;
        }

        internal void AddNaitveListener()
        {
            LogPrinter.Log($"AddNaitveListener run --- {nativeListenerEvent}");

#if UNITY_IPHONE
            CWrapperNative.Init(0, OnRunCallback);
#else
            CWrapperNative.Init(0, nativeListenerEvent);
#endif
        }

        internal void RemoveNativeListener()
        {
            CWrapperNative.UnInit();
        }
    }
}
