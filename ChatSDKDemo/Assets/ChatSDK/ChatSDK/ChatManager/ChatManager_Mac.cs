using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    internal class ChatManager_Mac : IChatManager
    {
        private IntPtr client;
        private ChatManagerHub chatManagerHub;

        //manager level events
        
        internal ChatManager_Mac(IClient _client)
        {
            if (_client is Client_Mac clientMac)
            {
                client = clientMac.client;
            }
            chatManagerHub = new ChatManagerHub(CallbackManager.Instance().chatManagerDelegates);
            //registered listeners
            ChatAPINative.ChatManager_AddListener(client, chatManagerHub.OnMessagesReceived,
                chatManagerHub.OnCmdMessagesReceived, chatManagerHub.OnMessagesRead, chatManagerHub.OnMessagesDelivered,
                chatManagerHub.OnMessagesRecalled, chatManagerHub.OnReadAckForGroupMessageUpdated, chatManagerHub.OnGroupMessageRead,
                chatManagerHub.OnConversationsUpdate, chatManagerHub.OnConversationRead);
        }

        public override bool DeleteConversation(string conversationId, bool deleteMessages)
        {
            ChatAPINative.ChatManager_RemoveConversation(client, conversationId, deleteMessages);
            return true;
        }

        public override void DownloadAttachment(string messageId, CallBack handle = null)
        {
            ChatAPINative.ChatManager_DownloadMessageAttachments(client, messageId,
                () =>
                {
                    try
                    {
                        handle?.Success();
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc) => handle?.Error(code, desc)
                );
        }

        public override void DownloadThumbnail(string messageId, CallBack handle = null)
        {
            ChatAPINative.ChatManager_DownloadMessageThumbnail(client, messageId,
                () =>
                {
                    try
                    {
                        handle?.Success();
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc) => handle?.Error(code, desc)
                );
        }

        public override void FetchHistoryMessagesFromServer(string conversationId, ConversationType type, string startMessageId = null, int count = 20, ValueCallBack<CursorResult<Message>> handle = null)
        {
            ChatAPINative.ChatManager_FetchHistoryMessages(client, conversationId, type, startMessageId, count,
                (IntPtr[] cursorResult, DataType dType, int size) => {
                    Debug.Log($"FetchHistoryMessages callback with dType={dType}, size={size}.");
                    //Verification: dType == DataType.CursorResult, size == 1
                    if (dType == DataType.CursorResult && size == 1)
                    {
                        var cursorResultTO = Marshal.PtrToStructure<CursorResultTO>(cursorResult[0]);
                        if (cursorResultTO.Type == DataType.ListOfMessage)
                        {
                            var result = new CursorResult<Message>();
                            result.Cursor = cursorResultTO.NextPageCursor;
                            int msgSize = cursorResultTO.Size;
                            MessageTO[] messages = new MessageTO[msgSize];
                            MessageBodyType[] subTypes = cursorResultTO.SubTypes;
                            IntPtr[] dataPtr = cursorResultTO.Data;
                            MessageBodyType msgType;
                            MessageTO mto = null;
                            for (int i = 0; i < msgSize; i++)
                            {
                                msgType = subTypes[i];
                                switch (msgType)
                                {
                                    //to-do: need to other types?
                                    case MessageBodyType.TXT:
                                        mto = new TextMessageTO();
                                        Marshal.PtrToStructure(dataPtr[i], mto);
                                        messages[i] = mto;
                                        break;
                                    case MessageBodyType.LOCATION:
                                        mto = new LocationMessageTO();
                                        Marshal.PtrToStructure(dataPtr[i], mto);
                                        messages[i] = mto;
                                        break;
                                    case MessageBodyType.CMD:
                                        mto = new CmdMessageTO();
                                        Marshal.PtrToStructure(dataPtr[i], mto);
                                        messages[i] = mto;
                                        break;
                                }
                            }
                            result.Data = MessageTO.ConvertToMessageList(messages, msgSize);
                            handle?.OnSuccessValue(result);
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
                (int code, string desc) => {
                    Debug.LogError($"FetchHistoryMessages failed with code={code},desc={desc}");
                    handle?.Error(code, desc);
                });
        }

        public override Conversation GetConversation(string conversationId, ConversationType type, bool createIfNeed = true)
        {
            bool conversationExist = ChatAPINative.ChatManager_ConversationWithType(client, conversationId, type, createIfNeed);
            Debug.Log($"conversationExist is {conversationExist}");
            if (conversationExist || createIfNeed)
                return new Conversation(conversationId, type);
            else
                return null;
        }

        public override void GetConversationsFromServer(ValueCallBack<List<Conversation>> handle = null)
        {
            ChatAPINative.ChatManager_GetConversationsFromServer(client,
                (IntPtr[] conversationResult, DataType dType, int size) =>
                {
                    Debug.Log($"GetConversationsFromServer callback with dType={dType}, size={size}");
                    //Verify parameter
                    if(dType == DataType.ListOfConversation && size == 1)
                    {
                        var conversationTOArray = Marshal.PtrToStructure<TOArray>(conversationResult[0]);
                        var result = new List<Conversation>();
                        int toSize = conversationTOArray.Size;
                        for (int i=0; i< toSize; i++)
                        {                            
                            ConversationTO conversationTO = new ConversationTO();
                            Marshal.PtrToStructure(conversationTOArray.Data[i], conversationTO);
                            Conversation conversation = new Conversation(conversationTO.ConverationId, conversationTO.Type);
                            //ExtField maybe empty
                            if (conversationTO.ExtField.Length > 0)
                                conversation.Ext = TransformTool.JsonStringToDictionary(conversationTO.ExtField); //to-do:ext is a json string?
                            result.Add(conversation);
                        }
                        handle?.OnSuccessValue(result);
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                },
                (int code, string desc) => handle?.Error(code, desc));
        }

        public override int GetUnreadMessageCount()
        {
            return ChatAPINative.ChatManager_GetUnreadMessageCount(client);
        }

        public override bool ImportMessages(List<Message> messages)
        {
            if (messages == null && messages.Count == 0)
                return true;
             //turn List<> into array
            int size = 0;
            //var messageArray = new IntPtr[0];
            //var typeArray = new MessageBodyType[0];
            size = messages.Count;
            var messageArray = new IntPtr[size];
            var typeArray = new MessageBodyType[size];
            int i = 0;
            foreach (Message message in messages)
            {
                MessageTO mto = MessageTO.FromMessage(message);
                IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
                Marshal.StructureToPtr(mto, mtoPtr, false);
                messageArray[i] = mtoPtr;
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
            //Construct param array
            TOArray toArray = new TOArray();
            toArray.Size = 0;

            IntPtr intPtr = Marshal.AllocCoTaskMem(toArray.Size);
            Marshal.StructureToPtr(toArray, intPtr, false);

            var toArrayList = new IntPtr[1];
            toArrayList[0] = intPtr;

            //Call API will change field value in toArray
            ChatAPINative.ChatManager_LoadAllConversationsFromDB(client, toArrayList, 1);

            //Parse return param array
            Marshal.PtrToStructure(toArrayList[0], toArray);
            List<Conversation> conversationList = new List<Conversation>();

            //Parse every returned ConversationTO item
            for (int i=0; i< toArray.Size; i++)
            {
                ConversationTO conversationTO = new ConversationTO();
                Marshal.PtrToStructure(toArray.Data[i], conversationTO);
                Conversation conversation = new Conversation(conversationTO.ConverationId, conversationTO.Type);
                conversationList.Add(conversation);
            }

            //free resource from SDK
            ChatAPINative.ChatManager_ReleaseConversationList(toArrayList, 1);
            //free IntPtr
            Marshal.FreeCoTaskMem(intPtr);

            return conversationList;
        }

        public override Message LoadMessage(string messageId)
        {
            //Construct param array
            TOArrayDiff toArray = new TOArrayDiff();
            toArray.Size = 0;

            IntPtr intPtr = Marshal.AllocCoTaskMem(toArray.Size);
            Marshal.StructureToPtr(toArray, intPtr, false);

            var toArrayList = new IntPtr[1];
            toArrayList[0] = intPtr;

            ChatAPINative.ChatManager_GetMessage(client, messageId, toArrayList, 1);

            //Parse return param array
            TOArrayDiff returnTOArray = new TOArrayDiff();
            Marshal.PtrToStructure(toArrayList[0], returnTOArray);

            //no any message returned
            if (0 == returnTOArray.Size)
            {
                Marshal.FreeCoTaskMem(intPtr);
                return null;
            }

            MessageTO mto = null;
            MessageBodyType type = (MessageBodyType)returnTOArray.Type[0];

            switch (type)
            {
                //to-do: need to add other types?
                case MessageBodyType.TXT:
                    mto = new TextMessageTO();
                    Marshal.PtrToStructure(returnTOArray.Data[0], mto);
                    break;
                case MessageBodyType.LOCATION:
                    mto = new LocationMessageTO();
                    Marshal.PtrToStructure(returnTOArray.Data[0], mto);
                    break;
                case MessageBodyType.CMD:
                    mto = new CmdMessageTO();
                    Marshal.PtrToStructure(returnTOArray.Data[0], mto);
                    break;
            }

            //free resouce from SDK
            ChatAPINative.ChatManager_ReleaseMessageList(toArrayList, 1);
            //free IntPtr
            Marshal.FreeCoTaskMem(intPtr);

            return mto.Unmarshall();
        }

        public override bool MarkAllConversationsAsRead()
        {
            return ChatAPINative.ChatManager_MarkAllConversationsAsRead(client);

        }

        public override void RecallMessage(string messageId, CallBack handle = null)
        {
            ChatAPINative.ChatManager_RecallMessage(client, messageId,
                 () =>
                 {
                     try
                     {
                         handle?.Success();
                     }
                     catch (NullReferenceException nre)
                     {
                         Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                     }

                 },
                 (int code, string desc) => handle?.Error(code, desc)
                 );
        }

        public override Message ResendMessage(string messageId, CallBack handle = null)
        {
            //Construct param array
            TOArrayDiff toArray = new TOArrayDiff();
            toArray.Size = 0;

            IntPtr intPtr = Marshal.AllocCoTaskMem(toArray.Size);
            Marshal.StructureToPtr(toArray, intPtr, false);

            var toArrayList = new IntPtr[1];
            toArrayList[0] = intPtr;

            ChatAPINative.ChatManager_ResendMessage(client, messageId, toArrayList, 1,
                () =>
                {
                    try
                    {
                        handle?.Success();
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc) => handle?.Error(code, desc)
                );

            //Parse return param array
            TOArrayDiff returnTOArray = new TOArrayDiff();
            Marshal.PtrToStructure(toArrayList[0], returnTOArray);

            //no any message returned
            if (0 == returnTOArray.Size)
            {
                Marshal.FreeCoTaskMem(intPtr);
                return null;
            }

            MessageTO mto = null;
            //tricky: use DataType to save MessageBodyType
            MessageBodyType type = (MessageBodyType)returnTOArray.Type[0];

            switch (type)
            {
                //to-do: need to add other types?
                case MessageBodyType.TXT:
                    mto = new TextMessageTO();
                    Marshal.PtrToStructure(returnTOArray.Data[0], mto);
                    break;
                case MessageBodyType.LOCATION:
                    mto = new LocationMessageTO();
                    Marshal.PtrToStructure(returnTOArray.Data[0], mto);
                    break;
                case MessageBodyType.CMD:
                    mto = new CmdMessageTO();
                    Marshal.PtrToStructure(returnTOArray.Data[0], mto);
                    break;
            }

            //free resource from SDK
            ChatAPINative.ChatManager_ReleaseMessageList(toArrayList, 1);
            //free IntPtr
            Marshal.FreeCoTaskMem(intPtr);

            return mto.Unmarshall();
        }

        public override List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            //Construct param array
            TOArrayDiff toArray = new TOArrayDiff();
            toArray.Size = 0;

            IntPtr intPtr = Marshal.AllocCoTaskMem(toArray.Size);
            Marshal.StructureToPtr(toArray, intPtr, false);

            var toArrayList = new IntPtr[1];
            toArrayList[0] = intPtr;

            ChatAPINative.ChatManager_LoadMoreMessages(client, toArrayList, 1, keywords, timestamp, maxCount, from, direction);

            //Parse return param array
            TOArrayDiff returnTOArray = new TOArrayDiff();
            Marshal.PtrToStructure(toArrayList[0], returnTOArray);

            //no any message returned
            if (0 == returnTOArray.Size)
            {
                Debug.Log($"Cannot find any message with kw:{keywords}, ts:{timestamp}, from:{from}, direction:{direction}");
                Marshal.FreeCoTaskMem(intPtr);
                return new List<Message>();
            }

            MessageTO[] messages = new MessageTO[returnTOArray.Size];
            MessageBodyType msgType;
            MessageTO mto = null;
            for (int i = 0; i < returnTOArray.Size; i++)
            {
                msgType = (MessageBodyType)returnTOArray.Type[i];
                switch (msgType)
                {
                    //to-do: need to other types?
                    case MessageBodyType.TXT:
                        mto = new TextMessageTO();
                        Marshal.PtrToStructure(returnTOArray.Data[i], mto);
                        messages[i] = mto;
                        break;
                    case MessageBodyType.LOCATION:
                        mto = new LocationMessageTO();
                        Marshal.PtrToStructure(returnTOArray.Data[i], mto);
                        messages[i] = mto;
                        break;
                    case MessageBodyType.CMD:
                        mto = new CmdMessageTO();
                        Marshal.PtrToStructure(returnTOArray.Data[i], mto);
                        messages[i] = mto;
                        break;
                }
            }
            List<Message> messageList = MessageTO.ConvertToMessageList(messages, returnTOArray.Size);

            //free resource from SDK
            ChatAPINative.ChatManager_ReleaseMessageList(toArrayList, 1);
            //free IntPtr
            Marshal.FreeCoTaskMem(intPtr);

            return messageList;
        }

        public override void SendConversationReadAck(string conversationId, CallBack callback = null)
        {
            ChatAPINative.ChatManager_SendReadAckForConversation(client, conversationId,
                () =>
                {
                    try
                    {
                        callback?.Success();
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc) => callback?.Error(code, desc)
                );
        }

        public override Message SendMessage(Message message, CallBack callback = null)
        {
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            ChatAPINative.ChatManager_SendMessage(client,
                () =>
                {
                    try
                    {
                        callback?.Success();
                    }
                    catch(NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }
                    
                },
                (int code, string desc) => callback?.Error(code, desc), mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            //TODO: what Message to return from this SendMessage?
            return null;
        }

        public override void SendMessageReadAck(string messageId, CallBack callback = null)
        {
            ChatAPINative.ChatManager_SendReadAckForMessage(client, messageId,
                () =>
                {
                    try
                    {
                        callback?.Success();
                    }
                    catch (NullReferenceException nre)
                    {
                        Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
                    }

                },
                (int code, string desc) => callback?.Error(code, desc)
                );
        }

        public override bool UpdateMessage(Message message)
        {
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ChatManager_UpdateMessage(client, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }
    }
}