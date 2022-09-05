using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    internal delegate void NativeListenerEvent(string listener, string method, string jsonString);

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

        public NativeListener() {
            callbackManager = new CallbackManager();
            nativeListenerEvent = (string listener, string method, string jsonString) =>
            {
                if (listener == "chatManagerListener")
                {
                    chatManagerEvent(method, jsonString);
                }
                else if (listener == "contactManagerListener")
                {
                    contactManagerEvent(method, jsonString);
                }
                else if (listener == "groupManagerListener")
                {
                    groupManagerEvent(method, jsonString);
                }
                else if (listener == "roomManagerListener")
                {
                    roomManagerEvent(method, jsonString);
                }
                else if (listener == "presenceManagerListener")
                {
                    presenceManagerEvent(method, jsonString);
                }
                else if (listener == "chatThreadManagerListener")
                {
                    chatThreadManagerEvent(method, jsonString);
                }
                else if (listener == "connectionListener")
                {
                    connectionEvent(method, jsonString);
                }
                else if (listener == "multiDeviceListener")
                {
                    multiDeviceEvent(method, jsonString);
                }
                else if (listener == "callback")
                {
                    callbackManager.CallAction(method, jsonString);
                }
            };
        }

        public void AddNaitveListener() {
            CWrapperNative.AddListener(nativeListenerEvent);
        }

        public void RemoveNativeListener() {
            CWrapperNative.CleanListener();
        }
    }
}
