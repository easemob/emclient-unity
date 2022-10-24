using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    internal delegate Object Parse(string json);
    internal delegate void Process(string cbid, Object t);

    internal class CallbackManager
    {
        internal long current_id = 0;

        Dictionary<string, CallbackItem> callbackMap = new Dictionary<string, CallbackItem>();
        internal CallbackManager()
        {
          
        }

        public void AddCallback(CallBack callback, Action<string, Parse, Process, CallBack> action, Parse parse, Process process)
        {
            callback.callbackId = current_id.ToString();
            callbackMap[callback.callbackId] = new CallbackItem(callback, action, parse, process);
            current_id++;
        }

        public void AddCallbackAction<T>(CallBack callback, Parse _parse, Process _process)
        {
            if (null == callback) return;

            AddCallback(
                callback, 
                (jstr, parse, process, cb) => {
                    if (null == jstr || jstr.Length <= 2) return;

                    JSONNode jn = JSON.Parse(jstr);
                    if (null == jn || !jn.IsObject) return;

                    JSONObject jo = jn.AsObject;

                    string cbid     = jo["callbackId"].Value;
                    int progress    = (null != jo["progress"]) ? jo["progress"].AsInt : -1;
                    string value    = (null != jo["value"]) ? jo["value"].Value : "";

                    int code = -1;
                    string desc = "";
                    if(null != jo["error"] && jo["error"].IsObject)
                    {
                        JSONObject jo_err = jo["error"].AsObject;
                        if (null != jo_err["code"]) code = jo_err["code"].AsInt;
                        if (null != jo_err["desc"]) desc = jo_err["desc"].Value;
                    }

                    Object obj = null;
                    if (value.Length > 0)
                    {
                        if (null != parse) obj = parse(value);
                        if (null != process) process(cbid, obj);
                    }

                    if (-1 != code)
                    {
                        cb.Error(code, desc);
                    } 
                    else if (progress >= 0)
                    {
                        cb.Progress(progress);
                    }
                    else if (null != obj)
                    {
                        ValueCallBack<T> value_callback = (ValueCallBack<T>)cb;
                        value_callback.OnSuccessValue((T)obj);
                    }
                    else
                    {
                        cb.Success();
                    }
                }, 
                _parse, 
                _process);
        }

        public void CallAction(string callbackId, string jsonString) { 
            CallbackItem item = callbackMap[callbackId];
            if(item != null)
            {
                item.callbackAction(jsonString, item.parse, item.process, item.callback);
                callbackMap.Remove(callbackId); // delete the callback after triggered
            }
        }

        public void CallActionProgress(string callbackId, string jsonString)
        {
            CallbackItem item = callbackMap[callbackId];
            if (item != null)
            {
                item.callbackAction(jsonString, item.parse, item.process, item.callback);
        }
    }

    internal class CallbackItem {
        public CallBack callback;
        public Action<string, Parse, Process, CallBack> callbackAction;
        public Parse parse;
        public Process process;

        public CallbackItem(CallBack callback, Action<string, Parse, Process, CallBack> callbackAction, Parse parse, Process process)
        {
            this.callback = callback;
            this.callbackAction = callbackAction;
            this.parse = parse;
            this.process = process;
        }
    }
}