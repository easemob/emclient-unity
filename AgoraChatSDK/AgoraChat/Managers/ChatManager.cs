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

        //TO-DO: need to remove, just for testing
        static internal string GetUnicodeStringFromUTF8(string utf8Str)
        {
            if (utf8Str.Length == 0) return utf8Str;
            string ret = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Unicode.GetBytes(utf8Str));
            int index = ret.IndexOf('\0');
            if (index > 0)
                ret = ret.Substring(0, index);
            return ret;
        }

        
        public void Test_Call(CallBack callback = null)
        {
            callbackManager.AddCallback(callback, (jstr, cb) => {
                string ret = GetUnicodeStringFromUTF8(jstr);
                Console.WriteLine($"Call cb: {cb.callbackId}; jstr:{ret}");
                cb.Success();
            });

            CWrapperNative.NativeCall("ChatManager", "ChatManager_Call", "hehe", callback.callbackId);
        }


        public void Test_Get()
        {
            string ret = CWrapperNative.NativeGet("ChatManager", "ChatManager_Get", "hehe");
            string ret1 = GetUnicodeStringFromUTF8(ret);
            Console.WriteLine($"Get: {ret1}");
        }
    }
}