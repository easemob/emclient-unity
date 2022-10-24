using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    internal delegate Object Process(string cbid, string json);

    internal class CallbackManager
    {
        internal long current_id = 0;

        Dictionary<string, CallbackItem> callbackMap = new Dictionary<string, CallbackItem>();
        internal CallbackManager()
        {
          
        }

        public void AddCallback(CallBack callback, Action<string, Process, CallBack> action, Process process)
        {
            callback.callbackId = current_id.ToString();
            callbackMap[callback.callbackId] = new CallbackItem(callback, action, process);
            current_id++;
        }

        public void AddCallbackAction<T>(CallBack callback, Process _process)
        {
            if (null == callback) return;

            AddCallback(
                callback, 
                (jstr, process, cb) => {
                    // parse result json
                    if (null == jstr || jstr.Length <= 2) return;

                    JSONNode jn = JSON.Parse(jstr);
                    if (null == jn || !jn.IsObject) return;

                    JSONObject jo = jn.AsObject;

                    string cbid = jo["callbackId"].Value;
                    int progress = (null != jo["progress"]) ? jo["progress"].AsInt : -1;
                    string value = (null != jo["value"]) ? jo["value"].Value : "";

                    int code = -1;
                    string desc = "";
                    if(null != jo["error"] && jo["error"].IsObject)
                    {
                        JSONObject jo_err = jo["error"].AsObject;
                        if (null != jo_err["code"]) code = jo_err["code"].AsInt;
                        if (null != jo_err["desc"]) desc = jo_err["desc"].Value;
                    }

                    // parse json value to object and process object
                    Object obj = null;
                    if (value.Length > 0 && null != process)
                    {
                        obj = process(cbid, value);
                    }

                    // call callback
                    if (null == cb) return;
                    if (-1 != code)
                    {
                        cb.Error?.Invoke(code, desc);
                    } 
                    else if (progress >= 0)
                    {
                        cb.Progress?.Invoke(progress);
                    }
                    else if (null != obj)
                    {
                        ValueCallBack<T> value_callback = (ValueCallBack<T>)cb;
                        value_callback.OnSuccessValue?.Invoke((T)obj);
                    }
                    else
                    {
                        cb.Success?.Invoke();
                    }
                }, 
                _process);
        }

        public void CallAction(string callbackId, string jsonString) { 
            CallbackItem item = callbackMap[callbackId];
            if(item != null)
            {
                item.callbackAction(jsonString, item.process, item.callback);
                callbackMap.Remove(callbackId); // delete the callback after triggered
            }
        }

        public void CallActionProgress(string callbackId, string jsonString)
        {
            CallbackItem item = callbackMap[callbackId];
            if (item != null)
            {
                item.callbackAction(jsonString, item.process, item.callback);
            }
        }
    }

    internal class CallbackItem {
        public CallBack callback;
        public Action<string, Process, CallBack> callbackAction;
         public Process process;

        public CallbackItem(CallBack callback, Action<string, Process, CallBack> callbackAction, Process process)
        {
            this.callback = callback;
            this.callbackAction = callbackAction;
            this.process = process;
        }
    }
}