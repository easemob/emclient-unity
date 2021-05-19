using System.Collections.Generic;

namespace ChatSDK
{
    public class Message
    {

        /// <summary>
        /// 消息Id
        /// </summary>
        public string MsgId = "";

        /// <summary>
        /// 消息所属会话Id
        /// </summary>
        public string ConversationId { get { return Direction == MessageDirection.SEND ? To : From; } }


        /// <summary>
        /// 消息发送方Id
        /// </summary>
        public string From = "";

        /// <summary>
        /// 消息接收方Id
        /// </summary>
        public string To = "";

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageBodyType ChatType;

        /// <summary>
        /// 消息方向
        /// </summary>
        public MessageDirection Direction;

        /// <summary>
        /// 消息本地时间
        /// </summary>
        public long LocalTime = 0;

        /// <summary>
        /// 消息服务器时间
        /// </summary>
        public long ServerTime = 0;

        /// <summary>
        /// 是否已送达
        /// </summary>
        public bool HasDeliverAck = false;

        /// <summary>
        /// 消息是否已读
        /// </summary>
        public bool HasReadAck = false;

        /// <summary>
        /// 消息体
        /// </summary>
        public IMessageBody Body;

        /// <summary>
        /// 消息扩展
        /// </summary>
        public Dictionary<string, string> attribute;


        public Message(IMessageBody body = null)
        {
            Body = body;
        }

        /// <summary>
        /// 创建接收的消息
        /// </summary>
        /// <returns>消息对象</returns>
        static public Message CreateReceiveMessage()
        {
            Message msg = new Message()
            {
                Direction = MessageDirection.RECEIVE,
                HasReadAck = false,
                From = SDKClient.Instance.CurrentUsername
            };

            return msg;
        }

        static public Message CreateSendMessage(string to, IMessageBody body, MessageDirection direction = MessageDirection.SEND, bool hasRead = true)
        {
            Message msg = new Message(body: body)
            {
                Direction = direction,
                HasReadAck = hasRead,
                To = to,
                From = SDKClient.Instance.CurrentUsername
            };

            return msg;
        }

        static public Message CreateTextSendMessage(string username, string content) {
            return CreateSendMessage(username, new MessageBody.TextBody(content));
        }

        static public Message CreateFileSendMessage(string username, string localPath, string displayName = "", long fileSize = 0)
        {
            return CreateSendMessage(username, new MessageBody.FileBody(localPath, displayName, fileSize: fileSize));
        }

        static public Message CreateImageSendMessage(string username, string localPath, string displayName = "", long fileSize = 0, bool original = false, double width = 0, double height = 0)
        {
            return CreateSendMessage(username, new MessageBody.ImageBody(localPath,displayName: displayName, fileSize: fileSize, original: original, width: width, height: height));
        }

        static public Message CreateVideoSendMessage(string username, string localPath, string displayName = "", string thumbnailLocalPath = "",  long fileSize = 0, int duration = 0, double width = 0, double height = 0)
        {
            return CreateSendMessage(username, new MessageBody.VideoBody(localPath, displayName: displayName, thumbnailLocalPath: thumbnailLocalPath, fileSize: fileSize, duration: duration, width: width, height: height));
        }

        static public Message CreateVoiceSendMessage(string username, string localPath, string displayName = "", long fileSize = 0, int duration = 0)
        {
            return CreateSendMessage(username, new MessageBody.VoiceBody(localPath, displayName: displayName, fileSize: fileSize, duration: duration));
        }

        static public Message CreateLocationSendMessage(string username, double latitude, double longitude, string address = "")
        {
            return CreateSendMessage(username, new MessageBody.LocationBody(latitude: latitude, longitude: longitude, address: address));
        }

        static public Message CreateCmdSendMessage(string username, string action, bool deliverOnlineOnly = false)
        {
            return CreateSendMessage(username, new MessageBody.CmdBody(action, deliverOnlineOnly: deliverOnlineOnly));
        }

        static public Message CreateCustomSendMessage(string username, string customEvent, Dictionary<string, string> customParams = null)
        {
            return CreateSendMessage(username, new MessageBody.CustomBody(customEvent, customParams: customParams));
        }
    }
}