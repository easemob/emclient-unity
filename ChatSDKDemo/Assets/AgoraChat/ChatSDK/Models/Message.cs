using System;
using System.Collections.Generic;
using SimpleJSON;

namespace AgoraChat
{
    /**
     * \~chinese
     * 消息类，用于定义一条要发送或接收的消息。
     * 
     * \~english
     * The message class that defines a message that is to be sent or received.
     * 
     */
    public class Message
    {
        /**
         * \~chinese
         * 消息的 ID。
         * 
         * \~english
         * The message ID.
         */
        public string MsgId = ((long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds)).ToString();

        /**
         * \~chinese
         * 消息所属会话的 ID。
         *
         * \~english
         * The ID of the conversation to which the message belongs.
         * 
         */
        public string ConversationId = "";

        /**
         * \~chinese
         * 消息发送者的 ID。
         * 
         * \~english
         * The user ID of the message sender.
         * 
         */
        public string From = "";

        /**
	     * \~chinese
         * 消息接收者的用户 ID 或群组 ID。
         * 
         * \~english
         * The user ID of the message recipient or the group ID.
         * 
         */
        public string To = "";


        /**
	     * \~chinese
         * 消息类型。
         * 
         * - `Chat`：单聊消息；
         * - `Group`：群聊消息；
         * - `Room`：聊天室消息；
         * 
         * \~english
         * The message type.
         * 
         * - `Chat`: The one-to-one chat message.
         * - `Group`: The group chat message.
         * - `Room`: The chat room message.
         */
        public MessageType MessageType;

        /**
	     * \~chinese
	     * 消息方向。
         * 
         * - `SEND`：该消息由当前用户发出；
         * - `RECEIVE`：该消息由当前用户接收；
         * 
         * 详见 {@link Direct}。
	     * 
	     * \~english
	     * The message direction, that is, whether the message is received or sent. 
         *
         * - `SEND`: This message is sent from the local client.
         * - `RECEIVE`: The message is received by the local client.
         *
         * See {@link Direct}.
         *  
	     */
        public MessageDirection Direction;

        /**
         * \~chinese
         * 消息的状态，包含以下状态：
         *
         * - `CREATE`：消息已创建；
         * - `PROGRESS`：消息正在发送；
         * - `SUCCESS`：消息成功发送；
         * - `FAIL`：消息发送失败。
         * 
         * \~english
         * The message status, which can be one of the following:
         *
         * - `CREATE`：The message is created.
         * - `PROGRESS`：The message is being delivered.
         * - `SUCCESS`：The message is successfully delivered.
         * - `FAIL`：The message fails to be delivered.
         */
        public MessageStatus Status;

        /**
         * \~chinese
         * 消息的本地创建 Unix 时间戳，单位为毫秒。
         * 
         * \~english
         * The local Unix timestamp for creating the message. The unit is millisecond.
         * 
         */
        public long LocalTime = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);

        /**
         * \~chinese
         * 消息的服务器接收的 Unix 时间戳，单位为毫秒。
         * 
         * \~english
         * The Unix timestamp when the message is received by the server. The unit is millisecond.
         * 
         */
        public long ServerTime = 0;

        /**
         * \~chinese
         * 消息是否已送达对方。
         *
         * - `true`: 已送达；
         * - `false`: 未送达。
         * 
         * \~english
         * Whether the message is delivered.
         *
         * - `true`: Yes.
         * - `false`: No.
         * 
         */
        public bool HasDeliverAck = false;

        /**
         * \~chinese
         * 消息是否已读。
         *
         * - `true`: 已读；
         * - `false`: 未读。
         *
         * \~english
         * Whether the message is read.
         *
         * -`true`: Yes. 
         * -`false`: No.
         * 
         */
        public bool HasReadAck = false;

        /**
         * \~chinese
         * 设置消息是否需要群组已读回执。
         * @param need - `true`：需要已读回执；
         * - `false`：不需要。
         *
         * \~english
         * Sets whether read receipts are required for group messages.
         * @param need - `true`: Yes.
         * - `false`: No.
         */
        public bool IsNeedGroupAck = false;

        /**
         * \~chinese
         * 消息是否已读。
         *
         * @note
         * 如要要设置消息已读，建议在会话中，使用 {@link IConversation#MarkAllMessageAsRead()}。
         *
         * \~english
         * Check the message is read or not.
         *
         * @note
         * If you want to set message read, we recommend you to use {@link Conversation#MarkAllMessagesAsRead()} in a conversation.
         *
		*/
        public bool IsRead = false;

        /**
         * \~chinese
         * 是否为在线消息。
         * @return  是否在线。
         * - `true`：是。
         * - `false`：是。
         *
         * \~english
         * Whether the message is an online message.
         * @return Whether the message is an online message.
         * - `true`: Yes.
         * - `false`: No.
         */
        public bool MessageOnlineState = false;
		
        /**
         * \~chinese
         * 获取群组消息回执数。
         * @return  群组消息回执数。
         *
         * \~english
         * Gets the group ack count.
         * @return The group ack count.
         */		
        public int GroupAckCount {
            get {
                if (IsNeedGroupAck)
                {
                    return SDKClient.Instance.MessageManager.GetGroupAckCount(MsgId);
                }
                else
                {
                    return 0;
                }
            }
        }		


        /**
         * \~chinese
         * 消息体。
         * 
         * \~english
         * The message body.
         * 
         */
        public IMessageBody Body;

        /**
         * \~chinese
         * 消息扩展。
         * 
         * \~english
         * The message extension.
         * 
         */
        public Dictionary<string, AttributeValue> Attributes;
        /**
         * \~chinese
         * 获取 Reaction 列表。
         * @return  Reaction 列表。
		 *
         * \~english
         * Gets the list of Reaction.
		 *
         * @return The list of Reactions.
         */
        public List<MessageReaction> ReactionList {
            get {
                return SDKClient.Instance.MessageManager.GetReactionList(MsgId);
            }
        }

        /**
         * \~chinese
         * 设置及获取是否是 Thread 消息。
		 *
         * \~english
         * Sets and gets whether the message is in a thread.
         */
        public bool IsThread = false;
		
		/**
         * \~chinese
         * 获取子区概览信息。
         *
         * \~english
         * Gets the Chat Thread overview information,
         *
         */
        public ChatThread ChatThread {
            get {
                return SDKClient.Instance.MessageManager.GetChatThread(MsgId);
            }
        }
		






        public Message(IMessageBody body = null)
        {
            Body = body;
        }

        /**
         * \~chinese
         * 创建一条接收的消息。
         * 
         * \~english
         * Creates a received message instance.
         */
        static public Message CreateReceiveMessage()
        {
            if (null == SDKClient.Instance.CurrentUsername || SDKClient.Instance.CurrentUsername.Length == 0)
            {
                // invalid input, return null
                return null;
            }

            Message msg = new Message()
            {
                Direction = MessageDirection.RECEIVE,
                HasReadAck = false,
                From = SDKClient.Instance.CurrentUsername
            };

            return msg;
        }

        /**
	     * \~chinese
         * 创建一条发送的消息。
         *
         * @param to        消息接收方 ID。
         * @param body      消息体。
         * @param direction 消息方向，设置为 `SEND`。
         *                  - `SEND`: 发送该消息。
         *                  - `RECEIVE`: 接收该消息。
         * @param hasRead   是否需要已读回执。
         * 
         * \~english
         * Creates a message instance for sending.
         *
         * @param to        The user ID of the message recipient.
         * @param body      The message body.
         * @param direction The message direction, that is, whether the message is received or sent. This parameter is set to `SEND`.
         *                  - `SEND`: This message is sent from the local client.
         *                  - `RECEIVE`: The message is received by the local client.
         * @param hasRead   Whether a read receipt is required.
         *                  - `true`: Yes.
         *                  - `false`: No.
         */
        static public Message CreateSendMessage(string to, IMessageBody body, MessageDirection direction = MessageDirection.SEND, bool hasRead = true)
        {
            if (null == SDKClient.Instance.CurrentUsername || SDKClient.Instance.CurrentUsername.Length == 0 || null == to || to.Length == 0)
            {
                // invalid input, return null
                return null;
            }

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

        /**
         * \~chinese
         * 创建一条文本发送消息。
         *
         * @param username 消息接收者的用户 ID 或群组 ID。
         * @param content 文本内容。
         * 
         * \~english
         * Creates a text message for sending.
         *
         * @param username The user ID of the message recipient or a group ID.
         * @param content The text content.
         */
        static public Message CreateTextSendMessage(string username, string content)
        {
            return CreateSendMessage(username, new MessageBody.TextBody(content));
        }

        /**
         * \~chinese
         * 创建一条文件发送消息。
         *
         * @param username          消息接收者的用户 ID 或群组 ID。
         * @param localPath         文件的本地路径。
         * @param displayName       文件的显示名称。
         * @param fileSize          文件大小，单位为字节。
         * 
         * \~english
         * Creates a file message for sending.
         * @param username          The user ID of the message recipient or a group ID.
         * @param localPath         The local path of the file.
         * @param displayName       The display name of the file.
         * @param fileSize          The file size in bytes.
         */
        static public Message CreateFileSendMessage(string username, string localPath, string displayName = "", long fileSize = 0)
        {
            return CreateSendMessage(username, new MessageBody.FileBody(localPath, displayName, fileSize: fileSize));
        }

        /**
         * \~chinese
         * 创建一条图片发送消息。
         *
         * @param username              消息接收者的用户 ID 或群组 ID。
         * @param localPath             图片的本地路径。
         * @param displayName           图片的显示名称。
         * @param fileSize              图片大小，单位为字节。
         * @param original              是否发送原图。
         *                              - `true`: 发送原图。
         *                              - (默认） `false`: 发送缩略图。若图片超过 100 KB，SDK 会先压缩图片，然后发送缩略图。
         *                              对于 Windows、Unity Mac 或 Unity Windows，SDK 暂不支持压缩功能，只支持原图发送。
         * 
         * @param width                 图片宽度，单位为像素。
         * @param heigh                 图片高度，单位为像素。
         * 
         * \~english
         * Creates an image message for sending.
         *
         * @param username              The user ID of the message recipient or a group ID.
         * @param localPath             The local path of the image.
         * @param displayName           The display name of the image.
         * @param fileSize              The image size in bytes.
         * @param original              Whether to send the original image.
         *                              - `true`: Yes. 
         *                              - (Default) `false`: No. The thumbnail is sent. For an image greater than 100 KB, the SDK will compress it before sending its thumbnail.
         *                              For the Windows, Unity Mac, or Unity Windows, the SDK can only send the original image as the compression is not supported. 
         * @param width                 The image width in pixels.
         * @param heigh                 The image height in pixels.
         * 
         *
         */
        static public Message CreateImageSendMessage(string username, string localPath, string displayName = "", long fileSize = 0, bool original = false, double width = 0, double height = 0)
        {
            return CreateSendMessage(username, new MessageBody.ImageBody(localPath, displayName: displayName, fileSize: fileSize, original: original, width: width, height: height));
        }

        /**
         * \~chinese
         * 创建一条视频发送消息。
         *
         * @param username              消息接收者的用户 ID 或群组 ID。
         * @param localPath             视频文件的 URI。
         * @param displayName           视频文件的显示名称。
         * @param thumbnailLocalPath    缩略图的本地路径。
         * @param fileSize              视频文件的大小，单位为字节。
         * @param duration              视频时间长度，单位为秒。
         * @param width                 视频宽度，单位为像素。
         * @param heigh                 视频高度，单位为像素。
         *
         * \~english
         * Creates a video message for sending.
         *
         * @param username              The user ID of the message recipient or a group ID.
         * @param localPath             The URI of the video file.
         * @param displayName           The display name of the video file.
         * @param thumbnailLocalPath    The local path of the thumbnail of the video file.
         * @param fileSize              The size of the video file, in bytes. 
         * @param duration              The video duration in seconds.
         * @param width                 The video width in pixels.
         * @param heigh                 The video height in pixels.
         * 
         */
        static public Message CreateVideoSendMessage(string username, string localPath, string displayName = "", string thumbnailLocalPath = "", long fileSize = 0, int duration = 0, double width = 0, double height = 0)
        {
            return CreateSendMessage(username, new MessageBody.VideoBody(localPath, displayName: displayName, thumbnailLocalPath: thumbnailLocalPath, fileSize: fileSize, duration: duration, width: width, height: height));
        }

        /**
         * \~chinese
         * 创建一条语音发送消息。
         *
         * @param username      消息接收者的用户 ID 或群组 ID。
         * @param localPath     语音文件的本地路径。
         * @param displayName   语音文件的显示名称。
         * @param fileSize      语音文件的大小，单位为字节。
         * @param duration      语音时间长度，单位为秒。
         * 
         * 
         * \~english
         * Creates a voice message for sending.
         *
         * @param username      The user ID of the message recipient or a group ID.
         * @param localPath     The local path of the voice file.
         * @param displayName   The display name of f the voice file.
         * @param fileSize      The size of the voice file, in bytes.
         * @param duration      The voice duration in seconds.
         *
         */
        static public Message CreateVoiceSendMessage(string username, string localPath, string displayName = "", long fileSize = 0, int duration = 0)
        {
            return CreateSendMessage(username, new MessageBody.VoiceBody(localPath, displayName: displayName, fileSize: fileSize, duration: duration));
        }

        /**
         * \~chinese
         * 创建一条位置发送消息。
         *
         * @param username      消息接收者的用户 ID 或群组 ID。
         * @param latitude      纬度。
         * @param longitude     经度。
         * @param address       位置详情。
         * @param buildingName  建筑物名称。
         *  
         * 
         * \~english
         * Creates a location message for sending.
         *
         * @param username      The user ID of the message recipient or a group ID.
         * @param latitude      The latitude.
         * @param longitude     The longitude.
         * @param address       The location details.
         * @param buildingName  The building name.
         * 
         */
        static public Message CreateLocationSendMessage(string username, double latitude, double longitude, string address = "", string buildingName = "")
        {
            return CreateSendMessage(username, new MessageBody.LocationBody(latitude: latitude, longitude: longitude, address: address, buildName: buildingName));
        }

        /**
         * \~chinese
         * 创建一条命令发送消息。
         *
         * @param username              消息接收者的用户 ID 或群组 ID。
         * @param action                命令内容。
         *                              - `true`：只投在线用户。
         *                              - （默认） `false`：不管用户是否在线均投递。
         *
         * 
         * \~english
         * Creates a command message for sending.
         *
         * @param username              The user ID of the message recipient or a group ID.
         * @param action                The command action.
         * @param deliverOnlineOnly     Whether this command message is delivered only to the online users.
         *                              - `true`: Yes.
         *                              - (Default) `false`: No. The command message is delivered to users, regardless of their online or offline status.
         * 
         */
        static public Message CreateCmdSendMessage(string username, string action, bool deliverOnlineOnly = false)
        {
            return CreateSendMessage(username, new MessageBody.CmdBody(action, deliverOnlineOnly: deliverOnlineOnly));
        }

        /**
         * \~chinese
         * 创建一条自定义发送消息。
         *
         * @param username          消息接收者的用户 ID 或群组 ID。
         * @param customEvent       自定义事件。
         * @param customParams      自定义参数字典。
         * 
         * 
         * \~english
         * Creates a custom message for sending.
         *
         * @param username          The user ID of the message recipient or a group ID.
         * @param customEvent       The custom event.
         * @param customParams      The dictionary of custom parameters.
         * 
         */
        static public Message CreateCustomSendMessage(string username, string customEvent, Dictionary<string, string> customParams = null)
        {
            return CreateSendMessage(username, new MessageBody.CustomBody(customEvent, customParams: customParams));
        }

        /**
         * \~chinese
         * 获取扩展属性的类型。
         *
         * @param value          扩展属性实例。
         * 
         * \~english
         * Gets the type of the message extension attribute.
         * @param value          The extension attribute instance.
         */
        static public AttributeValueType GetAttributeValueType(AttributeValue value)
        {
            if (null == value) return AttributeValueType.NULLOBJ;

            return value.GetAttributeValueType();
        }

        /**
         * \~chinese
         * 设置单个扩展属性。
         *
         * @param arriMap        要新增扩展属性的字典。
         * @param key            新增扩展属性的关键字。
         * @param type           扩展属性的类型。
         * @param value          扩展属性的值。
         * 
         * \~english
         * Sets an extension attribute.
         *
         * @param arriMap        The dictionary to which the new extension attribute will be added.
         * @param key            The keyword of the extension attribute.
         * @param type           The type of the extension attribute.
         * @param value          The value of the extension attribute.
         */
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

        /**
         * \~chinese
         * 获取单个扩展属性的泛型类型 T 的数据。
         *
         * @param value          扩展属性的值。
         * @param found          扩展属性的值中是否包含泛型类型 T 的数据。
         *
         * @return               返回泛型类型 T 的数据。
         *                       - `found` 为 `true` 时，返回泛型类型 T 的数据
         *                       - `found` 为 `false` 时，返回 `null`。
         * 
         * \~english
         * Gets the data of the generic <T> type of an extension attribute.
         *
         * @param value          The value of the extension attribute.
         * @param found          Whether the data of the generic <T> type is included in the value of the extension attribute.
         *
         * @return               The data of the generic <T> type.
         *                       - If `found` is `true`, the data of the generic <T> type is returned.
         *                       - If `found` is `false`, `null` is returned.
         */
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

        /**
         * \~chinese
         * 从扩展属性字典中获取单个扩展属性的泛型类型 T 的数据。 
         *
         * @param arriMap        扩展属性字典。
         * @param key            扩展属性在字典中的关键字。
         * @param found          扩展属性的值中是否包含泛型类型 T 的数据。
         *
         * @return               泛型类型 T 的数据。
         *                       - `found` 为 `true` 时，返回泛型类型 T 的数据
         *                       - `found` 为 `false` 时，返回 `null`。
         * 
         * \~english
         * Gets the data of the generic <T> type of an extension attribute from the extension attribute dictionary.
         *
         * @param arriMap        The dictionary which contains attributes.
         * @param key            The keyword in the dictionary for the extension attribute.
         * @param found          Whether the data of the generic <T> type is included in the value of the extension attribute.
         *
         * @return               The data of the generic <T> type.
         *                       - If `found` is `true`, the data of the generic <T> type is returned.
         *                       - If `found` is `false`, `null` is returned.
         *
         */
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
                    Status = MessageStatusFromInt(jo["status"].AsInt);
                    MessageType = MessageTypeFromInt(jo["chatType"].AsInt);
                    Direction = MessageDirectionFromString(jo["direction"].Value);
                    Attributes = TransformTool.JsonStringToAttributes(jo["attributes"].ToString());
                    Body = IMessageBody.Constructor(jo["body"].Value, jo["bodyType"].Value);
                    IsNeedGroupAck = jo["isNeedGroupAck"].AsBool;
                    IsRead = jo["isRead"].AsBool;
                    MessageOnlineState = jo["messageOnlineState"].AsBool;
                    IsThread = jo["isThread"].AsBool;
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
            string strAttr = TransformTool.JsonStringFromAttributes(Attributes);
            if (strAttr != null) {
                jo.Add("attributes", strAttr);
            }
            jo.Add("body", Body.ToJson().ToString());
            jo.Add("bodyType", Body.TypeString());

            jo.Add("isNeedGroupAck", IsNeedGroupAck);
            jo.Add("isRead", IsRead);
            jo.Add("messageOnlineState", MessageOnlineState);

            jo.Add("isThread", IsThread);
            
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
