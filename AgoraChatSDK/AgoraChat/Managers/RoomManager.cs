namespace AgoraChat
{
    public class RoomManager
    {
        internal RoomManager(NativeListener listener)
        {
            listener.roomManagerEvent += NativeEventHandle;
        }

        void NativeEventHandle(string method, string jsonString)
        {

        }
    }
}