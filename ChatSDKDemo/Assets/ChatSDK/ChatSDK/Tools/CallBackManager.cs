﻿using System;
using System.Collections.Generic;
using SimpleJSON;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace ChatSDK {

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal class CallbackManager : MonoBehaviour
#else
    internal class CallbackManager
#endif
    {
        private static bool isQuitting = false;

        static string Callback_Obj = "unity_chat_callback_obj";

        static string Connection_Obj = "unity_chat_emclient_connection_obj";
        static string ChatManagerListener_Obj = "unity_chat_emclient_chatmanager_delegate_obj";
        static string ContactManagerListener_Obj = "unity_chat_emclient_contactmanager_delegate_obj";
        static string GroupManagerListener_Obj = "unity_chat_emclient_groupmanager_delegate_obj";
        static string RoomManagerListener_Obj = "unity_chat_emclient_roommanager_delegate_obj";
        static string MultiDeviceListener_Obj = "unity_chat_emclient_multidevice_delegate_obj";
        static string PresenceManagerListener_Obj = "unity_chat_emclient_presencemanager_delegate_obj";
        static string ThreadManagerListener_Obj = "unity_chat_emclient_threadmanager_delegate_obj";


        internal ConnectionListener connectionListener;
        internal ChatManagerListener chatManagerListener;
        internal ContactManagerListener contactManagerListener;
        internal GroupManagerListener groupManagerListener;
        internal RoomManagerListener roomManagerListener;
        internal MultiDeviceListener multiDeviceListener;
        internal PresenceManagerListener presenceManagerListener;
        internal ChatThreadManagerListener threadManagerListener;

        internal int CurrentId { get; private set; }

        internal Dictionary<string, object> dictionary = new Dictionary<string, object>();

        private static CallbackManager _getInstance;

        internal Dictionary<string, Message> tempMsgDict = new Dictionary<string, Message>();

#if UNITY_EDITOR

        [RuntimeInitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            EditorApplication.wantsToQuit -= Quit;
            EditorApplication.wantsToQuit += Quit;

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static bool Quit()
        {
            IClient.Instance.Logout(false);
            CallbackManager.Instance().ClearResource();
            Debug.Log("Quit...");
            return true;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            switch (stateChange)
            {
                case (PlayModeStateChange.EnteredPlayMode):
                    {
                        EditorApplication.LockReloadAssemblies();
                        Debug.Log("Assembly Reload locked as entering play mode");
                        break;
                    }
                case (PlayModeStateChange.ExitingPlayMode):
                    {
                        Debug.Log("Assembly Reload unlocked as exiting play mode");
                        EditorApplication.UnlockReloadAssemblies();
                        break;
                    }
            }
        }
#endif

        private void OnApplicationQuit()
        {
            if (SDKClient.Instance.IsLoggedIn) {
                SDKClient.Instance.Logout(false);
            }
            CallbackManager.Instance().ClearResource();
            Debug.Log("Quit...");
        }

        /// 负责释放底层SDK资源，必须在logout后调用
        public void ClearResource()
        {
            CallbackManager.Instance().connectionListener.delegater.Clear();
            IClient.Instance.ContactManager().ClearDelegates();
            IClient.Instance.ChatManager().ClearDelegates();
            IClient.Instance.GroupManager().ClearDelegates();
            IClient.Instance.RoomManager().ClearDelegates();
            IClient.Instance.PresenceManager().ClearDelegates();
            IClient.Instance.ThreadManager().ClearDelegates();
            CallbackManager.Instance().multiDeviceListener.delegater.Clear();
            CallbackManager.Instance().CleanAllCallback();
            IClient.Instance.ClearResource();
        }

        public void OnDestroy()
        {
            isQuitting = true;
        }

        public static bool IsQuit()
        {
            return isQuitting;
        }

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
        internal static CallbackManager Instance()
        {

            if (_getInstance == null)
            {
                GameObject callbackGameObject = new GameObject(Callback_Obj);
                try
                {
                    DontDestroyOnLoad(callbackGameObject);
                    _getInstance = callbackGameObject.AddComponent<CallbackManager>();
                    _getInstance.SetupAllListeners();
                }
                catch(Exception)
                {
                    Debug.Log($"DontDestroyOnLoad triggered.");
                }

            }

            return _getInstance;
        }
#else
        internal static CallbackManager Instance()
        {
            if (_getInstance == null)
            {
                _getInstance = new CallbackManager();
                _getInstance.SetupAllListeners();
            }

            return _getInstance;
        }
#endif

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

        internal object GetCallBackHandle(int callbackId)
        {
            if (-1 == callbackId) return null;
            return dictionary[callbackId.ToString()];
        }

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
        internal void SetupAllListeners()
        {
            GameObject connectionObject = new GameObject(Connection_Obj);
            try
            {
                DontDestroyOnLoad(connectionObject);
                connectionListener = connectionObject.AddComponent<ConnectionListener>();
                connectionListener.delegater = new List<IConnectionDelegate>();
            }
            catch (Exception)
            {
                Debug.Log($"DontDestroyOnLoad triggered.");
            }

            GameObject chatManagerObject = new GameObject(ChatManagerListener_Obj);
            try
            {
                DontDestroyOnLoad(chatManagerObject);
                chatManagerListener = chatManagerObject.AddComponent<ChatManagerListener>();
                chatManagerListener.delegater = new List<IChatManagerDelegate>();
            }
            catch (Exception)
            {
                Debug.Log($"DontDestroyOnLoad triggered.");
            }


            GameObject contactGameObj = new GameObject(ContactManagerListener_Obj);
            try
            {
                DontDestroyOnLoad(contactGameObj);
                contactManagerListener = contactGameObj.AddComponent<ContactManagerListener>();
                contactManagerListener.delegater = new List<IContactManagerDelegate>();
            }
            catch (Exception)
            {
                Debug.Log($"DontDestroyOnLoad triggered.");
            }

            GameObject groupGameObj = new GameObject(GroupManagerListener_Obj);
            try
            {
                DontDestroyOnLoad(groupGameObj);
                groupManagerListener = groupGameObj.AddComponent<GroupManagerListener>();
                groupManagerListener.delegater = new List<IGroupManagerDelegate>();
            }
            catch (Exception)
            {
                Debug.Log($"DontDestroyOnLoad triggered.");
            }


            GameObject roomGameObj = new GameObject(RoomManagerListener_Obj);
            try
            {
                DontDestroyOnLoad(roomGameObj);
                roomManagerListener = roomGameObj.AddComponent<RoomManagerListener>();
                roomManagerListener.delegater = new List<IRoomManagerDelegate>();
            }
            catch (Exception)
            {
                Debug.Log($"DontDestroyOnLoad triggered.");
            }

            GameObject mdGameObj = new GameObject(MultiDeviceListener_Obj);
            try
            {
                DontDestroyOnLoad(mdGameObj);
                multiDeviceListener = mdGameObj.AddComponent<MultiDeviceListener>();
                multiDeviceListener.delegater = new List<IMultiDeviceDelegate>();
            }
            catch (Exception)
            {
                Debug.Log($"DontDestroyOnLoad triggered.");
            }

            GameObject presenceGameObj = new GameObject(PresenceManagerListener_Obj);
            try
            {
                DontDestroyOnLoad(presenceGameObj);
                presenceManagerListener = presenceGameObj.AddComponent<PresenceManagerListener>();
                presenceManagerListener.delegater = new List<IPresenceManagerDelegate>();
            }
            catch (Exception)
            {
                Debug.Log($"DontDestroyOnLoad triggered.");
            }

            GameObject threadManagerObject = new GameObject(ThreadManagerListener_Obj);
            try
            {
                DontDestroyOnLoad(threadManagerObject);
                threadManagerListener = threadManagerObject.AddComponent<ChatThreadManagerListener>();
                threadManagerListener.delegater = new List<IChatThreadManagerDelegate>();
            }
            catch (Exception)
            {
                Debug.Log($"DontDestroyOnLoad triggered.");
            }
        }
#else
        internal void SetupAllListeners()
        {
            connectionListener = new ConnectionListener();
            connectionListener.delegater = new List<IConnectionDelegate>();

            chatManagerListener = new ChatManagerListener();
            chatManagerListener.delegater = new List<IChatManagerDelegate>();

            contactManagerListener = new ContactManagerListener();
            contactManagerListener.delegater = new List<IContactManagerDelegate>();

            groupManagerListener = new GroupManagerListener();
            groupManagerListener.delegater = new List<IGroupManagerDelegate>();

            roomManagerListener = new RoomManagerListener();
            roomManagerListener.delegater = new List<IRoomManagerDelegate>();

            multiDeviceListener = new MultiDeviceListener();
            multiDeviceListener.delegater = new List<IMultiDeviceDelegate>();

            presenceManagerListener = new PresenceManagerListener();
            presenceManagerListener.delegater = new List<IPresenceManagerDelegate>();

            threadManagerListener = new ChatThreadManagerListener();
            threadManagerListener.delegater = new List<IChatThreadManagerDelegate>();
        }
#endif
        public void OnSuccess(string jsonString) {

            if (jsonString == null) return;

            JSONNode jo = JSON.Parse(jsonString);

            string callbackId = jo["callbackId"].Value;

            if (dictionary.ContainsKey(callbackId))

            {

                CallBack callBack = (CallBack)dictionary[callbackId];

                if (callBack.Success != null)
                {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        callBack.Success();
                    });
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
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<Group>")
                {
                    List<Group> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToGroupList(responseValue.Value);
                    }

                    ValueCallBack<List<Group>> valueCallBack = (ValueCallBack<List<Group>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "CursorResult<GroupInfo>")
                {
                    CursorResult<GroupInfo> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToGroupInfoResult(responseValue.Value);
                    }

                    ValueCallBack<CursorResult<GroupInfo>> valueCallBack = (ValueCallBack<CursorResult<GroupInfo>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "CursorResult<String>")
                {
                    CursorResult<string> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToStringResult(responseValue.Value);
                    }

                    ValueCallBack<CursorResult<string>> valueCallBack = (ValueCallBack<CursorResult<string>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "CursorResult<Message>")
                {
                    CursorResult<Message> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToMessageResult(responseValue.Value);
                    }

                    ValueCallBack<CursorResult<Message>> valueCallBack = (ValueCallBack<CursorResult<Message>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<MucSharedFile>")
                {
                    List<GroupSharedFile> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToGroupSharedFileList(responseValue.Value);
                    }

                    ValueCallBack<List<GroupSharedFile>> valueCallBack = (ValueCallBack<List<GroupSharedFile>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "Group")
                {
                    Group result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = new Group(responseValue.Value);
                    }
                    ValueCallBack<Group> valueCallBack = (ValueCallBack<Group>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "PageResult<ChatRoom>")
                {
                    PageResult<Room> result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = TransformTool.JsonStringToRoomPageResult(responseValue.Value);
                    }

                    ValueCallBack<PageResult<Room>> valueCallBack = (ValueCallBack<PageResult<Room>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
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
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<Conversation>")
                {
                    List<Conversation> result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = TransformTool.JsonStringToConversationList(responseValue.Value);
                    }

                    ValueCallBack<List<Conversation>> valueCallBack = (ValueCallBack<List<Conversation>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "ChatRoom")
                {
                    Room result = null;
                    if (responseValue != null && responseValue.IsString)
                    {
                        result = new Room(responseValue.Value);
                    }
                    ValueCallBack<Room> valueCallBack = (ValueCallBack<Room>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "PushConfigs")
                {
                    //PushConfig result = null;
                    //if (responseValue != null && responseValue.IsString)
                    //{
                    //    result = new PushConfig(responseValue.Value);
                    //}

                    //ValueCallBack<PushConfig> valueCallBack = (ValueCallBack<PushConfig>)dictionary[callbackId];
                    //if (valueCallBack.OnSuccessValue != null)
                    //{
                    //    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                    //    {
                    //        valueCallBack.OnSuccessValue(result);
                    //    });
                    //}
                    //dictionary.Remove(callbackId);
                }
                else if (value == "bool")
                {
                    ValueCallBack<bool> valueCallBack = (ValueCallBack<bool>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        bool result = false;
                        do
                        {
                            if (responseValue != null)
                            {
                                result = responseValue.Value == "0" ? false : true;
                            }

                        } while (false);

                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });

                    }

                    dictionary.Remove(callbackId);
                }
                else if (value == "String")
                {
                    ValueCallBack<string> valueCallBack = (ValueCallBack<string>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        string result = null;
                        if (responseValue != null && responseValue.IsString)
                        {
                            result = responseValue.Value;
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<Message>")
                {
                    ValueCallBack<List<Message>> valueCallBack = (ValueCallBack<List<Message>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        List<Message> result = null;
                        if (responseValue != null)
                        {
                            result = TransformTool.JsonStringToMessageList(responseValue.Value);
                        }

                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "OnMessageSuccess")
                {
                    Message msg = new Message(responseValue.Value);
                    Message sendMsg = tempMsgDict[msg.LocalTime.ToString()];
                    sendMsg.MsgId = msg.MsgId;
                    sendMsg.Body = msg.Body;
                    sendMsg.Status = msg.Status;
                    tempMsgDict.Remove(msg.LocalTime.ToString());
                    OnSuccess(jsonString);
                }
                else if (value == "OnMessageError")
                {
                    Message msg = new Message(responseValue.Value);
                    Message sendMsg = tempMsgDict[msg.LocalTime.ToString()];
                    sendMsg.Body = msg.Body;
                    sendMsg.Status = msg.Status;
                    tempMsgDict.Remove(msg.LocalTime.ToString());
                    OnError(jsonString);
                }
                else if (value == "Map<String, UserInfo>")
                {

                    ValueCallBack<Dictionary<string, UserInfo>> valueCallBack = (ValueCallBack<Dictionary<string, UserInfo>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        Dictionary<string, UserInfo> result = new Dictionary<string, UserInfo>();
                        JSONNode jn = JSON.Parse(responseValue.Value);
                        foreach (JSONNode obj in jn.AsArray)
                        {
                            foreach (string key in obj.Keys)
                            {
                                result[key] = new UserInfo(obj[key].Value);
                            }
                        }

                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<SupportLanguage>")
                {
                    ValueCallBack<List<SupportLanguage>> valueCallBack = (ValueCallBack<List<SupportLanguage>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        List<SupportLanguage> result = null;
                        if (responseValue != null)
                        {
                            result = TransformTool.JsonStringToSupportLanguageList(responseValue.Value);
                        }

                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "CursorResult<GroupReadAck>")
                {
                    CursorResult<GroupReadAck> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToGroupReadAckResult(responseValue.Value);
                    }

                    ValueCallBack<CursorResult<GroupReadAck>> valueCallBack = (ValueCallBack<CursorResult<GroupReadAck>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "CursorResult<MessageReaction>")
                {
                    CursorResult<MessageReaction> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToMessageReactionResult(responseValue.Value);
                    }

                    ValueCallBack<CursorResult<MessageReaction>> valueCallBack = (ValueCallBack<CursorResult<MessageReaction>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "Message")
                {
                    Message result = null;
                    if (responseValue != null)
                    {
                        result = new Message(responseValue.Value);
                    }

                    ValueCallBack<Message> valueCallBack = (ValueCallBack<Message>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "Dictionary<string, List<MessageReaction>>")
                {
                    Dictionary<string, List<MessageReaction>> result = null;
                    if (responseValue != null)
                    {
                        result = TransformTool.JsonStringToReactionMap(responseValue.Value);
                    }
                    ValueCallBack<Dictionary<string, List<MessageReaction>>> valueCallBack = (ValueCallBack<Dictionary<string, List<MessageReaction>>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "List<Presence>")
                {
                    List<Presence> result = TransformTool.JsonStringToPresenceList(responseValue.Value);
                    ValueCallBack<List<Presence>> valueCallBack = (ValueCallBack<List<Presence>>)dictionary[callbackId];
                    if (valueCallBack.OnSuccessValue != null)
                    {
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                        {
                            valueCallBack.OnSuccessValue(result);
                        });
                    }
                    dictionary.Remove(callbackId);
                }
                else if (value == "ChatThread")
                {
                    ValueCallBack<ChatThread> valueCallBack = (ValueCallBack<ChatThread>)dictionary[callbackId];
                    if (valueCallBack != null)
                    {
                        ChatThread result = null;
                        if (responseValue != null)
                        {
                            result = ChatThread.FromJson(responseValue.Value);
                            if (valueCallBack.OnSuccessValue != null)
                            {
                                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                                {
                                    valueCallBack.OnSuccessValue(result);
                                });
                            }
                            dictionary.Remove(callbackId);
                        }
                    }
                }
                else if (value == "Dictionary<string, Message>")
                {
                    ValueCallBack<Dictionary<string, Message>> valueCallBack = (ValueCallBack<Dictionary<string, Message>>)dictionary[callbackId];
                    if (valueCallBack != null)
                    {
                        if (valueCallBack.OnSuccessValue != null)
                        {
                            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                            {
                                valueCallBack.OnSuccessValue(ChatThread.DictFromJson(responseValue.Value));
                            });
                        }
                        dictionary.Remove(callbackId);
                    }
                }
                else if (value == "CursorResult<ChatThread>") {
                    ValueCallBack<CursorResult<ChatThread>> valueCallBack = (ValueCallBack<CursorResult<ChatThread>>)dictionary[callbackId];
                    if (valueCallBack != null)
                    {
                        if (valueCallBack.OnSuccessValue != null)
                        {
                            CursorResult<ChatThread> result = ChatThread.CursorThreadFromJson(responseValue.Value);

                            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() =>
                            {
                                valueCallBack.OnSuccessValue(result);
                            });
                        }
                        dictionary.Remove(callbackId);
                    }
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
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    callBack.Error(code, desc);
                });
            }

            dictionary.Remove(callbackId);
        }
    }
}