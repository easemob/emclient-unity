using AgoraChat.SimpleJSON;
using System;
using System.Runtime.InteropServices;

namespace AgoraChat
{
    internal delegate void NativeListenerEvent(string listener, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString);

    internal delegate void ManagerHandle(string method, JSONNode jsonNode);

    internal class NativeListener
    {

        public NativeListenerEvent nativeListenerEvent;

        public event ManagerHandle ManagerEvent;

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
                if (string.IsNullOrEmpty(method) || string.IsNullOrEmpty(listener)) return;

                string json = Tools.GetUnicodeStringFromUTF8(jsonString);
                JSONNode jsonNode = JSON.Parse(json);

                queue_worker.EnQueue(() =>
                {
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
                        case SDKMethod.connetionListener:
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
            CWrapperNative.AddListener(nativeListenerEvent);
        }

        public void RemoveNativeListener()
        {
            CWrapperNative.CleanListener();
        }
    }
}
