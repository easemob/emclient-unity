using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{
    internal class ChatManager_Mac : IChatManager
    {
        private IntPtr client;
        private ChatManagerHub chatManagerHub;

        //events
        public event Action OnSendMessageSuccess;
        public event OnError OnSendMessageError;
        
        internal ChatManager_Mac(IClient _client)
        {
            if (_client is Client_Mac clientMac)
            {
                client = clientMac.client;
            }
            chatManagerHub = new ChatManagerHub(CallbackManager.Instance().chatManagerDelegates);
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
            var mto = new MessageTransferObject(message);
            //clear previous linked events handler
            OnSendMessageSuccess = () => callback?.Success();
            OnSendMessageError = (int code, string desc) => callback?.Error(code, desc);
            ChatAPINative.ChatManager_SendMessage(client, OnSendMessageSuccess, OnSendMessageError, ref mto);
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