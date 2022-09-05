namespace AgoraChat
{
    public class PresenceManager
    {
        internal PresenceManager(NativeListener listener)
        {
            listener.presenceManagerEvent += NativeEventHandle;
        }

        void NativeEventHandle(string method, string jsonString)
        {

        }
    }
}