using System;
using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    public class Message
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public string MsgId = ((long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds)).ToString();

        /// <summary>
        /// 消息所属会话Id
        /// </summary>
        public string ConversationId = "";


        /// <summary>
        /// 消息发送方Id
        /// </summary>
        public string From = "";

        /// <summary>
        /// 消息接收方Id
        /// </summary>
        public string To = "";

        /// <summary>
        /// 消息撤回人
        /// </summary>
        public string RecallBy = "";

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
        public long LocalTime = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);

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
        public Dictionary<string, AttributeValue> Attributes;


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
                From = SDKClient.Instance.CurrentUsername,
                ConversationId = to,
            };

            return msg;
        }

        static public Message CreateTextSendMessage(string username, string content)
        {
            return CreateSendMessage(username, new MessageBody.TextBody(content));
        }

        static public Message CreateFileSendMessage(string username, string localPath, string displayName = "", long fileSize = 0)
        {
            return CreateSendMessage(username, new MessageBody.FileBody(localPath, displayName, fileSize: fileSize));
        }

        static public Message CreateImageSendMessage(string username, string localPath, string displayName = "", long fileSize = 0, bool original = false, double width = 0, double height = 0)
        {
            return CreateSendMessage(username, new MessageBody.ImageBody(localPath, displayName: displayName, fileSize: fileSize, original: original, width: width, height: height));
        }

        static public Message CreateVideoSendMessage(string username, string localPath, string displayName = "", string thumbnailLocalPath = "", long fileSize = 0, int duration = 0, double width = 0, double height = 0)
        {
            return CreateSendMessage(username, new MessageBody.VideoBody(localPath, displayName: displayName, thumbnailLocalPath: thumbnailLocalPath, fileSize: fileSize, duration: duration, width: width, height: height));
        }

        static public Message CreateVoiceSendMessage(string username, string localPath, string displayName = "", long fileSize = 0, int duration = 0)
        {
            return CreateSendMessage(username, new MessageBody.VoiceBody(localPath, displayName: displayName, fileSize: fileSize, duration: duration));
        }

        static public Message CreateLocationSendMessage(string username, double latitude, double longitude, string address = "", string buildingName = "")
        {
            return CreateSendMessage(username, new MessageBody.LocationBody(latitude: latitude, longitude: longitude, address: address, buildName: buildingName));
        }

        static public Message CreateCmdSendMessage(string username, string action, bool deliverOnlineOnly = false)
        {
            return CreateSendMessage(username, new MessageBody.CmdBody(action, deliverOnlineOnly: deliverOnlineOnly));
        }

        static public Message CreateCustomSendMessage(string username, string customEvent, Dictionary<string, string> customParams = null)
        {
            return CreateSendMessage(username, new MessageBody.CustomBody(customEvent, customParams: customParams));
        }

        /// <summary>
        /// 获取扩展属性的类型
        /// </summary>
        ///
        /// <returns>
        /// 返回枚举类型，参考SDKEnumerations.cs
        /// </returns>
        static public AttributeValueType GetAttributeValueType(AttributeValue value)
        {
            if (null == value) return AttributeValueType.NULLOBJ;

            return value.GetAttributeValueType();
        }

        /// <summary>
        /// 设置一个扩展属性;
        /// 
        /// attrMap为目标属性Dictionary;
        /// value为需要设置的类型数据;
        /// type为value的枚举类型，参考SDKEnumerations.cs
        /// </summary>
        static public void SetAttribute(Dictionary<string, AttributeValue> arriMap, string key, in object value, AttributeValueType type)
        {
            if(null == arriMap)
            {
                return;
            }
            AttributeValue attr = AttributeValue.Of(value, type);
            if (null != attr)
            {
                arriMap[key] = attr;
            }
        }

        /// <summary>
        /// 获取扩展属性的value;
        /// 
        /// value为需要解析的AttributeValue;
        /// 注意：需要优先判断found为true，才能使用返回值;
        /// </summary>
        /// 
        /// <returns>
        /// 对应类型数据T;
        /// found是否成功
        /// </returns>
        static public T GetAttributeValue<T>(AttributeValue value, out bool found)
        {
            if(null == value)
            {
                found = false;
                return default(T);
            }

            AttributeValueType type = value.GetAttributeValueType();
            object v = value.GetAttributeValue(type);
            if (null != v)
            {
                found = true;
                return (T)v;
            } else
            {
                found = false;
                return default(T);
            }
        }

        /// <summary>
        /// 获取扩展属性的value;
        /// 
        /// attrMap为目标属性Dictionary;
        /// key为属性名称;
        /// 注意：需要优先判断found为true，才能使用返回值
        /// </summary>
        /// 
        /// <returns>
        /// 对应类型数据;
        /// found是否成功
        /// </returns>
        static public T GetAttributeValue<T>(Dictionary<string, AttributeValue> arriMap, string key, out bool found)
        {
            if (null == arriMap)
            {
                found = false;
                return default(T);
            }

            if (!arriMap.ContainsKey(key))
            {
                found = false;
                return default(T);
            }

            AttributeValue value = arriMap[key];
            return GetAttributeValue<T>(value, out found);
        }

        internal Message(string jsonString)
        {
            if (jsonString != null)
            {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    From = jo["from"].Value;
                    To = jo["to"].Value;
                    HasReadAck = jo["hasReadAck"].AsBool;
                    HasDeliverAck = jo["hasDeliverAck"].AsBool;
                    LocalTime = long.Parse(jo["localTime"].Value);
                    ServerTime = long.Parse(jo["serverTime"].Value);
                    ConversationId = jo["conversationId"].Value;
                    MsgId = jo["msgId"].Value;
                    HasReadAck = jo["hasRead"].AsBool;
                    Status = MessageStatusFromInt(jo["status"].AsInt);
                    MessageType = MessageTypeFromInt(jo["chatType"].AsInt);
                    Direction = MessageDirectionFromString(jo["direction"].Value);
                    //Attributes = TransformTool.JsonStringToAttributes(jo["attributes"].Value);
                    Body = IMessageBody.Constructor(jo["body"].Value, jo["bodyType"].Value);
                }
            }
        }

        internal JSONObject ToJson()
        {
            JSONObject jo = new JSONObject();
            jo.Add("from", From);
            jo.Add("to", To);
            jo.Add("hasReadAck", HasReadAck);
            jo.Add("hasDeliverAck", HasDeliverAck);
            jo.Add("localTime", LocalTime.ToString());
            jo.Add("serverTime", ServerTime.ToString());
            jo.Add("conversationId", ConversationId);
            jo.Add("msgId", MsgId);
            jo.Add("hasRead", HasReadAck);
            jo.Add("status", MessageStatusToInt(Status));
            jo.Add("chatType", MessageTypeToInt(MessageType));
            jo.Add("direction", MessageDirectionToString(Direction));
            //jo.Add("attributes", TransformTool.JsonStringFromAttributes(Attributes));
            jo.Add("body", Body.ToJson().ToString());
            jo.Add("bodyType", Body.TypeString());
            return jo;
        }

        private MessageStatus MessageStatusFromInt(int intStatus)
        {
            MessageStatus ret = MessageStatus.CREATE;
            switch (intStatus)
            {
                case 0:
                    {
                        ret = MessageStatus.CREATE;
                        break;
                    }
                case 1:
                    {
                        ret = MessageStatus.PROGRESS;
                        break;
                    }
                case 2:
                    {
                        ret = MessageStatus.SUCCESS;
                        break;
                    }
                case 3:
                    {
                        ret = MessageStatus.FAIL;
                        break;
                    }
            }
            return ret;
        }

        private MessageType MessageTypeFromInt(int intType)
        {
            MessageType ret = MessageType.Chat;
            switch (intType)
            {
                case 0: ret = MessageType.Chat; break;
                case 1: ret = MessageType.Group; break;
                case 2: ret = MessageType.Room; break;
            }

            return ret;
        }

        private MessageDirection MessageDirectionFromString(string stringDirection)
        {
            if (stringDirection == "send")
            {
                return MessageDirection.SEND;
            }
            else
            {
                return MessageDirection.RECEIVE;
            }
        }

        private int MessageStatusToInt(MessageStatus status)
        {
            int ret = 0;
            switch (status)
            {
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

        private int MessageTypeToInt(MessageType type)
        {
            int ret = 0;
            switch (type)
            {
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

        private string MessageDirectionToString(MessageDirection direction)
        {
            if (direction == MessageDirection.SEND)
            {
                return "send";
            }
            else
            {
                return "recv";
            }
        }
    }
}
