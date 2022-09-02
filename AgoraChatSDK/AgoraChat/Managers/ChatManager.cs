namespace AgoraChat
{
    public class ChatManager
    {
        internal ChatManager(NativeListener listener)
        {
            listener.chatManagerEvent += NativeEventHandle;
        }
        void NativeEventHandle(string method, string jsonString)
        {

        }
    }
}