using AgoraChat.SimpleJSON;
using System;
using System.Runtime.InteropServices;

namespace AgoraChat
{
    internal delegate void NativeListenerEvent(string listener, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString);

    internal delegate void ChatManagerHandle(string method, string jsonString);

    internal delegate void ContactManagerHandle(string method, string jsonString);

    internal delegate void GroupManagerHandle(string method, string jsonString);

    internal delegate void RoomManagerHandle(string method, string jsonString);

    internal delegate void ChatThreadManagerHandle(string method, string jsonString);

    internal delegate void PresenceManagerHandle(string method, string jsonString);

    internal delegate void ConnectionHandle(string method, string jsonString);

    internal delegate void MultiDeviceHandle(string method, string jsonString);

    internal delegate void CallbackManagerHandle(string callId, string jsonString);

    internal class NativeListener
    {
  
        public NativeListenerEvent nativeListenerEvent;

        public event ChatManagerHandle chatManagerEvent;

        public event ContactManagerHandle contactManagerEvent;

        public event GroupManagerHandle groupManagerEvent;

        public event RoomManagerHandle roomManagerEvent;

        public event ChatThreadManagerHandle chatThreadManagerEvent;

        public event PresenceManagerHandle presenceManagerEvent;

        public event ConnectionHandle connectionEvent;

        public event MultiDeviceHandle multiDeviceEvent;

        public event CallbackManagerHandle callbackEvent;

        public CallbackManager callbackManager;

        internal CallbackQueue_Worker queue_worker;

        public NativeListener() {

            callbackManager = new CallbackManager();

            queue_worker = CallbackQueue_Worker.Instance();
            queue_worker.StartRun();

            nativeListenerEvent = (string listener, string method, string jsonString) =>
            {
                queue_worker.EnQueue(() => {
                    string json = Tools.GetUnicodeStringFromUTF8(jsonString);

                    if (listener == "chatManagerListener")
                    {
                        chatManagerEvent(method, json);
                    }
                    else if (listener == "contactManagerListener")
                    {
                        contactManagerEvent(method, json);
                    }
                    else if (listener == "groupManagerListener")
                    {
                        groupManagerEvent(method, json);
                    }
                    else if (listener == "roomManagerListener")
                    {
                        roomManagerEvent(method, json);
                    }
                    else if (listener == "presenceManagerListener")
                    {
                        presenceManagerEvent(method, json);
                    }
                    else if (listener == "chatThreadManagerListener")
                    {
                        chatThreadManagerEvent(method, json);
                    }
                    else if (listener == "connectionListener")
                    {
                        connectionEvent(method, json);
                    }
                    else if (listener == "multiDeviceListener")
                    {
                        multiDeviceEvent(method, json);
                    }
                    else if (listener == "callback")
                    {
                        callbackManager.CallAction(method, json);
                    }
                    else if (listener == "callbackProgress")
                    {
                        callbackManager.CallActionProgress(method, json);
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
