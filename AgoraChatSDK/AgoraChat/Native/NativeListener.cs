using AgoraChat.SimpleJSON;
using System;
using System.Runtime.InteropServices;


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

        public NativeListenerEvent nativeListenerEvent;

        public event ManagerHandle ChatManagerEvent;

        public event ManagerHandle ContactManagerEvent;

        public event ManagerHandle GroupManagerEvent;

        public event ManagerHandle RoomManagerEvent;

        public event ManagerHandle ChatThreadManagerEvent;

        public event ManagerHandle PresenceManagerEvent;

        public event ManagerHandle ConnectionEvent;

        public event ManagerHandle MultiDeviceEvent;

        public CallbackManager callbackManager;

        internal CallbackQueue_Worker queue_worker;

        public NativeListener()
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

                    LogPrinter.Log($"listener: {listener}  method: {method}  jsonString: {jsonString}");

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

        ~NativeListener()
        {
            queue_worker.Stop();
            nativeListenerEvent = null;

        }

        public void AddNaitveListener()
        {
            CWrapperNative.Init(0, nativeListenerEvent);
        }

        public void RemoveNativeListener()
        {
            CWrapperNative.UnInit();
        }
    }
}
