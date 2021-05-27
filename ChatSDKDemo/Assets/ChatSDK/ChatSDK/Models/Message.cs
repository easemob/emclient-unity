using System.Collections.Generic;
using SimpleJSON;

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
        public MessageType MessageType;

        /// <summary>
        /// 消息方向
        /// </summary>
        public MessageDirection Direction;

        /// <summary>
        /// 消息的状态
        /// </summary>
        public MessageStatus Status;

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
        public Dictionary<string, string> Attribute;


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

        internal Message(string jsonString) {
            JSONObject jo = JSON.Parse(jsonString).AsObject;
        }

        internal string ToJsonString() {
            JSONObject jo = new JSONObject();
            jo.Add("from", From);
            jo.Add("to", To);
            jo.Add("hasReadAck", HasReadAck);
            jo.Add("hasDeliverAck", HasDeliverAck);
            jo.Add("localTime", LocalTime);
            jo.Add("serverTime", ServerTime);
            jo.Add("conversationId", ConversationId);
            jo.Add("msgId", MsgId);
            jo.Add("hasRead", HasReadAck);
            jo.Add("status", StatusToInt(Status));
            jo.Add("messageType", MessageTypeToInt(MessageType));
            jo.Add("direction", DirectionToString(Direction));
            jo.Add("attributes", TransformTool.JsonStringFromDictionary(Attribute));
            jo.Add("body", Body.ToJsonString());
            return jo.ToString();
        }


        private string DirectionToString(MessageDirection direction) {
            if (direction == MessageDirection.SEND)
            {
                return "send";
            }
            else {
                return "recv";
            }
        }

        private int MessageTypeToInt(MessageType type) {
            int ret = 0;
            switch (type) {
                case MessageType.Chat:
                    ret = 0;
                    break;
                case MessageType.Group:
                    ret = 1;
                    break;
                case MessageType.Room:
                    ret = 2;
                    break;
            }

            return ret;
        }

        private int StatusToInt(MessageStatus status) {
            int ret = 0;
            switch (status) {
                case MessageStatus.CREATE:
                    {
                        ret = 0;
                        break;
                    }
                case MessageStatus.PROGRESS:
                    {
                        ret = 1;
                        break;
                    }
                case MessageStatus.SUCCESS:
                    {
                        ret = 2;
                        break;
                    }
                case MessageStatus.FAIL:
                    {
                        ret = 3;
                        break;
                    }
            }
            return ret;
        }
    }
}