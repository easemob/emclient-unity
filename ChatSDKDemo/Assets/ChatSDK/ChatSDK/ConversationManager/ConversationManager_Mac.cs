using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//to-do: just for testing
using UnityEngine;

namespace ChatSDK
{
    public class ConversationManager_Mac : IConversationManager
    {
        private IntPtr client;
        internal ConversationManager_Mac(IClient _client)
        {
            if (_client is Client_Mac clientMac)
            {
                client = clientMac.client;
            }
        }


        public override bool AppendMessage(string conversationId, ConversationType converationType, Message message)
        {
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ConversationManager_AppendMessage(client, conversationId, converationType, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }

        public override bool DeleteAllMessages(string conversationId, ConversationType conversationType)
        {
            return ChatAPINative.ConversationManager_ClearAllMessages(client, conversationId, conversationType);
        }

        public override bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            return ChatAPINative.ConversationManager_RemoveMessage(client, conversationId, conversationType, messageId);
        }

        public override Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType)
        {
            //make a array of IntPtr(point to TOArray)
            TOArray toArray = new TOArray();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArray));
            Marshal.StructureToPtr(toArray, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.ConversationManager_ExtField(client, conversationId, conversationType, array, 1);

            //get data from IntPtr
            var returnTO = Marshal.PtrToStructure<TOArray>(array[0]);
            if(returnTO.Size > 0 && returnTO.Type == DataType.ListOfString)
            {
                if(returnTO.Size > 0)
                {
                //only one string return
                string ext = Marshal.PtrToStringAnsi(returnTO.Data[0]);
                if (ext.Length > 0)
                {
                    Marshal.FreeCoTaskMem(intPtr);
                    return TransformTool.JsonStringToDictionary(ext);
                }
                }
            }

            // return empty dictionary
            //Dictionary<string, string> ret = new Dictionary<string, string>();
            Marshal.FreeCoTaskMem(intPtr);
            ChatAPINative.ConversationManager_ReleaseStringList(array, 1);
            return null;
        }

        public override bool InsertMessage(string conversationId, ConversationType conversationType, Message message)
        {
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ConversationManager_InsertMessage(client, conversationId, conversationType, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }

        public override Message LastMessage(string conversationId, ConversationType conversationType)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.ConversationManager_LatestMessage(client, conversationId, conversationType, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);

            //cannot get any message
            if(returnTOArray.Size == 0)
            {
                Debug.Log($"Cannot find any last message for conversation id {conversationId}");
                Marshal.FreeCoTaskMem(intPtr);
                return null;
            }

            MessageTO mto = null;
            MessageBodyType type = (MessageBodyType)returnTOArray.Type[0];

            Debug.Log($"Found the last message for conversation id {conversationId}, msgtype: {type}");

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
            Message msg = mto.Unmarshall();
            Marshal.FreeCoTaskMem(intPtr);
            ChatAPINative.ConversationManager_ReleaseMessageList(array, 1);
            return msg;
        }

        public override Message LastReceivedMessage(string conversationId, ConversationType conversationType)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.ConversationManager_LatestMessageFromOthers(client, conversationId, conversationType, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);

            //cannot get any message
            if (returnTOArray.Size == 0)
            {
                Debug.Log($"Cannot find any last received message for conversation id {conversationId}");
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
            Message msg = mto.Unmarshall();
            Marshal.FreeCoTaskMem(intPtr);
            ChatAPINative.ConversationManager_ReleaseMessageList(array, 1);
            return msg;
        }

        public override Message LoadMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.ConversationManager_LoadMessage(client, conversationId, conversationType, messageId, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);
            //cannot get any message
            if (returnTOArray.Size == 0)
            {
                Debug.Log($"Cannot find any last message for conversation id {conversationId}");
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
            Message msg = mto.Unmarshall();
            Marshal.FreeCoTaskMem(intPtr);
            ChatAPINative.ConversationManager_ReleaseMessageList(array, 1);
            return msg;
        }

        public override void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId = "", int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            ChatAPINative.ConversationManager_LoadMessages(client, conversationId, conversationType, startMessageId, count, direction,

                 (IntPtr[] toResult, DataType dType, int size) => {
                    if (dType == DataType.ListOfString && size == 1)
                    {
                        var toArrayDiff = Marshal.PtrToStructure<TOArrayDiff>(toResult[0]);

                        int msgSize = toArrayDiff.Size;
                        Debug.Log($"LoadMessages callback with dType={dType}, size={msgSize}.");

                        if(msgSize <= 0)
                        {
                             var emptyList = new List<Message>();
                             callback?.OnSuccessValue(emptyList);
                             return;
                         }

                        MessageTO[] messages = new MessageTO[msgSize];
                        MessageTO mto = null;
                        for (int i = 0; i < msgSize; i++)
                        {
                            switch ((MessageBodyType)toArrayDiff.Type[i])
                            {
                                //to-do: need to other types?
                                case MessageBodyType.TXT:
                                    mto = new TextMessageTO();
                                    Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                    messages[i] = mto;
                                    break;
                                case MessageBodyType.LOCATION:
                                    mto = new LocationMessageTO();
                                    Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                    messages[i] = mto;
                                    break;
                                case MessageBodyType.CMD:
                                    mto = new CmdMessageTO();
                                    Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                    messages[i] = mto;
                                    break;
                            }
                        }
                        var result = MessageTO.ConvertToMessageList(messages, msgSize);
                        callback?.OnSuccessValue(result);
                    }
                    else
                    {
                        Debug.LogError("Incorrect parameters returned.");
                    }
                },
                (int code, string desc) => callback?.Error(code, desc));
        }

        public override void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords = "", string sender = "", long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            ChatAPINative.LoadMessagesWithKeyword(client, conversationId, conversationType, keywords, sender, timestamp, count, direction,

                 (IntPtr[] toResult, DataType dType, int size) => {
                     if (dType == DataType.ListOfString && size == 1)
                     {
                         var toArrayDiff = Marshal.PtrToStructure<TOArrayDiff>(toResult[0]);

                         int msgSize = toArrayDiff.Size;
                         Debug.Log($"LoadMessagesWithKeyword callback with dType={dType}, size={msgSize}.");

                         if (msgSize <= 0)
                         {
                             var emptyList = new List<Message>();
                             callback?.OnSuccessValue(emptyList);
                             return;
                         }

                         MessageTO[] messages = new MessageTO[msgSize];
                         MessageTO mto = null;
                         for (int i = 0; i < msgSize; i++)
                         {
                             switch ((MessageBodyType)toArrayDiff.Type[i])
                             {
                                 //to-do: need to other types?
                                 case MessageBodyType.TXT:
                                     mto = new TextMessageTO();
                                     Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                     messages[i] = mto;
                                     break;
                                 case MessageBodyType.LOCATION:
                                     mto = new LocationMessageTO();
                                     Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                     messages[i] = mto;
                                     break;
                                 case MessageBodyType.CMD:
                                     mto = new CmdMessageTO();
                                     Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                     messages[i] = mto;
                                     break;
                             }
                         }
                         var result = MessageTO.ConvertToMessageList(messages, msgSize);
                         callback?.OnSuccessValue(result);
                     }
                     else
                     {
                         Debug.LogError("Incorrect parameters returned.");
                     }
                 },
                (int code, string desc) => callback?.Error(code, desc));
        }

        public override void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            ChatAPINative.LoadMessagesWithMsgType(client, conversationId, conversationType, bodyType, timestamp, count, sender, direction,

                 (IntPtr[] toResult, DataType dType, int size) => {
                     if (dType == DataType.ListOfString && size == 1)
                     {
                         var toArrayDiff = Marshal.PtrToStructure<TOArrayDiff>(toResult[0]);

                         int msgSize = toArrayDiff.Size;
                         Debug.Log($"LoadMessagesWithKeyword callback with dType={dType}, size={msgSize}.");

                         if (msgSize <= 0)
                         {
                             var emptyList = new List<Message>();
                             callback?.OnSuccessValue(emptyList);
                             return;
                         }

                         MessageTO[] messages = new MessageTO[msgSize];
                         MessageTO mto = null;
                         for (int i = 0; i < msgSize; i++)
                         {
                             switch ((MessageBodyType)toArrayDiff.Type[i])
                             {
                                 //to-do: need to other types?
                                 case MessageBodyType.TXT:
                                     mto = new TextMessageTO();
                                     Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                     messages[i] = mto;
                                     break;
                                 case MessageBodyType.LOCATION:
                                     mto = new LocationMessageTO();
                                     Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                     messages[i] = mto;
                                     break;
                                 case MessageBodyType.CMD:
                                     mto = new CmdMessageTO();
                                     Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                     messages[i] = mto;
                                     break;
                             }
                         }
                         var result = MessageTO.ConvertToMessageList(messages, msgSize);
                         callback?.OnSuccessValue(result);
                     }
                     else
                     {
                         Debug.LogError("Incorrect parameters returned.");
                     }
                 },
                (int code, string desc) => callback?.Error(code, desc));
        }

        public override void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null)
        {
            ChatAPINative.LoadMessagesWithTime(client, conversationId, conversationType, startTime, endTime, count,

                 (IntPtr[] toResult, DataType dType, int size) => {
                     if (dType == DataType.ListOfString && size == 1)
                     {
                         var toArrayDiff = Marshal.PtrToStructure<TOArrayDiff>(toResult[0]);

                         int msgSize = toArrayDiff.Size;
                         Debug.Log($"LoadMessagesWithKeyword callback with dType={dType}, size={msgSize}.");

                         if (msgSize <= 0)
                         {
                             var emptyList = new List<Message>();
                             callback?.OnSuccessValue(emptyList);
                             return;
                         }

                         MessageTO[] messages = new MessageTO[msgSize];
                         MessageTO mto = null;
                         for (int i = 0; i < msgSize; i++)
                         {
                             switch ((MessageBodyType)toArrayDiff.Type[i])
                             {
                                 //to-do: need to other types?
                                 case MessageBodyType.TXT:
                                     mto = new TextMessageTO();
                                     Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                     messages[i] = mto;
                                     break;
                                 case MessageBodyType.LOCATION:
                                     mto = new LocationMessageTO();
                                     Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                     messages[i] = mto;
                                     break;
                                 case MessageBodyType.CMD:
                                     mto = new CmdMessageTO();
                                     Marshal.PtrToStructure(toArrayDiff.Data[i], mto);
                                     messages[i] = mto;
                                     break;
                             }
                         }
                         var result = MessageTO.ConvertToMessageList(messages, msgSize);
                         callback?.OnSuccessValue(result);
                     }
                     else
                     {
                         Debug.LogError("Incorrect parameters returned.");
                     }
                 },
                (int code, string desc) => callback?.Error(code, desc)); 
        }

        public override void MarkAllMessageAsRead(string conversationId, ConversationType conversationType)
        {
            ChatAPINative.ConversationManager_MarkAllMessagesAsRead(client, conversationId, conversationType);
        }

        public override void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId)
        {
            ChatAPINative.ConversationManager_MarkMessageAsRead(client, conversationId, conversationType, messageId);
        }

        public override void SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext)
        {
            string extStr = TransformTool.JsonStringFromDictionary(ext);
            ChatAPINative.ConversationManager_SetExtField(client, conversationId, conversationType, extStr);
        }

        public override int UnReadCount(string conversationId, ConversationType conversationType)
        {
            return ChatAPINative.ConversationManager_UnreadMessagesCount(client, conversationId, conversationType);
        }

        public override bool UpdateMessage(string conversationId, ConversationType conversationType, Message message)
        {
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ConversationManager_UpdateMessage(client, conversationId, conversationType, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }
    }
}
