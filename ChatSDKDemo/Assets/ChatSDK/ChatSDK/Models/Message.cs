namespace ChatSDK
{
    public class Message
    {
        public IMessageBody Body;

        internal Message(IMessageBody body = null)
        {
            Body = body;
        }

        static public Message CreateReceiveMessage()
        {
            return new Message();
        }

        static public Message CreateSendMessage(IMessageBody body, string to, MessageDirection direction = MessageDirection.SEND, bool hasRead = true) {
            return new Message(body:body);
        }
    }
}