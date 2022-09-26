using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat
{
    internal sealed class ChatManager_Common : IChatManager
    {
        private IntPtr client;
        private ChatManagerHub chatManagerHub;
        private ReactionManagerHub reactionManagerHub;

        System.Object msgMapLocker;
        Dictionary<string, IntPtr> msgPtrMap;
        Dictionary<string, Message> msgMap;
        Dictionary<int, string> cbId2MsgIdMap;

        //manager level events

        internal ChatManager_Common(IClient _client)
        {
            if (_client is Client_Common clientCommon)
            {
                client = clientCommon.client;
            }
            chatManagerHub = new ChatManagerHub();
            reactionManagerHub = new ReactionManagerHub();

            //registered listeners
            ChatAPINative.ChatManager_AddListener(client, chatManagerHub.OnMessagesReceived,
                chatManagerHub.OnCmdMessagesReceived, chatManagerHub.OnMessagesRead, chatManagerHub.OnMessagesDelivered,
                chatManagerHub.OnMessagesRecalled, chatManagerHub.OnReadAckForGroupMessageUpdated, chatManagerHub.OnGroupMessageRead,
                chatManagerHub.OnConversationsUpdate, chatManagerHub.OnConversationRead, reactionManagerHub.messageReactionDidChange);

            msgPtrMap = new Dictionary<string, IntPtr>();
            msgMap = new Dictionary<string, Message>();
            cbId2MsgIdMap = new Dictionary<int, string>();
            msgMapLocker = new System.Object();
        }

        public void AddMsgMap(string localmsgId, IntPtr msgPtr, Message msg, int cbId)
        {
            lock (msgMapLocker)
            {
                msgPtrMap.Add(localmsgId, msgPtr);
                msgMap.Add(localmsgId, msg);
                cbId2MsgIdMap.Add(cbId, localmsgId);
            }
        }

        public void UpdatedMsg(string localmsgId)
        {
            lock (msgMapLocker)
            {
                if (msgPtrMap.ContainsKey(localmsgId) && msgMap.ContainsKey(localmsgId))
                {
                    var msg = msgMap[localmsgId];
                    var intPtr = msgPtrMap[localmsgId];
                    var messageTO = MessageTO.FromIntPtr(intPtr, msg.Body.Type);
                    messageTO.UpdateMsg(msg);
                }
            }
        }

        public void DeleteFromMsgMap(string localmsgId, int cbId)
        {
            lock (msgMapLocker)
            {
                if (msgPtrMap.ContainsKey(localmsgId))
                {
                    IntPtr intPtr = msgPtrMap[localmsgId];
                    msgPtrMap.Remove(localmsgId);
                    Marshal.FreeCoTaskMem(intPtr);
                }
                if (msgMap.ContainsKey(localmsgId))
                {
                    msgMap.Remove(localmsgId);
                }
                if (cbId2MsgIdMap.ContainsKey(cbId))
                {
                    cbId2MsgIdMap.Remove(cbId);
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
            ChatAPINative.ChatManager_RemoveConversation(client, conversationId, deleteMessages, false);
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
                },
                (int progress, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnProgress(cbId, progress);
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
                },
                (int progress, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnProgress(cbId, progress);
                }
                );
        }

        public override void FetchHistoryMessagesFromServer(string conversationId, ConversationType type, string startMessageId = null, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<CursorResult<Message>> handle = null)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_FetchHistoryMessages(client, callbackId, conversationId, type, startMessageId, count, direction,

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
            bool conversationExist = ChatAPINative.ChatManager_ConversationWithType(client, conversationId, type, createIfNeed, false);
            Debug.Log($"conversationExist is {conversationExist}");
            if (conversationExist || createIfNeed)
                return new Conversation(conversationId, type, false);
            else
                return null;
        }

        public override Conversation GetThreadConversation(string threadId)
        {
            if (null == threadId || 0 == threadId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return null;
            }
            bool conversationExist = ChatAPINative.ChatManager_ConversationWithType(client, threadId, ConversationType.Group, true, true);
            Debug.Log($"conversationExist is {conversationExist}");
            if (conversationExist)
                return new Conversation(threadId, ConversationType.Group, true);
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
                    if (DataType.ListOfConversation == dType)
                    {
                        var result = new List<Conversation>();
                        for (int i = 0; i < size; i++)
                        {
                            ConversationTO conversationTO = Marshal.PtrToStructure<ConversationTO>(array[i]);

                            Conversation conversation = new Conversation(conversationTO.ConverationId, conversationTO.Type, conversationTO.isThread);
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

                            Conversation conversation = new Conversation(conversationTO.ConverationId, conversationTO.Type, conversationTO.isThread);
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
                 },
                 (int progress, int cbId) =>
                 {
                     ChatCallbackObject.CallBackOnProgress(cbId, progress);
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
            AddMsgMap(message.MsgId, mtoPtr, message, callbackId);

            ChatAPINative.ChatManager_SendMessage(client, callbackId,
                (int cbId) =>
                {
                    try
                    {
                        ChatManager_Common chatCommon = (ChatManager_Common)SDKClient.Instance.ChatManager;
                        if (chatCommon.cbId2MsgIdMap.ContainsKey(cbId))
                        {
                            string msgId = chatCommon.cbId2MsgIdMap[cbId];
                            chatCommon.UpdatedMsg(msgId);
                            chatCommon.DeleteFromMsgMap(msgId, cbId);
                        }
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc, int cbId) =>
                {
                    ChatManager_Common chatCommon = (ChatManager_Common)SDKClient.Instance.ChatManager;
                    if (chatCommon.cbId2MsgIdMap.ContainsKey(cbId))
                    {
                        string msgId = chatCommon.cbId2MsgIdMap[cbId];
                        chatCommon.DeleteFromMsgMap(msgId, cbId);
                    }
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                },
                (int progress, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnProgress(cbId, progress);
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

        public override void SendReadAckForGroupMessage(string messageId, string ackContent, CallBack callback = null)
        {
            if (null == messageId || 0 == messageId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            ChatAPINative.ChatManager_SendReadAckForGroupMessage(client, callbackId, messageId, ackContent,
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

        public override void RemoveMessagesBeforeTimestamp(long timeStamp, CallBack callback = null)
        {
            if (timeStamp < 0)
            {
                Debug.LogError("timeStamp is negtive!");
                return;
            }

            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            ChatAPINative.ChatManager_RemoveMessagesBeforeTimestamp(client, callbackId, timeStamp,
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

        public override void DeleteConversationFromServer(string conversationId, ConversationType conversationType, bool isDeleteServerMessages, CallBack callback = null)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }

            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            ChatAPINative.ChatManager_DeleteConversationFromServer(client, callbackId, conversationId, conversationType, isDeleteServerMessages,
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

        public override void FetchSupportLanguages(ValueCallBack<List<SupportLanguage>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_FetchSupportLanguages(client, callbackId,
                (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    List<SupportLanguage> supportList = new List<SupportLanguage>();
                    if (DataType.ListOfString == dType && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var supportItem = Marshal.PtrToStructure<SupportLanguagesTO>(data[i]);
                            SupportLanguage spl = supportItem.SupportLanguagesInfo();
                            supportList.Add(spl);
                        }
                        ChatCallbackObject.ValueCallBackOnSuccess<List<SupportLanguage>>(cbId, supportList);
                    }
                    else
                    {
                        Debug.LogError($"Supported languages information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<SupportLanguage>>(cbId, code, desc);
                });
        }

        public override void TranslateMessage(Message message, List<string> targetLanguages, ValueCallBack<Message>handle = null)
        {
            if (null == targetLanguages || targetLanguages.Count == 0)
            {
                Debug.LogError("targetLanguages is valid!");
                return;
            }

            MessageTO mto = MessageTO.FromMessage(message);
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);

            int size = targetLanguages.Count;
            var tlArray = targetLanguages.ToArray();

            ChatAPINative.ChatManager_TranslateMessage(client, callbackId, mtoPtr, message.Body.Type, tlArray, size,
                (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    string json = TransformTool.PtrToString(data[0]);
                    Message msg = new Message(json);
                    ChatCallbackObject.ValueCallBackOnSuccess<Message>(cbId, msg);
                },
                (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.ValueCallBackOnError<Message>(cbId, code, desc);
                }
               );

        }

        public override void FetchGroupReadAcks(string messageId, string groupId, int pageSize = 20, string startAckId = null, ValueCallBack<CursorResult<GroupReadAck>> handle = null)
        {
            if (null == messageId || 0 == messageId.Length || null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_FetchGroupReadAcks(client, callbackId, messageId, groupId, pageSize, startAckId,

                (IntPtr header, IntPtr[] array, DataType dType, int size, int cbId) => {
                    Debug.Log($"FetchGroupReadAcks callback with dType={dType}, size={size}.");
                    var result = new CursorResult<GroupReadAck>();
                    result.Data = new List<GroupReadAck>();
                    if (DataType.CursorResult == dType)
                    {
                        //header
                        var cursorResultTO = Marshal.PtrToStructure<CursorResultTOV2>(header);
                        if (cursorResultTO.Type == DataType.ListOfGroup)
                        {
                            result.Cursor = cursorResultTO.NextPageCursor;
                            //items
                            for (int i = 0; i < size; i++)
                            {
                                TOItem item = Marshal.PtrToStructure<TOItem>(array[i]);
                                GroupReadAckTO gkto = Marshal.PtrToStructure<GroupReadAckTO>(item.Data);
                                GroupReadAck gk = gkto.Unmarshall();
                                if (null != gk)
                                {
                                    result.Data.Add(gk);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Invalid return type from native FetchGroupReadAcks(), please check native c wrapper code.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                    ChatCallbackObject.ValueCallBackOnSuccess<CursorResult<GroupReadAck>>(cbId, result);
                },

                (int code, string desc, int cbId) => {
                    Debug.LogError($"FetchGroupReadAcks failed with code={code},desc={desc}");
                    ChatCallbackObject.ValueCallBackOnError<CursorResult<GroupReadAck>>(cbId, code, desc);
                });
        }

        public override void ReportMessage(string messageId, string tag, string reason, CallBack handle = null)
        {
            if (null == messageId || 0 == messageId.Length || null == tag || 0 == tag.Length
                || null == reason || 0 == reason.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_ReportMessage(client, callbackId, messageId, tag, reason,
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

        public override void AddReaction(string messageId, string reaction, CallBack handle = null)
        {
            if (null == messageId || 0 == messageId.Length || null == reaction || 0 == reaction.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_AddReaction(client, callbackId, messageId, reaction,
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

        public override void RemoveReaction(string messageId, string reaction, CallBack handle = null)
        {
            if (null == messageId || 0 == messageId.Length || null == reaction || 0 == reaction.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_RemoveReaction(client, callbackId, messageId, reaction,
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

        public override void GetReactionList(List<string> messageIdList, MessageType chatType, string groupId, ValueCallBack<Dictionary<string, List<MessageReaction>>> handle = null)
        {
            if (messageIdList.Count == 0)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            TransformTool.DeleteEmptyStringFromList(ref messageIdList);
            string idList = TransformTool.JsonStringFromStringList(messageIdList);

            string ctype = (MessageType.Group == chatType) ? "groupchat" : "chat";

            ChatAPINative.ChatManager_GetReactionList(client, callbackId, idList, ctype, groupId,

                (IntPtr[] array, DataType dType, int size, int cbId) => {
                    Debug.Log($"GetReactionList callback with dType={dType}, size={size}.");
                    
                    if (DataType.String == dType && 1 == size)
                    {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
                        var result = Marshal.PtrToStringAnsi(array[0]);
#else
                        var result = Marshal.PtrToStringUni(array[0]);
#endif
                        var dict = MessageReaction.MapFromJson(TransformTool.GetUnicodeStringFromUTF8(result));
                        ChatCallbackObject.ValueCallBackOnSuccess<Dictionary<string, List<MessageReaction>>>(cbId, dict);
                    }
                    else
                    {
                        var dict = new Dictionary<string, List<MessageReaction>>();
                        ChatCallbackObject.ValueCallBackOnSuccess<Dictionary<string, List<MessageReaction>>>(cbId, dict);
                    }
                },

                (int code, string desc, int cbId) => {
                    Debug.LogError($"GetReactionList failed with code={code},desc={desc}");
                    ChatCallbackObject.ValueCallBackOnError<Dictionary<string, List<MessageReaction>>>(cbId, code, desc);
                });
        }

        public override void GetReactionDetail(string messageId, string reaction, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<MessageReaction>> handle = null)
        {
            if (null == messageId || 0 == messageId.Length || null == reaction || 0 == reaction.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ChatManager_GetReactionDetail(client, callbackId, messageId, reaction, cursor, pageSize,

               (IntPtr header, IntPtr[] array, DataType dType, int size, int cbId) => {
                    Debug.Log($"GetReactionDetail callback with dType={dType}, size={size}.");
                    var result = new CursorResult<MessageReaction>();

                    //header
                    var cursorResultTO = Marshal.PtrToStructure<CursorResultTOV2>(header);
                    result.Cursor = cursorResultTO.NextPageCursor;

                   //items
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
                   var json = Marshal.PtrToStringAnsi(array[0]);
#else
                   var json = Marshal.PtrToStringUni(array[0]);
#endif
                   var messageReation = MessageReaction.FromJson(TransformTool.GetUnicodeStringFromUTF8(json));
                   result.Data = new List<MessageReaction>();
                   if (null != messageReation) result.Data.Add(messageReation);
                   ChatCallbackObject.ValueCallBackOnSuccess<CursorResult<MessageReaction>>(cbId, result);
               },

                (int code, string desc, int cbId) => {
                    Debug.LogError($"GetReactionDetail failed with code={code},desc={desc}");
                    ChatCallbackObject.ValueCallBackOnError<CursorResult<MessageReaction>>(cbId, code, desc);
                });
        }
    }

}