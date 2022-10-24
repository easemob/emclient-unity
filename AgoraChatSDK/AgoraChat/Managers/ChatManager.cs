using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    public class ChatManager
    {
        CallbackManager callbackManager;

        internal string NAME_CHATMANAGER = "ChatManager";

        System.Object msgMapLocker;
        Dictionary<string, Message> msgMap;

        internal ChatManager(NativeListener listener)
        {
            callbackManager = listener.callbackManager;
            listener.chatManagerEvent += NativeEventHandle;
            msgMapLocker = new System.Object();
            msgMap = new Dictionary<string, Message>();
        }

        internal void NativeEventHandle(string method, string jsonString)
        {
        }

        private void AddMsgMap(string cbid, Message msg)
        {
            lock (msgMapLocker)
            {
                msgMap.Add(cbid, msg);
            }
        }

        private void UpdatedMsg(string cbid, string json)
        {
            lock (msgMapLocker)
            {
                if (msgMap.ContainsKey(cbid))
                {
                    var msg = msgMap[cbid];
                    if (json.Length > 0)
                    {
                        JSONNode jn = JSON.Parse(json);
                        if (null != jn && jn.IsObject)
                        {
                            msg.FromJsonObject(jn.AsObject);
                        }
                    }
                }
            }
        }

        private void DeleteFromMsgMap(string cbid)
        {
            lock (msgMapLocker)
            {
                if (msgMap.ContainsKey(cbid))
                {
                    msgMap.Remove(cbid);
                }
            }
        }

        public void SendMessage(ref Message message, CallBack callback = null)
        {
            Process process = (_cbid, _json) =>
            {
                UpdatedMsg(_cbid, _json);
                DeleteFromMsgMap(_cbid);
                return null;
            };

            callbackManager.AddCallbackAction<Object>(callback, process);

            string cbid = (null != callback) ? callback.callbackId : message.MsgId;

            AddMsgMap(cbid, message);

            JSONObject jo_param = message.ToJsonObject();

            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "sendMessage", jo_param, (null != callback) ? callback.callbackId : "");

            if(json.Length > 0)
            {
                UpdatedMsg(cbid, json);
            }
        }
    }
}