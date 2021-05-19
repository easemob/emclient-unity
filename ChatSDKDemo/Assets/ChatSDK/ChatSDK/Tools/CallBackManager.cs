using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {

    internal class CallbackManager : MonoBehaviour
    {

        static string Callback_Obj = "unity_chat_callback_obj";

        static GameObject callbackGameObject;

        internal int currentId = 1;

        internal Dictionary<string, object> dictionary = new Dictionary<string, object>();

        private static CallbackManager _getInstance;

        internal static CallbackManager Instance()
        {
            if (_getInstance == null)
            {
                callbackGameObject = new GameObject(Callback_Obj);
                _getInstance = callbackGameObject.AddComponent<CallbackManager>();
            }

            return _getInstance;
        }


        internal void AddCallback(int callbackId, CallBack callback) {
            dictionary.Add(callbackId.ToString(), callback);
            currentId++;
        }

        internal void AddValueCallback<T>(int callbackId, ValueCallBack<T> valueCallBack)
        {
            dictionary.Add(callbackId.ToString(), valueCallBack);
            currentId++;
        }


        internal void RemoveCallback(int callbackId)
        {
            if (dictionary.ContainsKey(callbackId.ToString())) {
                dictionary.Remove(callbackId.ToString());
            }
        }


        public void OnSuccess(string jsonString) {
            JSONNode jo = JSON.Parse(jsonString);
            string callbackId = jo["callbackId"].Value;
            if (dictionary.ContainsKey(callbackId)) {
                CallBack callBack = (CallBack)dictionary[callbackId];
                callBack.Success();
                dictionary.Remove(callbackId);
            }
        }

        public void OnSuccessValue(string jsonString) {
            JSONNode jo = JSON.Parse(jsonString);
            string callbackId = jo["callbackId"].Value;
            if (dictionary.ContainsKey(callbackId))
            {
                string value = jo["type"].Value;
                if (value == "List<String>")
                {    
                    string responseValue = jo["value"].Value;
                    List<string> list = TransfromTool.JsonStringToListString(responseValue);
                    ValueCallBack<List<string>> valueCallBack = (ValueCallBack<List<string>>)dictionary[callbackId];                    
                    valueCallBack.OnSuccessValue(list);
                    dictionary.Remove(callbackId);
                }
            }
        }


        public void OnProgress(string jsonString) {
            JSONNode jo = JSON.Parse(jsonString);
            string callbackId = jo["callbackId"].Value;
            if (dictionary.ContainsKey(callbackId))
            {
                int progress = jo["progress"].AsInt;
                CallBack callBack = (CallBack)dictionary[callbackId];
                callBack.Progress(progress);
            }
        }

        public void OnError(string jsonString) {
            JSONNode jo = JSON.Parse(jsonString);
            string callbackId = jo["callbackId"].Value;
            if (dictionary.ContainsKey(callbackId)) {
                int code = jo["code"].AsInt;
                string desc = jo["desc"].Value;
                CallBack callBack = (CallBack)dictionary[callbackId];
                callBack.Error(code, desc);
                dictionary.Remove(callbackId);
            }
        }
    }
}