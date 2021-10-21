using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return false;
            }
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ConversationManager_AppendMessage(client, conversationId, converationType, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }

        public override bool DeleteAllMessages(string conversationId, ConversationType conversationType)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return false;
            }
            return ChatAPINative.ConversationManager_ClearAllMessages(client, conversationId, conversationType);
        }

        public override bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return false;
            }
            return ChatAPINative.ConversationManager_RemoveMessage(client, conversationId, conversationType, messageId);
        }

        public override Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return new Dictionary<string, string>(); // return empty dict
            }
            string ext = null;
            ChatAPINative.ConversationManager_ExtField(client, conversationId, conversationType,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.String == dType)
                    {
                        if (1 == dSize)
                            ext = Marshal.PtrToStringAnsi(data[0]);
                        else
                            Debug.Log($"Empty ext for conversation id={conversationId}");
                    }
                    else
                    {
                        Debug.LogError($"String type expected, but return type is {dType}.");
                    }
                }
                );

            if (null != ext && ext.Length > 0)
                return TransformTool.JsonStringToDictionary(ext);
            else
                return new Dictionary<string, string>(); // return empty dict
        }

        public override bool InsertMessage(string conversationId, ConversationType conversationType, Message message)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return false;
            }
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ConversationManager_InsertMessage(client, conversationId, conversationType, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }

        public override Message LastMessage(string conversationId, ConversationType conversationType)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return null;
            }
            Message msg = null;
            ChatAPINative.ConversationManager_LatestMessage(client, conversationId, conversationType,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.ListOfMessage == dType)
                    {
                        if (1 == dSize)
                        {
                            TOItem item = Marshal.PtrToStructure<TOItem>(data[0]);
                            var msgTO = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                            msg = msgTO.Unmarshall();
                        }
                        else if (-1 == dSize)
                            Debug.LogError("Mandatory parameter is null!");
                        else
                            Debug.Log($"Cannot find the last message with conversationid={conversationId}");
                    }
                    else
                        Debug.LogError($"ListOfMessage type expected, but return type is {dType}.");
                }
                );
            return msg;
        }

        public override Message LastReceivedMessage(string conversationId, ConversationType conversationType)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return null;
            }
            Message msg = null;
            ChatAPINative.ConversationManager_LatestMessageFromOthers(client, conversationId, conversationType,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.ListOfMessage == dType)
                    {
                        if (1 == dSize)
                        {
                            TOItem item = Marshal.PtrToStructure<TOItem>(data[0]);
                            var msgTO = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                            msg = msgTO.Unmarshall();
                        }
                        else if (-1 == dSize)
                            Debug.LogError("Mandatory parameter is null!");
                        else
                            Debug.Log($"Cannot find the last message with conversationid={conversationId}");
                    }
                    else
                        Debug.LogError($"ListOfMessage type expected, but return type is {dType}.");
                }
                );
            return msg;
        }

        public override Message LoadMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return null;
            }
            Message msg = null;
            ChatAPINative.ConversationManager_LoadMessage(client, conversationId, conversationType, messageId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.ListOfMessage == dType)
                    {
                        if (1 == dSize)
                        {
                            TOItem item = Marshal.PtrToStructure<TOItem>(data[0]);
                            var msgTO = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                            msg = msgTO.Unmarshall();
                        }
                        else if (-1 == dSize)
                            Debug.LogError("Mandatory parameter is null!");
                        else
                            Debug.Log($"Failed to load the message in conversation={conversationId}");
                    }
                    else
                        Debug.LogError($"ListOfMessage type expected, but return type is {dType}.");
                }
                );
            return msg;
        }

        public override void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId = "", int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            ChatAPINative.ConversationManager_LoadMessages(client, callbackId, conversationId, conversationType, startMessageId, count, direction,

                 (IntPtr[] data, DataType dType, int size, int cbId) => {
                    if (dType == DataType.ListOfMessage)
                    {
                        var list = new List<Message>();
                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                 TOItem item = Marshal.PtrToStructure<TOItem>(data[i]);
                                 MessageTO mto = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                                 list.Add(mto.Unmarshall());
                            }
                        }
                        else
                        {
                             Debug.Log($"Cannot load any message for coversationid={conversationId}");
                        }
                        ChatCallbackObject.ValueCallBackOnSuccess<List<Message>>(cbId, list);
                     }
                    else
                    {
                        Debug.LogError("Incorrect parameters returned.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<Message>>(cbId, code, desc);
                });
        }

        public override void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords = "", string sender = "", long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            ChatAPINative.ConversationManager_LoadMessagesWithKeyword(client, callbackId, conversationId, conversationType, keywords, sender, timestamp, count, direction,

                 (IntPtr[] data, DataType dType, int size, int cbId) => {
                     if (dType == DataType.ListOfMessage)
                     {
                         var list = new List<Message>();
                         if (size > 0)
                         {
                             for (int i = 0; i < size; i++)
                             {
                                 TOItem item = Marshal.PtrToStructure<TOItem>(data[i]);
                                 MessageTO mto = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                                 list.Add(mto.Unmarshall());
                             }
                         }
                         else
                         {
                             Debug.Log($"Cannot load any message for coversationid={conversationId}");
                         }
                         ChatCallbackObject.ValueCallBackOnSuccess<List<Message>>(cbId, list);
                     }
                     else
                     {
                         Debug.LogError("Incorrect parameters returned.");
                     }
                 },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<Message>>(cbId, code, desc);
                });
        }

        public override void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            ChatAPINative.ConversationManager_LoadMessagesWithMsgType(client, callbackId, conversationId, conversationType, bodyType, timestamp, count, sender, direction,

                 (IntPtr[] data, DataType dType, int size, int cbId) => {
                     if (dType == DataType.ListOfMessage)
                     {
                         var list = new List<Message>();
                         if (size > 0)
                         {
                             for (int i = 0; i < size; i++)
                             {
                                 TOItem item = Marshal.PtrToStructure<TOItem>(data[i]);
                                 MessageTO mto = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                                 list.Add(mto.Unmarshall());
                             }
                         }
                         else
                         {
                             Debug.Log($"Cannot load any message for coversationid={conversationId}");
                         }
                         ChatCallbackObject.ValueCallBackOnSuccess<List<Message>>(cbId, list);
                     }
                     else
                     {
                         Debug.LogError("Incorrect parameters returned.");
                     }
                 },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<Message>>(cbId, code, desc);
                });
        }

        public override void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;

            ChatAPINative.ConversationManager_LoadMessagesWithTime(client, callbackId, conversationId, conversationType, startTime, endTime, count,

                 (IntPtr[] data, DataType dType, int size, int cbId) => {
                     if (dType == DataType.ListOfMessage)
                     {
                         var list = new List<Message>();
                         if (size > 0)
                         {
                             for (int i = 0; i < size; i++)
                             {
                                 TOItem item = Marshal.PtrToStructure<TOItem>(data[i]);
                                 MessageTO mto = MessageTO.FromIntPtr(item.Data, (MessageBodyType)item.Type);
                                 list.Add(mto.Unmarshall());
                             }
                         }
                         else
                         {
                             Debug.Log($"Cannot load any message for coversationid={conversationId}");
                         }
                         ChatCallbackObject.ValueCallBackOnSuccess<List<Message>>(cbId, list);
                     }
                     else
                     {
                         Debug.LogError("Incorrect parameters returned.");
                     }
                 },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<Message>>(cbId, code, desc);
                });
        }

        public override void MarkAllMessageAsRead(string conversationId, ConversationType conversationType)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.ConversationManager_MarkAllMessagesAsRead(client, conversationId, conversationType);
        }

        public override void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId)
        {
            if (null == conversationId || null == messageId || 0 == conversationId.Length || 0 == messageId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.ConversationManager_MarkMessageAsRead(client, conversationId, conversationType, messageId);
        }

        public override void SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext)
        {
            if (null == conversationId || 0 == conversationId.Length || null == ext || 0 == ext.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            string extStr = TransformTool.JsonStringFromDictionary(ext);
            ChatAPINative.ConversationManager_SetExtField(client, conversationId, conversationType, extStr);
        }

        public override int UnReadCount(string conversationId, ConversationType conversationType)
        {
            if (null == conversationId || 0 == conversationId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return -1;
            }
            return ChatAPINative.ConversationManager_UnreadMessagesCount(client, conversationId, conversationType);
        }

        public override bool UpdateMessage(string conversationId, ConversationType conversationType, Message message)
        {
            if (null == conversationId || 0 == conversationId.Length || null == message)
            {
                Debug.LogError("Mandatory parameter is null!");
                return false;
            }
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ConversationManager_UpdateMessage(client, conversationId, conversationType, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }
    }
}
