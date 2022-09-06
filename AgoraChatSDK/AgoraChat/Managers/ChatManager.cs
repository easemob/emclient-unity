using System;

namespace AgoraChat
{
    public class ChatManager
    {
        CallbackManager callbackManager;

        internal ChatManager(NativeListener listener)
        {
            callbackManager = listener.callbackManager;
            listener.chatManagerEvent += NativeEventHandle;
        }
        void NativeEventHandle(string method, string jsonString)
        {
            //TO-DO: need to remove, just for testing
            Console.WriteLine($"ChatManager: method:{method}; jsonString:{jsonString}");
        }

        //TO-DO: need to remove, just for testing
        public void AddCallBack(CallBack callback = null)
        {
            callbackManager.AddCallback(callback, (jstr, cb) => { 
                if(jstr.CompareTo("success") == 0)
                {
                    cb.Success();
                }
                if(jstr.CompareTo("progress") == 0)
                {
                    cb.Progress(123);
                }
                if(jstr.CompareTo("error") == 0)
                {
                    cb.Error(123, "err description");
                }
            });
        }
    }
}