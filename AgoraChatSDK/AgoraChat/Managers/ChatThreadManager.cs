namespace AgoraChat
{
    public class ChatThreadManager
    {
        internal ChatThreadManager(NativeListener listener)
        {
            listener.chatThreadManagerEvent += NativeEventHandle;
        }

        void NativeEventHandle(string method, string jsonString)
        {

        }
    }
}