using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public class PresenceManager
    {
        internal PresenceManager(NativeListener listener)
        {
            listener.PresenceManagerEvent += NativeEventHandle;
        }

        internal void NativeEventHandle(string method, JSONNode jsonNode)
        {

        }
    }
}