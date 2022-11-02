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

        public event ManagerHandle ChatManagerEvent;

        public event ManagerHandle ContactManagerEvent;

        public event ManagerHandle GroupManagerEvent;

        public event ManagerHandle RoomManagerEvent;

        public event ManagerHandle ChatThreadManagerEvent;

        public event ManagerHandle PresenceManagerEvent;

        public event ManagerHandle ConnectionEvent;

        public event ManagerHandle MultiDeviceEvent;

        //public event ManagerHandle CallbackEvent;

        public CallbackManager callbackManager;

        internal CallbackQueue_Worker queue_worker;

        public NativeListener() {

            callbackManager = new CallbackManager();

            queue_worker = CallbackQueue_Worker.Instance();
            queue_worker.StartRun();

            nativeListenerEvent = (string listener, string method, string jsonString) =>
            {
                string json = Tools.GetUnicodeStringFromUTF8(jsonString);
                JSONNode jsonNode = JSON.Parse(json);

                queue_worker.EnQueue(() => {
                    
                    if (listener == "chatManagerListener")
                    {
                        ChatManagerEvent(method, jsonNode);
                    }
                    else if (listener == "contactManagerListener")
                    {
                        ContactManagerEvent(method, jsonNode);
                    }
                    else if (listener == "groupManagerListener")
                    {
                        GroupManagerEvent(method, jsonNode);
                    }
                    else if (listener == "roomManagerListener")
                    {
                        RoomManagerEvent(method, jsonNode);
                    }
                    else if (listener == "presenceManagerListener")
                    {
                        PresenceManagerEvent(method, jsonNode);
                    }
                    else if (listener == "chatThreadManagerListener")
                    {
                        ChatThreadManagerEvent(method, jsonNode);
                    }
                    else if (listener == "connectionListener")
                    {
                        ConnectionEvent(method, jsonNode);
                    }
                    else if (listener == "multiDeviceListener")
                    {
                        MultiDeviceEvent(method, jsonNode);
                    }
                    else if (listener == "callback")
                    {
                        callbackManager.CallAction(method, jsonNode);
                    }
                    else if (listener == "callbackProgress")
                    {
                        callbackManager.CallActionProgress(method, jsonNode);
                    }
                });
            };
        }

        ~NativeListener()
        {
            queue_worker.Stop();
        }

        public void AddNaitveListener() {
            CWrapperNative.AddListener(nativeListenerEvent);
        }

        public void RemoveNativeListener() {
            CWrapperNative.CleanListener();
        }
    }
}
