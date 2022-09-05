using System;
using System.Collections.Generic;

namespace AgoraChat
{
    internal class CallbackManager
    {

        Dictionary<string, CallbackItem> callbackMap = new Dictionary<string, CallbackItem>();
        internal CallbackManager()
        {
          
        }

        public void AddCallback(CallBack callback, Action<string, CallBack> action)
        {
            callbackMap[callback.callbackId] = new CallbackItem(callback, action);
        }

        public void CallAction(string callbackId, string jsonString) { 
            CallbackItem item = callbackMap[callbackId];
            if(item == null)
            {
                item.callbackAction(jsonString, item.callback);
            }
        }
    }

    internal class CallbackItem {
        public CallBack callback;
        public Action<string, CallBack> callbackAction;

        public CallbackItem(CallBack callback, Action<string, CallBack> callbackAction)
        {
            this.callback = callback;
            this.callbackAction = callbackAction;
        }
    }
}