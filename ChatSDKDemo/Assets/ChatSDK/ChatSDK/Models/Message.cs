using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;

namespace ChatSDK
{
    //MessageTransferObject: Data object to be transferred across managed/unmanaged border
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MessageTransferObject
    {
        public string MsgId;
        public string ConversationId;
        public string From;
        public string To;
        public MessageType Type;
        public MessageDirection Direction;
        public MessageStatus Status;
        [MarshalAs(UnmanagedType.U1)]
        public bool HasDeliverAck;
        [MarshalAs(UnmanagedType.U1)]
        public bool HasReadAck;
        public MessageBodyType BodyType;
        /*[MarshalAs(UnmanagedType.AsAny)]
        public IMessageBody Body; //only 1 body processed
        public string[] AttributesKeys;
        public AttributeValue[] AttributesValues;
        public int AttributesSize;*/
        public long LocalTime;
        public long ServerTime;

        public MessageTransferObject(in Message message) {
            (MsgId, ConversationId, From, To, Type, Direction, Status, HasDeliverAck, HasReadAck, BodyType, LocalTime, ServerTime)
                = (message.MsgId, message.ConversationId, message.From, message.To, message.MessageType, message.Direction, message.Status,
                    message.HasDeliverAck, message.HasReadAck, message.Body.Type,
                    message.LocalTime, message.ServerTime);
        }

        public static List<Message> ConvertToMessageList(in MessageTransferObject[] _messages, int size)
        {
            List<Message> messages = new List<Message>();
            for(int i=0; i<size; i++)
            {
                messages.Add(_messages[i].Unmarshall());
            }
            return messages;
        }

        private static void SplitMessageAttributes(in Message message, out string[] Keys, out AttributeValue[] Values)
        {
            int count = message.Attributes.Count;
            Keys = new string[count];
            Values = new AttributeValue[count];
            var keys = message.Attributes.Keys;
            int i = 0;
            foreach (var key in keys)
            {
                Keys[i] = key;
                if(!message.Attributes.TryGetValue(key, out Values[i]))
                    Values[i] = new AttributeValue();
                i++;
            }
        }

        private static Dictionary<string, AttributeValue> MergeMessageAttributes(in string[] keys, in AttributeValue[] values, in int size)
        {
            var result = new Dictionary<string, AttributeValue>();
            for(int i=0; i<size; i++)
            {
                result.Add(keys[i], values[i]);
            }
            return result;
        }

        internal Message Unmarshall()
        {
            return new Message()
            {
                MsgId = MsgId,
                ConversationId = ConversationId,
                From = From,
                To = To,
                MessageType = Type,
                Direction = Direction,
                Status = Status,
                LocalTime = LocalTime,
                ServerTime = ServerTime,
                HasDeliverAck = HasDeliverAck,
                HasReadAck = HasReadAck,
            };
        }
    }

    // AttributeValue Union
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    public struct AttributeValue
    {
        enum AttributeValueType : byte
        {
            BOOL = 0,
            CHAR,
            UCHAR,
            SHORT,
            USHORT,
            INT32,
            UINT32,
            INT64,
            UINT64,
            FLOAT,
            DOUBLE,
            STRING,
            STRVECTOR,
            JSONSTRING,
            NULLOBJ
        }

        [FieldOffset(0)]
        AttributeValueType VType;
        [FieldOffset(1), MarshalAs(UnmanagedType.U1)]
        bool BoolV;
        [FieldOffset(1)]
        sbyte CharV;
        [FieldOffset(1)]
        char UCharV;
        [FieldOffset(1)]
        short ShortV;
        [FieldOffset(1)]
        ushort UShortV;
        [FieldOffset(1)]
        int Int32V;
        [FieldOffset(1)]
        uint UInt32V;
        [FieldOffset(1)]
        long Int64V;
        [FieldOffset(1)]
        ulong UInt64V;
        [FieldOffset(1)]
        float FloatV;
        [FieldOffset(1)]
        double DoubleV;
        [FieldOffset(1)]
        string StringV;

        /*[FieldOffset(1)]
        string[] StringVec;
        [FieldOffset(1)]
        string JsonStringV;
        [FieldOffset(1)]
        IntPtr NullV;*/

        public static AttributeValue Of(in bool value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.BOOL,
                BoolV = value
            };
            return result;
        }

        public static AttributeValue Of(in sbyte value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.CHAR,
                CharV = value
            };
            return result;
        }

        public static AttributeValue Of(in char value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.UCHAR,
                UCharV = value
            };
            return result;
        }

        public static AttributeValue Of(in short value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.SHORT,
                ShortV = value
            };
            return result;
        }

        public static AttributeValue Of(in ushort value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.USHORT,
                UShortV = value
            };
            return result;
        }

        public static AttributeValue Of(in int value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.INT32,
                Int32V = value
            };
            return result;
        }

        public static AttributeValue Of(in uint value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.UINT32,
                UInt32V = value
            };
            return result;
        }

        public static AttributeValue Of(in long value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.INT64,
                Int64V = value
            };
            return result;
        }

        public static AttributeValue Of(in ulong value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.UINT64,
                UInt64V = value
            };
            return result;
        }

        public static AttributeValue Of(in float value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.FLOAT,
                FloatV = value
            };
            return result;
        }

        public static AttributeValue Of(in double value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.DOUBLE,
                DoubleV = value
            };
            return result;
        }

        public static AttributeValue Of(in string value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.STRING,
                StringV = value
            };
            return result;
        }

        public string ToJsonString()
        {
            JSONObject jo = new JSONObject();            
            string _type;
            string value;
            switch (VType)
            {
                case AttributeValueType.BOOL:
                    _type = "b";
                    value = BoolV.ToString();
                    break;
                case AttributeValueType.CHAR:
                    _type = "c";
                    value = CharV.ToString();
                    break;
                case AttributeValueType.UCHAR:
                    _type = "uc";
                    value = UCharV.ToString();
                    break;
                case AttributeValueType.SHORT:
                    _type = "s";
                    value = ShortV.ToString();
                    break;
                case AttributeValueType.USHORT:
                    _type = "us";
                    value = UShortV.ToString();
                    break;
                case AttributeValueType.INT32:
                    _type = "i1";
                    value = Int32V.ToString();
                    break;
                case AttributeValueType.UINT32:
                    _type = "ui1";
                    value = UInt32V.ToString();
                    break;
                case AttributeValueType.INT64:
                    _type = "i2";
                    value = Int64V.ToString();
                    break;
                case AttributeValueType.UINT64:
                    _type = "ui2";
                    value = UInt64V.ToString();
                    break;
                case AttributeValueType.FLOAT:
                    _type = "f";
                    value = FloatV.ToString();
                    break;
                case AttributeValueType.DOUBLE:
                    _type = "d";
                    value = DoubleV.ToString();
                    break;
                case AttributeValueType.STRING:
                    _type = "str";
                    value = StringV;
                    break;
                //TODO: add STRVECTOR, JSONSTRING, NULLOBJ
                default:
                    throw new NotImplementedException();               
            }
            jo["type"] = _type;
            jo["value"] = value;
            return jo.ToString();
        }

        internal static AttributeValue FromJsonString(string jsonString)
        {
            if (jsonString == null) return new AttributeValue();
            AttributeValue result = new AttributeValue();
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            string typeString = jo["type"];
            string value = jo["value"].Value;
            switch (typeString) {
                case "b":
                    result.VType = AttributeValueType.BOOL;
                    result.BoolV = Boolean.Parse(value);
                    break;
                case "c":
                    result.VType = AttributeValueType.CHAR;
                    result.CharV = (sbyte)Char.Parse(value);
                    break;
                case "uc":
                    result.VType = AttributeValueType.UCHAR;
                    result.UCharV = Char.Parse(value);
                    break;
                case "s":
                    result.VType = AttributeValueType.SHORT;
                    result.ShortV = short.Parse(value);
                    break;
                case "us":
                    result.VType = AttributeValueType.USHORT;
                    result.UShortV = ushort.Parse(value);
                    break;
                case "i1":
                    result.VType = AttributeValueType.INT32;
                    result.Int32V = int.Parse(value);
                    break;
                case "ui1":
                    result.VType = AttributeValueType.UINT32;
                    result.UInt32V = uint.Parse(value);
                    break;
                case "i2":
                    result.VType = AttributeValueType.INT64;
                    result.Int64V = long.Parse(value);
                    break;
                case "ui2":
                    result.VType = AttributeValueType.UINT64;
                    result.UInt64V = ulong.Parse(value);
                    break;
                case "f":
                    result.VType = AttributeValueType.FLOAT;
                    result.FloatV = float.Parse(value);
                    break;
                case "d":
                    result.VType = AttributeValueType.DOUBLE;
                    result.DoubleV = double.Parse(value);
                    break;
                case "str":
                    result.VType = AttributeValueType.STRING;
                    result.StringV = value;
                    break;
                default: throw new NotImplementedException();
            }
            return result;
        }
    }

    public class Message
    {

        /// <summary>
        /// 消息Id
        /// </summary>
        public string MsgId = "";

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
            JSONNode jn = JSON.Parse(jsonString);
            if (!jn.IsNull && jn.IsObject) {
                JSONObject jo = jn.AsObject;
                From = jo["from"].Value;
                To = jo["to"].Value;
                HasReadAck = jo["hasReadAck"].AsBool;
                HasDeliverAck = jo["hasDeliverAck"].AsBool;
                LocalTime = jo["localTime"].AsInt;
                ServerTime = jo["serverTime"].AsInt;
                ConversationId = jo["conversationId"].Value;
                MsgId = jo["msgId"].Value;
                HasReadAck = jo["hasRead"].AsBool;
                Status = MessageStatusFromInt(jo["status"].AsInt);
                MessageType = MessageTypeFromInt(jo["messageType"].AsInt);
                Direction = MessageDirectionFromString(jo["direction"].Value);
                //Attributes = TransformTool.JsonStringToDictionary(jo["attributes"].Value);
                Body = BodyFromJsonString(jo["body"].Value, jo["bodyType"].Value);
            }
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
            jo.Add("status", MessageStatusToInt(Status));
            jo.Add("messageType", MessageTypeToInt(MessageType));
            jo.Add("direction", MessageDirectionToString(Direction));
            jo.Add("attributes", TransformTool.JsonStringFromAttributes(Attributes));
            jo.Add("body", Body.ToJsonString());
            return jo.ToString();
        }

        private string MessageDirectionToString(MessageDirection direction) {
            if (direction == MessageDirection.SEND)
            {
                return "send";
            }
            else {
                return "recv";
            }
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

        private MessageType MessageTypeFromInt(int intType) {
            MessageType ret = MessageType.Chat;
            switch (intType)
            {
                case 0: ret = MessageType.Chat; break;
                case 1: ret = MessageType.Group; break;
                case 2: ret = MessageType.Room; break;
            }

            return ret;
        }

        private int MessageStatusToInt(MessageStatus status) {
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

        private MessageStatus MessageStatusFromInt(int intStatus) {
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

        private IMessageBody BodyFromJsonString(string jsonString, string bodyType) {
            IMessageBody body = null;
            if (bodyType == "txt")
            {
                body = new MessageBody.TextBody(jsonString, null);
            }
            else if (bodyType == "img")
            {
                body = new MessageBody.ImageBody(jsonString, null);
            }
            else if (bodyType == "loc")
            {
                body = new MessageBody.LocationBody(jsonString, null);
            }
            else if (bodyType == "cmd")
            {
                body = new MessageBody.CmdBody(jsonString, null);
            }
            else if (bodyType == "custom")
            {
                body = new MessageBody.CustomBody(jsonString, null);
            }
            else if (bodyType == "file")
            {
                body = new MessageBody.FileBody(jsonString, null);
            }
            else if (bodyType == "video")
            {
                body = new MessageBody.VideoBody(jsonString, null);
            }
            else if (bodyType == "voice")
            {
                body = new MessageBody.VoiceBody(jsonString, null);
            }
            return body;
        }
    }
}
