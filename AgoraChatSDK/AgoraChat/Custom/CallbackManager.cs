using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    internal delegate Object Process(string cbid, JSONNode jsonNode);

    internal class CallbackManager
    {
        internal long current_id = 0;

        Dictionary<string, CallbackItem> callbackMap = new Dictionary<string, CallbackItem>();
        internal CallbackManager()
        {

        }

        private void AddCallback(CallBack callback, Action<JSONNode, CallBack, Process> action, Process process)
        {
            callback.callbackId = current_id.ToString();
            callbackMap[callback.callbackId] = new CallbackItem(callback, action, process);
            current_id++;
        }

        internal void AddCallbackAction(CallBack callback, Process _process = null)
        {
            if (null == callback) return;
            AddCallback(
                callback,
                (jn, cb, process) =>
                {
                    // parse result json
                    if (null == jn || !jn.IsObject) return;

                    JSONObject jo = jn.AsObject;

                    string cbid = jo["callbackId"].Value;
                    int progress = (null != jo["progress"]) ? jo["progress"].AsInt : -1;
                    string value = (null != jo["value"]) ? jo["value"].Value : "";

                    int code = -1;
                    string desc = "";
                    if (null != jo["error"] && jo["error"].IsObject)
                    {
                        JSONObject jo_err = jo["error"].AsObject;
                        if (null != jo_err["code"]) code = jo_err["code"].AsInt;
                        if (null != jo_err["desc"]) desc = jo_err["desc"].Value;
                    }

                    // parse json value to object and process object
                    Object obj = null;
                    if (value.Length > 0 && null != process)
                    {
                        JSONObject jsonNode = JSON.Parse(value).AsObject;
                        obj = process(cbid, jsonNode["ret"]);
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
                    else
                    {
                        cb.Success?.Invoke();
                    }
                },
                _process);
        }

        internal void AddCallbackAction<T>(CallBack callback, Process _process = null)
        {
            if (null == callback) return;

            AddCallback(
                callback,
                (jn, cb, process) =>
                {
                    // parse result json
                    if (null == jn || !jn.IsObject) return;

                    JSONObject jo = jn.AsObject;

                    string cbid = jo["callbackId"].Value;
                    int progress = (null != jo["progress"]) ? jo["progress"].AsInt : -1;
                    // here is json string
                    string value = (null != jo["value"]) ? jo["value"].Value : "";

                    int code = -1;
                    string desc = "";
                    if (null != jo["error"] && jo["error"].IsObject)
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

        internal void CallAction(string callbackId, JSONNode jsonNode)
        {
            CallbackItem item = callbackMap[callbackId];
            if (item != null)
            {
                item.callbackAction?.Invoke(jsonNode, item.callback, item.process);
                callbackMap.Remove(callbackId); // delete the callback after triggered
            }
        }

        internal void CallActionProgress(string callbackId, JSONNode jsonNode)
        {
            CallbackItem item = callbackMap[callbackId];
            if (item != null)
            {
                item.callbackAction?.Invoke(jsonNode, item.callback, item.process);
            }
        }

        internal void CleanAllItem()
        {
            callbackMap.Clear();
        }
    }

    internal class CallbackItem
    {
        internal CallBack callback;
        internal Action<JSONNode, CallBack, Process> callbackAction;
        internal Process process;

        internal CallbackItem(CallBack callback, Action<JSONNode, CallBack, Process> callbackAction, Process process)
        {
            this.callback = callback;
            this.callbackAction = callbackAction;
            this.process = process;
        }
    }
}
