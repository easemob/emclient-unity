using System;

namespace AgoraChat
{
    public class ChatManager
    {
        CallbackManager callbackManager;

        internal string NAME_CHATMANAGER = "ChatManager";

        internal ChatManager(NativeListener listener)
        {
            callbackManager = listener.callbackManager;
            listener.chatManagerEvent += NativeEventHandle;
        }
        internal void NativeEventHandle(string method, string jsonString)
        {
        }

        public void SendMessage(ref Message message, CallBack callback = null)
        {
            string callback_id = (null != callback) ? callback.callbackId : message.MsgId;


        }

    }
}