using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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

        public override bool DeleteAllMessages(string conversationId, ConversationType converationType)
        {
            return ChatAPINative.ConversationManager_ClearAllMessages(client, conversationId, converationType);
        }

        public override bool DeleteMessage(string conversationId, ConversationType converationType, string messageId)
        {
            return ChatAPINative.ConversationManager_RemoveMessage(client, conversationId, converationType, messageId);
        }

        public override Dictionary<string, string> GetExt(string conversationId, ConversationType converationType)
        {
            //make a array of IntPtr(point to TOArray)
            TOArray toArray = new TOArray();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArray));
            Marshal.StructureToPtr(toArray, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.ConversationManager_ExtField(client, conversationId, converationType, array, 1);

            //get data from IntPtr
            var returnTO = Marshal.PtrToStructure<TOArray>(array[0]);
            if(returnTO.Size > 0 && returnTO.Type == DataType.ListOfString)
            {
                //only one string return
                string ext = Marshal.PtrToStringAnsi(returnTO.Data[0]);
                if (ext.Length > 0)
                {
                    Marshal.FreeCoTaskMem(intPtr);
                    return TransformTool.JsonStringToDictionary(ext);
                }

            }

            // return empty dictionary
            Dictionary<string, string> ret = new Dictionary<string, string>();
            Marshal.FreeCoTaskMem(intPtr);
            return ret;
        }

        public override bool InsertMessage(string conversationId, ConversationType converationType, Message message)
        {
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ConversationManager_InsertMessage(client, conversationId, converationType, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }

        public override Message LastMessage(string conversationId, ConversationType converationType)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.ConversationManager_LatestMessage(client, conversationId, converationType, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);

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
            return mto.Unmarshall();
        }

        public override Message LastReceivedMessage(string conversationId, ConversationType converationType)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.ConversationManager_LatestMessageFromOthers(client, conversationId, converationType, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);

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
            return mto.Unmarshall();
        }

        public override Message LoadMessage(string conversationId, ConversationType converationType, string messageId)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.ConversationManager_LoadMessage(client, conversationId, converationType, messageId, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);

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
            return mto.Unmarshall();
        }

        public override List<Message> LoadMessages(string conversationId, ConversationType converationType, string startMessageId, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.ConversationManager_LoadMessages(client, conversationId, converationType, startMessageId, count, direction, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);
            MessageTO[] messages = new MessageTO[returnTOArray.Size];
            for (int i=0; i<returnTOArray.Size; i++)
            {
                MessageTO mto = null;
                MessageBodyType type = (MessageBodyType)returnTOArray.Type[i];
                switch (type)
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
            return MessageTO.ConvertToMessageList(messages, returnTOArray.Size);
        }

        public override List<Message> LoadMessagesWithKeyword(string conversationId, ConversationType converationType, string keywords, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.LoadMessagesWithKeyword(client, conversationId, converationType, keywords, sender, timestamp, count, direction, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);
            MessageTO[] messages = new MessageTO[returnTOArray.Size];
            for (int i = 0; i < returnTOArray.Size; i++)
            {
                MessageTO mto = null;
                MessageBodyType type = (MessageBodyType)returnTOArray.Type[i];
                switch (type)
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
            return MessageTO.ConvertToMessageList(messages, returnTOArray.Size);
        }

        public override List<Message> LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.LoadMessagesWithMsgType(client, conversationId, conversationType, bodyType, timestamp, count, sender, direction, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);
            MessageTO[] messages = new MessageTO[returnTOArray.Size];
            for (int i = 0; i < returnTOArray.Size; i++)
            {
                MessageTO mto = null;
                MessageBodyType type = (MessageBodyType)returnTOArray.Type[i];
                switch (type)
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
            return MessageTO.ConvertToMessageList(messages, returnTOArray.Size);
        }

        public override List<Message> LoadMessagesWithTime(string conversationId, ConversationType converationType, long startTime, long endTime, int count = 20)
        {
            //make a array of IntPtr(point to TOArray)
            TOArrayDiff toArrayDiff = new TOArrayDiff();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArrayDiff));
            Marshal.StructureToPtr(toArrayDiff, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.LoadMessagesWithTime(client, conversationId, converationType, startTime, endTime, count, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArrayDiff>(array[0]);
            MessageTO[] messages = new MessageTO[returnTOArray.Size];
            for (int i = 0; i < returnTOArray.Size; i++)
            {
                MessageTO mto = null;
                MessageBodyType type = (MessageBodyType)returnTOArray.Type[i];
                switch (type)
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
            return MessageTO.ConvertToMessageList(messages, returnTOArray.Size);
        }

        public override void MarkAllMessageAsRead(string conversationId, ConversationType converationType)
        {
            ChatAPINative.ConversationManager_MarkAllMessagesAsRead(client, conversationId, converationType);
        }

        public override void MarkMessageAsRead(string conversationId, ConversationType converationType, string messageId)
        {
            ChatAPINative.ConversationManager_MarkMessageAsRead(client, conversationId, converationType, messageId);
        }

        public override void SetExt(string conversationId, ConversationType converationType, Dictionary<string, string> ext)
        {
            string extStr = TransformTool.JsonStringFromDictionary(ext);
            ChatAPINative.ConversationManager_SetExtField(client, conversationId, converationType, extStr);
        }

        public override int UnReadCount(string conversationId, ConversationType converationType)
        {
            return ChatAPINative.ConversationManager_UnreadMessagesCount(client, conversationId, converationType);
        }

        public override bool UpdateMessage(string conversationId, ConversationType converationType, Message message)
        {
            MessageTO mto = MessageTO.FromMessage(message);
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            bool ret = ChatAPINative.ConversationManager_UpdateMessage(client, conversationId, converationType, mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            return ret;
        }
    }
}
