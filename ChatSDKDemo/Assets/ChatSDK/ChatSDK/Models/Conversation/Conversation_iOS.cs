using System.Collections.Generic;

namespace ChatSDK
{
    public class Conversation_iOS : Conversation
    {
        public Conversation_iOS()
        {
        }

        public override string Id
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override ConversationType Type
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override Message LastMessage
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override Message LastReceivedMessage
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override Dictionary<string, string> Ext
        {
            get { throw new System.NotImplementedException(); }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override int UnReadCount
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override bool AppendMessage(Message message)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteAllMessages()
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteMessage(string messageId)
        {
            throw new System.NotImplementedException();
        }

        public override bool InsertMessage(Message message)
        {
            throw new System.NotImplementedException();
        }

        public override Message LoadMessage(string messageId)
        {
            throw new System.NotImplementedException();
        }

        public override List<Message> LoadMessages(string startMessageId, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            throw new System.NotImplementedException();
        }

        public override List<Message> LoadMessagesWithKeyword(string keywords, string sender, int timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            throw new System.NotImplementedException();
        }

        public override List<Message> LoadMessagesWithMsgType(MessageBodyType type, string sender, int timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            throw new System.NotImplementedException();
        }

        public override List<Message> LoadMessagesWithTime(int startTime, int endTime, int count = 20)
        {
            throw new System.NotImplementedException();
        }

        public override void MarkAllMessageAsRead()
        {
            throw new System.NotImplementedException();
        }

        public override void MarkMessageAsRead(string messageId)
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdateMessage(Message message)
        {
            throw new System.NotImplementedException();
        }

        internal override Conversation ConversationFromJson(string jsonString)
        {
            throw new System.NotImplementedException();
        }

        internal override string ToJson()
        {
            throw new System.NotImplementedException();
        }
    }

}