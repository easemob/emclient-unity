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
            chatManagerHub = new ChatManagerHub(Delegate);
            //registered listeners
            ChatAPINative.ChatManager_AddListener(client, chatManagerHub.OnMessagesReceived,
                chatManagerHub.OnCmdMessagesReceived, chatManagerHub.OnMessagesRead, chatManagerHub.OnMessagesDelivered,
                chatManagerHub.OnMessagesRecalled, chatManagerHub.OnReadAckForGroupMessageUpdated, chatManagerHub.OnGroupMessageRead,
                chatManagerHub.OnConversationsUpdate, chatManagerHub.OnConversationRead);
        }

        public override bool DeleteConversation(string conversationId, bool deleteMessages)
        {
            throw new System.NotImplementedException();
        }

        public override void DownloadAttachment(string messageId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DownloadThumbnail(string messageId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchHistoryMessages(string conversationId, ConversationType type, string startMessageId = null, int count = 20, ValueCallBack<CursorResult<Message>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override Conversation GetConversation(string conversationId, ConversationType type, bool createIfNeed = true)
        {
            throw new System.NotImplementedException();
        }

        public override void GetConversationsFromServer(ValueCallBack<List<Conversation>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override int GetUnreadMessageCount()
        {
            throw new System.NotImplementedException();
        }

        public override bool ImportMessages(List<Message> messages)
        {
            throw new System.NotImplementedException();
        }

        public override List<Conversation> LoadAllConversations()
        {
            throw new System.NotImplementedException();
        }

        public override Message LoadMessage(string messageId)
        {
            throw new System.NotImplementedException();
        }

        public override bool MarkAllConversationsAsRead()
        {
            throw new System.NotImplementedException();
        }

        public override void RecallMessage(string messageId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override Message ResendMessage(string messageId, ValueCallBack<Message> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            throw new System.NotImplementedException();
        }

        public override void SendConversationReadAck(string conversationId, CallBack callback = null)
        {
            throw new System.NotImplementedException();
        }

        public override Message SendMessage(Message message, CallBack callback = null)
        {
            MessageTO mto = null;
            switch(message.Body.Type) {
                case MessageBodyType.TXT:
                    mto = new TextMessageTO(message);
                    break;
                case MessageBodyType.LOCATION:
                    mto = new LocationMessageTO(message);
                    break;
                case MessageBodyType.CMD:
                    mto = new CmdMessageTO(message);
                    break;
            }
            IntPtr mtoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mto));
            Marshal.StructureToPtr(mto, mtoPtr, false);
            ChatAPINative.ChatManager_SendMessage(client, () => callback?.Success(),
                (int code, string desc) => callback?.Error(code, desc), mtoPtr, message.Body.Type);
            Marshal.FreeCoTaskMem(mtoPtr);
            //TODO: what Message to return from this SendMessage?
            return null;
        }

        public override void SendMessageReadAck(string messageId, CallBack callback = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateMessage(Message message, CallBack callback = null)
        {
            throw new System.NotImplementedException();
        }
    }
}