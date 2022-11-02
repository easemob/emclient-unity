using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public class RoomManager
    {
        internal RoomManager(NativeListener listener)
        {
            listener.RoomManagerEvent += NativeEventHandle;
        }

        internal void NativeEventHandle(string method, JSONNode jsonNode)
        {

        }
    }
}