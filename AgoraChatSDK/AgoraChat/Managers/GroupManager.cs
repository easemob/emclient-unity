namespace AgoraChat
{
    public class GroupManager
    {
        internal GroupManager(NativeListener listener)
        {
            listener.groupManagerEvent += NativeEventHandle;
        }

        void NativeEventHandle(string method, string jsonString)
        {

        }
    }
}