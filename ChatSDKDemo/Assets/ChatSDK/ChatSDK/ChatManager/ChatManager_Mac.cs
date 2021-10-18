using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    internal sealed class ChatManager_Mac : IChatManager
    {
        private IntPtr client;
        private ChatManagerHub chatManagerHub;

        System.Object msgMapLocker;
        Dictionary<long, IntPtr> msgPtrMap;
        Dictionary<long, Message> msgMap;
        Dictionary<int, long> cbId2TsMap;

        //manager level events
        
        internal ChatManager_Mac(IClient _client)
        {
            if (_client is Client_Mac clientMac)
            {
                client = clientMac.client;
            }
            chatManagerHub = new ChatManagerHub();
            //registered listeners
            ChatAPINative.ChatManager_AddListener(client, chatManagerHub.OnMessagesReceived,
                chatManagerHub.OnCmdMessagesReceived, chatManagerHub.OnMessagesRead, chatManagerHub.OnMessagesDelivered,
                chatManagerHub.OnMessagesRecalled, chatManagerHub.OnReadAckForGroupMessageUpdated, chatManagerHub.OnGroupMessageRead,
                chatManagerHub.OnConversationsUpdate, chatManagerHub.OnConversationRead);

            msgPtrMap = new Dictionary<long, IntPtr>();
            msgMap = new Dictionary<long, Message>();
            cbId2TsMap = new Dictionary<int, long>();
            msgMapLocker = new System.Object();  
        }

        public void AddMsgMap(long ts, IntPtr msgPtr, Message msg, int cbId)
        {
            lock(msgMapLocker)
            {
                msgPtrMap.Add(ts, msgPtr);
                msgMap.Add(ts, msg);
                cbId2TsMap.Add(cbId, ts);
            }
        }

        public void UpdatedMsgId(long ts)
        {
            lock (msgMapLocker)
            {
                if (msgPtrMap.ContainsKey(ts) && msgMap.ContainsKey(ts))
                {
                    var intPtr = msgPtrMap[ts];
                    var messageTO = Marshal.PtrToStructure<MessageTO>(intPtr);
                    var msg = msgMap[ts];
                    msg.MsgId = messageTO.MsgId;
                }
            }
        }

        public void DeleteFromMsgMap(long ts, int cbId)
        {
            lock (msgMapLocker)
            {
                if(msgPtrMap.ContainsKey(ts))
                {
                    IntPtr intPtr = msgPtrMap[ts];
                    msgPtrMap.Remove(ts);
                    Marshal.FreeCoTaskMem(intPtr);
                }
                if (msgMap.ContainsKey(ts))
                {
                    msgMap.Remove(ts);
                }
                if (cbId2TsMap.ContainsKey(cbId))
                {
                    cbId2TsMap.Remove(cbId);
                }
            }
        }

        public override bool DeleteConversation(string conversationId, bool deleteMessages)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return false;
            }
            ChatAPINative.ChatManager_RemoveConversation(client, conversationId, deleteMessages);
            return true;
        }

        public override void DownloadAttachment(string messageId, CallBack handle = null)
        {
            if (null == messageId || 0 == messageId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_DownloadMessageAttachments(client, callbackId, messageId,
                (int cbId) =>
                {
                    try
                    {
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                }
                );
        }

        public override void DownloadThumbnail(string messageId, CallBack handle = null)
        {
            if (null == messageId || 0 == messageId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_DownloadMessageThumbnail(client, callbackId, messageId,
                (int cbId) =>
                {
                    try
                    {
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                }
                );
        }

        public override void FetchHistoryMessagesFromServer(string conversationId, ConversationType type, string startMessageId = null, int count = 20, ValueCallBack<CursorResult<Message>> handle = null)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_FetchHistoryMessages(client, callbackId, conversationId, type, startMessageId, count,

                (IntPtr header, IntPtr[] array, DataType dType, int size, int cbId) => {
                    Debug.Log($"FetchHistoryMessages callback with dType={dType}, size={size}."); 
                    if (DataType.CursorResult == dType)
                    {
                        //header
                        var cursorResultTO = Marshal.PtrToStructure<CursorResultTOV2>(header);
                        if (cursorResultTO.Type == DataType.ListOfMessage)
                        {
                            var result = new CursorResult<Message>();
                            result.Cursor = cursorResultTO.NextPageCursor;

                            MessageTO[] messages = new MessageTO[size];
                            MessageTO mto = null;
                            //items
                            for (int i = 0; i < size; i++)
                            {
                                TOItem item = Marshal.PtrToStructure<TOItem>(array[i]);
                                mto = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                                if (null != mto) messages[i] = mto;
                            }
                            result.Data = MessageTO.ConvertToMessageList(messages, size);
                            ChatCallbackObject.ValueCallBackOnSuccess<CursorResult<Message>>(cbId, result);
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid return type from native ChatManager_FetchHistoryMessages(), please check native c wrapper code.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                },

                (int code, string desc, int cbId) => {
                    Debug.LogError($"FetchHistoryMessages failed with code={code},desc={desc}");
                    ChatCallbackObject.ValueCallBackOnError<CursorResult<Message>>(cbId, code, desc);
                });
        }

        public override Conversation GetConversation(string conversationId, ConversationType type, bool createIfNeed = true)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return null;
            }
            bool conversationExist = ChatAPINative.ChatManager_ConversationWithType(client, conversationId, type, createIfNeed);
            Debug.Log($"conversationExist is {conversationExist}");
            if (conversationExist || createIfNeed)
                return new Conversation(conversationId, type);
            else
                return null;
        }

        public override void GetConversationsFromServer(ValueCallBack<List<Conversation>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_GetConversationsFromServer(client, callbackId, 
                (IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"GetConversationsFromServer callback with dType={dType}, size={size}");
                    if(DataType.ListOfConversation == dType)
                    {
                        var result = new List<Conversation>();
                        for (int i=0; i<size; i++)
                        {
                            ConversationTO conversationTO = Marshal.PtrToStructure<ConversationTO>(array[i]);

                            Conversation conversation = new Conversation(conversationTO.ConverationId, conversationTO.Type);
                            //ExtField maybe empty
                            if (conversationTO.ExtField.Length > 0)
                                conversation.Ext = TransformTool.JsonStringToDictionary(conversationTO.ExtField); //to-do:ext is a json string?
                            result.Add(conversation);
                        }
                        ChatCallbackObject.ValueCallBackOnSuccess<List<Conversation>>(cbId, result);
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                },
                (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.ValueCallBackOnError<List<Conversation>>(cbId, code, desc);
                });
        }

        public override int GetUnreadMessageCount()
        {
            return ChatAPINative.ChatManager_GetUnreadMessageCount(client);
        }

        public override bool ImportMessages(List<Message> messages)
        {
            if (null == messages && messages.Count == 0)
                return true;

            int size = messages.Count;
            var messageArray = new IntPtr[size];
            var typeArray = new MessageBodyType[size];

            int i = 0;
            //List to array
            foreach (Message message in messages)
            {
                MessageTO mto = MessageTO.FromMessage(message);
                IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
                Marshal.StructureToPtr(mto, mtoPtr, false);
                messageArray[i] = mtoPtr;
                typeArray[i] = mto.BodyType;
                i++;
            }
            bool ret = ChatAPINative.ChatManager_InsertMessages(client, messageArray, typeArray, size);
            //free resource
            for (int j = 0; j < size; j++)
            {
                Marshal.FreeCoTaskMem(messageArray[j]);
            }
            return ret;
        }

        public override List<Conversation> LoadAllConversations()
        {
            var result = new List<Conversation>();
            ChatAPINative.ChatManager_LoadAllConversationsFromDB(client,
                (IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"GetConversationsFromServer callback with dType={dType}, size={size}");
                    if (DataType.ListOfConversation == dType)
                    {
                        
                        for (int i = 0; i < size; i++)
                        {
                            ConversationTO conversationTO = Marshal.PtrToStructure<ConversationTO>(array[i]);

                            Conversation conversation = new Conversation(conversationTO.ConverationId, conversationTO.Type);
                            //ExtField maybe empty
                            if (conversationTO.ExtField.Length > 0)
                                conversation.Ext = TransformTool.JsonStringToDictionary(conversationTO.ExtField); //to-do:ext is a json string?
                            result.Add(conversation);
                        }
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                }
                );

            return result;
        }

        public override Message LoadMessage(string messageId)
        {
            if (null == messageId || 0 == messageId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return null;
            }
            Message msg = null;
            ChatAPINative.ChatManager_GetMessage(client, messageId,
                (IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"LoadMessage callback with dType={dType}, size={size}");
                    if (DataType.ListOfMessage == dType)
                    {
                        if (0 == size)
                        {
                            Debug.Log($"Cannot find the message with id={messageId}");
                        }
                        else if (1 == size)
                        {
                            TOItem item = Marshal.PtrToStructure<TOItem>(array[0]);
                            MessageTO mto = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                            msg = mto.Unmarshall();
                        }
                        else
                        {
                            Debug.LogError($"Incorrect message size returned {size}.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Incorrect datatype returned.");
                    }
                },
                (int code, string desc, int cbId) => { Debug.Log($"Load message failed with error id={code}, desc={desc}"); });
            return msg;
        }

        public override bool MarkAllConversationsAsRead()
        {
            return ChatAPINative.ChatManager_MarkAllConversationsAsRead(client);

        }

        public override void RecallMessage(string messageId, CallBack handle = null)
        {
            if (null == messageId || 0 == messageId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;
            ChatAPINative.ChatManager_RecallMessage(client, callbackId, messageId,
                 (int cbId) =>
                 {
                     try
                     {
                         ChatCallbackObject.CallBackOnSuccess(cbId);
                     }
                     catch (NullReferenceException nre)
                     {
                         Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                     }

                 },
                 (int code, string desc, int cbId) =>
                 {
                     ChatCallbackObject.CallBackOnError(cbId, code, desc);
                 }
                 );
        }

        public override Message ResendMessage(string messageId, CallBack handle = null)
        {
            if (null == messageId || 0 == messageId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return null;
            }
            Message msg = null;
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_ResendMessage(client, callbackId, messageId,
                //onSuccessResult (used to get message from SDK)
                (IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"ResendMessage return message dType={dType}, size={size}");
                    if (DataType.ListOfMessage == dType)
                    {
                        if (0 == size)
                        {
                            Debug.Log($"Cannot find the message with id={messageId}");
                        }
                        else if (1 == size)
                        {
                            TOItem item = Marshal.PtrToStructure<TOItem>(array[0]);
                            MessageTO mto = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                            msg = mto.Unmarshall();
                        }
                        else
                        {
                            Debug.LogError($"Incorrect message size returned {size}.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Incorrect datatype returned.");
                    }
                },
                //onSuccess
                (int cbId) =>
                {
                    try
                    {
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                //onError
                (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                }
                );

            return msg;
        }

        public override List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.DOWN)
        {
            var messageList = new List<Message>();
            if (null == keywords || 0 == keywords.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return messageList;
            }

            ChatAPINative.ChatManager_LoadMoreMessages(client,
                (IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"SearchMsgFromDB return message dType={dType}, size={size}");

                    if (DataType.ListOfMessage == dType)
                    {
                        //items
                        for (int i = 0; i < size; i++)
                        {
                            TOItem item = Marshal.PtrToStructure<TOItem>(array[i]);
                            MessageTO mto = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                            if (null != mto) messageList.Add(mto.Unmarshall());
                        }
                    }
                    else
                    {
                        Debug.LogError("Incorrect datatype returned.");
                    }
                },
                keywords, timestamp, maxCount, from, direction);

            return messageList;
        }

        public override void SendConversationReadAck(string conversationId, CallBack callback = null)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            ChatAPINative.ChatManager_SendReadAckForConversation(client, callbackId, conversationId,
                (int cbId) =>
                {
                    try
                    {
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                }
                );
        }

        public override void SendMessage(ref Message message, CallBack callback = null)
        {
            MessageTO mto = MessageTO.FromMessage(message);
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            AddMsgMap(mto.LocalTime, mtoPtr, message, callbackId);
            
            ChatAPINative.ChatManager_SendMessage(client, callbackId, 
                (int cbId) =>
                {
                    try
                    {
                        ChatManager_Mac chatMac = (ChatManager_Mac)SDKClient.Instance.ChatManager;
                        if (chatMac.cbId2TsMap.ContainsKey(cbId))
                        {
                            long ts = chatMac.cbId2TsMap[cbId];
                            chatMac.UpdatedMsgId(ts);
                            chatMac.DeleteFromMsgMap(ts, cbId);
                        }
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    catch(NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }
                    
                },
                (int code, string desc, int cbId) =>
                {
                    ChatManager_Mac chatMac = (ChatManager_Mac)SDKClient.Instance.ChatManager;
                    if (chatMac.cbId2TsMap.ContainsKey(cbId))
                    {
                        long ts = chatMac.cbId2TsMap[cbId];
                        chatMac.DeleteFromMsgMap(ts, cbId);
                    }
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                },
                mtoPtr, message.Body.Type);

            //Marshal.FreeCoTaskMem(mtoPtr); // will be freed in DeleteFromMsgMap
        }

        public override void SendMessageReadAck(string messageId, CallBack callback = null)
        {
            if (null == messageId || 0 == messageId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            ChatAPINative.ChatManager_SendReadAckForMessage(client, callbackId, messageId,
                (int cbId) =>
                {
                    try
                    {
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                }
                );
        }

        public override bool UpdateMessage(Message message)
        {
            if (null == message)
            {
                Debug.LogError("Mandatory parameter is null!");
                return false;
            }
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ChatManager_UpdateMessage(client, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }
    }
}