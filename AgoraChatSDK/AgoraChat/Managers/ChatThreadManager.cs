using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public class ChatThreadManager
    {
        internal ChatThreadManager(NativeListener listener)
        {
            listener.ChatThreadManagerEvent += NativeEventHandle;
        }

        internal void NativeEventHandle(string method, JSONNode jsonNode)
        {

        }
    }
}