using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {

    internal class CallbackManager : MonoBehaviour
    {

        static string Callback_Obj = "unity_chat_callback_obj";

        static string Connection_Obj = "unity_chat_emclient_connection_obj";
        static string ChatManagerListener_Obj = "unity_chat_emclient_chatmanager_delegate_obj";
        static string ContactManagerListener_Obj = "unity_chat_emclient_contactmanager_delegate_obj";
        static string GroupManagerListener_Obj = "unity_chat_emclient_groupmanager_delegate_obj";
        static string RoomManagerListener_Obj = "unity_chat_emclient_roommanager_delegate_obj";


        internal ConnectionListener connectionListener;
        internal ChatManagerListener chatManagerListener;
        internal ContactManagerListener contactManagerListener;
        internal GroupManagerListener groupManagerListener;
        internal RoomManagerListener roomManagerListener;
        
        internal int CurrentId { get; private set; }

        internal Dictionary<string, object> dictionary = new Dictionary<string, object>();

        private static CallbackManager _getInstance;

        internal Dictionary<string, Message> tempMsgDict = new Dictionary<string, Message>();

        internal static CallbackManager Instance()
        {
            if (_getInstance == null)
            {
                GameObject callbackGameObject = new GameObject(Callback_Obj);
                DontDestroyOnLoad(callbackGameObject);
                _getInstance = callbackGameObject.AddComponent<CallbackManager>();
                _getInstance.SetupAllListeners();
            }

            return _getInstance;
        }


        internal void AddCallback(int callbackId, CallBack callback) {
            dictionary.Add(callbackId.ToString(), callback);
            CurrentId++;
        }

        internal void AddValueCallback<T>(int callbackId, ValueCallBack<T> valueCallBack)
        {
            dictionary.Add(callbackId.ToString(), valueCallBack);
            CurrentId++;
        }


        internal void RemoveCallback(int callbackId)
        {
            RemoveCallback(callbackId.ToString());
        }

        internal void RemoveCallback(string callbackId)
        {
            if (dictionary.ContainsKey(callbackId))
            {
                dictionary.Remove(callbackId);
            }
        }

        internal void CleanAllCallback() {
            dictionary.Clear();
            tempMsgDict.Clear();
        }

        internal void SetupAllListeners() {
            GameObject connectionObject = new GameObject(Connection_Obj);
            DontDestroyOnLoad(connectionObject);
            connectionListener = connectionObject.AddComponent<ConnectionListener>();
            connectionListener.delegater = new List<IConnectionDelegate>();

            GameObject chatManagerObject = new GameObject(ChatManagerListener_Obj);
            DontDestroyOnLoad(chatManagerObject);
            chatManagerListener = chatManagerObject.AddComponent<ChatManagerListener>();
            chatManagerListener.delegater = new List<IChatManagerDelegate>(); ;

            GameObject contactGameObj = new GameObject(ContactManagerListener_Obj);
            DontDestroyOnLoad(contactGameObj);
            contactManagerListener = contactGameObj.AddComponent<ContactManagerListener>();
            contactManagerListener.delegater = new List<IContactManagerDelegate>();

            GameObject groupGameObj = new GameObject(GroupManagerListener_Obj);
            DontDestroyOnLoad(groupGameObj);
            GroupManagerListener groupManagerListener = groupGameObj.AddComponent<GroupManagerListener>();
            groupManagerListener.delegater = new List<IGroupManagerDelegate>(); ;

            GameObject roomGameObj = new GameObject(RoomManagerListener_Obj);
            DontDestroyOnLoad(roomGameObj);
            roomManagerListener = roomGameObj.AddComponent<RoomManagerListener>();
            roomManagerListener.delegater = new List<IRoomManagerDelegate>();
        }

        public void OnSuccess(string jsonString) {

            if (jsonString == null) return;

            JSONNode jo = JSON.Parse(jsonString);

            string callbackId = jo["callbackId"].Value;

            if (dictionary.ContainsKey(callbackId))

            {

                CallBack callBack = (CallBack)dictionary[callbackId];

                if (callBack.Success != null)

                {
                    callBack.Success();
                }

                dictionary.Remove(callbackId);

            }
            
        }

        public void OnSuccessValue(string jsonString) {

            if (jsonString == null) return;

            JSONNode jo = JSON.Parse(jsonString);

            string callbackId = jo["callbackId"].Value;

            if (dictionary.ContainsKey(callbackId))
            {
                string value = jo["type"].Value;
                JSONNode responseValue = jo["value"];
                if (value == "List<String>")
                {
                    List<string> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToStringList(responseValue.Value);
                    }
                    ValueCallBack<List<string>> valueCallBack = (ValueCallBack<List<string>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<EMGroup>")
                {
                    List<Group> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToGroupList(responseValue.Value);
                    }

                    ValueCallBack<List<Group>> valueCallBack = (ValueCallBack<List<Group>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "EMCursorResult<EMGroupInfo>")
                {
                    CursorResult<GroupInfo> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToGroupInfoResult(responseValue.Value);
                    }

                    ValueCallBack<CursorResult<GroupInfo>> valueCallBack = (ValueCallBack<CursorResult<GroupInfo>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "EMCursorResult<String>")
                {
                    CursorResult<string> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToStringResult(responseValue.Value);
                    }

                    ValueCallBack<CursorResult<string>> valueCallBack = (ValueCallBack<CursorResult<string>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "EMCursorResult<EMMessage>")
                {
                    CursorResult<Message> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToMessageResult(responseValue.Value);
                    }

                    ValueCallBack<CursorResult<Message>> valueCallBack = (ValueCallBack<CursorResult<Message>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<EMMucSharedFile>")
                {
                    List<GroupSharedFile> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToGroupSharedFileList(responseValue.Value);
                    }

                    ValueCallBack<List<GroupSharedFile>> valueCallBack = (ValueCallBack<List<GroupSharedFile>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "EMGroup")
                {
                    Group result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = new Group(responseValue.Value);
                    }
                    ValueCallBack<Group> valueCallBack = (ValueCallBack<Group>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "EMPageResult<EMChatRoom>")
                {
                    PageResult<Room> result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = TransformTool.JsonStringToRoomPageResult(responseValue.Value);
                    }

                    ValueCallBack<PageResult<Room>> valueCallBack = (ValueCallBack<PageResult<Room>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<EMChatRoom>")
                {
                    List<Room> result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = TransformTool.JsonStringToRoomList(responseValue.Value);
                    }

                    ValueCallBack<List<Room>> valueCallBack = (ValueCallBack<List<Room>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<EMConversation>")
                {
                    List<Conversation> result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = TransformTool.JsonStringToConversationList(responseValue.Value);
                    }

                    ValueCallBack<List<Conversation>> valueCallBack = (ValueCallBack<List<Conversation>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "EMChatRoom")
                {
                    Room result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = new Room(responseValue.Value);
                    }
                    ValueCallBack<Room> valueCallBack = (ValueCallBack<Room>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "EMPushConfigs")
                {
                    PushConfig result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = new PushConfig(responseValue.Value);
                    }

                    ValueCallBack<PushConfig> valueCallBack = (ValueCallBack<PushConfig>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "bool")
                {
                    ValueCallBack<bool> valueCallBack = (ValueCallBack<bool>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        bool ret = false;
                        do
                        {
                            if (responseValue != null)
                            {
                                ret = responseValue.Value == "0" ? false : true;
                            }

                        } while (false);

                        valueCallBack.OnSuccessValue(ret);

                    }

                    dictionary.Remove(callbackId);
                }
                else if (value == "String")
                {
                    ValueCallBack<string> valueCallBack = (ValueCallBack<string>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        string str = null;
                        if (responseValue != null && responseValue.IsString)
                        {
                            str = responseValue.Value;
                        }
                        valueCallBack.OnSuccessValue(str);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<EMMessage>")
                {
                    ValueCallBack<List<Message>> valueCallBack = (ValueCallBack<List<Message>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        List<Message> result = null;
                        if (responseValue != null)
                        {
                            result = TransformTool.JsonStringToMessageList(responseValue.Value);
                        }

                        valueCallBack.OnSuccessValue(result);
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "OnMessageSuccess")
                {
                    Message msg = new Message(responseValue.Value);

                    foreach (var info in tempMsgDict) {
                        Debug.Log($"key -- {info.Key}");
                    }

                    Message sendMsg = tempMsgDict[msg.LocalTime.ToString()];
                    sendMsg.MsgId = msg.MsgId;
                    sendMsg.Body = msg.Body;
                    sendMsg.Status = msg.Status;
                    tempMsgDict.Remove(msg.LocalTime.ToString());
                    OnSuccess(jsonString);
                }
                else if (value == "OnMessageError") {
                    Message msg = new Message(responseValue.Value);
                    Message sendMsg = tempMsgDict[msg.LocalTime.ToString()];
                    sendMsg.Body = msg.Body;
                    sendMsg.Status = msg.Status;
                    tempMsgDict.Remove(msg.LocalTime.ToString());
                    OnError(jsonString);
                }
            }
        }


        public void OnProgress(string jsonString) {

            if (jsonString == null) return;

            JSONNode jo = JSON.Parse(jsonString);

            string callbackId = jo["callbackId"].Value;

            if (dictionary.ContainsKey(callbackId))
            {
                int progress = jo["progress"].AsInt;

                CallBack callBack = (CallBack)dictionary[callbackId];

                if (callBack.Progress != null)
                {
                    callBack.Progress(progress);
                }
            }
        }

        public void OnError(string jsonString) {

            if (jsonString == null) return;

            JSONNode jo = JSON.Parse(jsonString);

            string callbackId = jo["callbackId"].Value;

            int code = jo["code"].AsInt;

            string desc = jo["desc"].Value;

            CallBack callBack = (CallBack)dictionary[callbackId];

            if (callBack != null && callBack.Error != null)
            {
                callBack.Error(code, desc);
            }

            dictionary.Remove(callbackId);
        }
    }
}