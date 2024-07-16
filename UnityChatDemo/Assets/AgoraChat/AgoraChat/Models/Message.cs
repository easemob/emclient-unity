using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    [Preserve]
    public class Message : BaseModel
    {
        /**
         * \~chinese
         * 消息的 ID。
         * 
         * \~english
         * The message ID.
         */
        public string MsgId = ((long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds) + Tools.GetRandom()).ToString();

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
         * 消息发送者的用户 ID。
         * 
         * \~english
         * The user ID of the message sender.
         * 
         */
        public string From = "";

        /**
	     * \~chinese
         * 消息接收方，可以是：
         * - 单聊：用户 ID；
         * - 群组：群组 ID；
         * - 聊天室：聊天室 ID。
         * 
         * \~english
         * The message recipient.
         * - For a one-to-one chat, it is the user ID of the peer user.
         * - For a group chat, it is the group ID.
         * - For a chat room, it is the chat room ID.
         * 
         */
        public string To = "";


        /**
	     * \~chinese
         * 聊天类型。
         * 
         * - `Chat`：单聊；
         * - `Group`：群聊；
         * - `Room`：聊天室；
         * 
         * \~english
         * The chat type.
         * 
         * - `Chat`: One-to-one chat.
         * - `Group`: Group chat.
         * - `Room`: Chat room.
         */
        public MessageType MessageType;

        private RoomMessagePriority Priority = RoomMessagePriority.Normal;

        /**
	     * \~chinese
         * 设置聊天室消息优先级。
         *
         * \~english
         * Sets the priority of the chat room message.
         */
        public void SetRoomMessagePriority(RoomMessagePriority priority)
        {
            Priority = priority;
        }

        /**
        * \~chinese
        * 消息是否只投递给在线用户：
        * - `true`：只有消息接收方在线时才能投递成功。若接收方离线，则消息会被丢弃。
        * - （默认）`false`：如果用户在线，则直接投递；如果用户离线，消息会在用户上线时投递。
        *
        * \~english
        * Whether the message is delivered only when the recipient(s) is/are online:
        * - `true`：The message is delivered only when the recipient(s) is/are online. If the recipient is offline, the message is discarded.
        * - (Default) `false`：The message is delivered when the recipient(s) is/are online. If the recipient(s) is/are offline, the message will not be delivered to them until they get online.
        *
        */
        public bool DeliverOnlineOnly = false;

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
         * 群组消息是否需要已读回执。
         * - `true`：需要；
         * - `false`：不需要。
         *
         * \~english
         * Whether read receipts are required for group messages.
         * - `true`: Yes.
         * - `false`: No.
         */
        public bool IsNeedGroupAck = false;

        /**
         * \~chinese
         * 消息是否已读。
         *
         * @note
         * 如果要设置消息已读，建议在会话中，使用 {@link IConversation#MarkAllMessageAsRead()}。
         *
         * \~english
         * Whether the message is read or not.
         *
         * @note
         * To set the message as read, you are advised to use {@link Conversation#MarkAllMessagesAsRead()} in a conversation.
         *
        */
        public bool IsRead = false;

        /**
         * \~chinese
         * 是否为在线消息。
         * @return  是否在线。
         * - `true`：是。
         * - `false`：否。
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
         * 获取群组消息的已读回执数。
         *
         * @return  群组消息的已读回执数。
         *
         * \~english
         * Gets the number of read receipts for a group message.
         *
         * @return The number of read receipts for a group message.
         */
        public int GroupAckCount
        {
            get
            {
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
         * 获取当前消息的置顶信息。
         *
         * @return  置顶信息。
         *
         * \~english
         * Gets the message pinning information.
         *
         * @return The message pinning information.
         */
        public PinnedInfo PinnedInfo
        {
            get
            {
                return SDKClient.Instance.MessageManager.GetPinnedInfo(MsgId);
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
         *
         * @return  Reaction 列表。
         *
         * \~english
         * Gets the list of Reactions.
         *
         * @return The list of Reactions.
         */
        public List<MessageReaction> ReactionList()
        {
            return SDKClient.Instance.MessageManager.GetReactionList(MsgId);
        }

        /**
         * \~chinese
         * 定向消息的接收方。
         *
         * 该属性仅对群组聊天和聊天室中的消息有效。
         * \~english
         * The recipient list of a targeted message.
         *
         * This property is used only for messages in groups and chat rooms.
         */
        public List<string> ReceiverList
        {
            internal get { return _receiverList; }
            set { _receiverList = value; }
        }

        private List<string> _receiverList;

        /**
         * \~chinese
         * 是否是 Thread 消息：
         * - `true`：是；
         * - `false`：否。
         * 
         * 该属性为只读属性。
         *
         * \~english
         * Whether the message is in a message thread:
         * - `true`: Yes.
         * - `false`: No.
         * 
         * This property is read only.
         */
        public bool IsThread = false;

        /**
         * \~chinese
         * 是否是聊天室全局广播消息：
         * - `true`：是；
         * - `false`：否。
         *
         * 该属性为只读属性。
         *
         * \~english
         * Whether it is a global broadcast message for all chat rooms in an app:
         * - `true`: Yes.
         * - `false`: No.
         * 
         * This property is read only.
         */
        public bool Broadcast = false;

        /**
         * \~chinese
         * 内容是否被替换：
         * - `true`：是；
         * - `false`：否。
         *
         * 该属性为只读属性。
         *
         * \~english
         * Whether the content of message is replaced:
         * - `true`: Yes.
         * - `false`: No.
         * 
         * This property is read only.
         */
        public bool IsContentReplaced = false;

        /**
         * \~chinese
         * 获取子区概览信息。
         *
         * 子区概览信息仅在创建子区后携带。
         *
         * \~english
         * Gets the overview of the message thread.
         *
         * The overview of the message thread exists only after you create a message thread.
         */
        public ChatThread ChatThread
        {
            get
            {
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
            string user_name = SDKClient.Instance.CurrentUsername;

            if (string.IsNullOrEmpty(user_name))
            {
                user_name = "";
            }

            Message msg = new Message()
            {
                Direction = MessageDirection.RECEIVE,
                HasReadAck = false,
                From = user_name
            };

            return msg;
        }

        /**
         * \~chinese
         * 创建一条发送的消息。
         *
         * @param to        消息接收方。
         *                  - 单聊：对端用户 ID；
         *                  - 群组聊天：群组 ID；
         *                  - 聊天室：聊天室 ID；
         *                  - 子区：子区 ID。
         * @param body      消息体。
         * @param direction 消息方向，设置为 `SEND`。
         *                  - `SEND`: 发送该消息。
         *                  - `RECEIVE`: 接收该消息。
         * @param hasRead   是否需要已读回执。
         * 
         * \~english
         * Creates a message instance for sending.
         *
         * @param to        The message recipient:
         *                  - One-to-one chat: The user ID of the peer user.
         *                  - Group chat: Group ID.
         *                  - Chat room: Chat room ID.
         *                  - Thread: Thread ID.
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
            string user_name = SDKClient.Instance.CurrentUsername;

            if (string.IsNullOrEmpty(user_name))
            {
                user_name = "";
            }

            if (string.IsNullOrEmpty(to))
            {
                to = "";
            }

            Message msg = new Message(body: body)
            {
                Direction = direction,
                HasReadAck = hasRead,
                To = to,
                From = user_name,
                ConversationId = to,
                IsRead = true,
            };

            return msg;
        }

        /**
         * \~chinese
         * 创建一条文本发送消息。
         *
         * @param userId  消息接收方。
         *                  - 单聊：对端用户 ID；
         *                  - 群组聊天：群组 ID；
         *                  - 聊天室：聊天室 ID；
         *                  - 子区：子区 ID。
         * @param content 文本内容。
         * 
         * \~english
         * Creates a text message for sending.
         *
         * @param userId  The message recipient:
         *                  - One-to-one chat: The user ID of the peer user.
         *                  - Group chat: Group ID.
         *                  - Chat room: Chat room ID.
         *                  - Thread: Thread ID.
         * @param content The text content.
         */
        static public Message CreateTextSendMessage(string userId, string content)
        {
            return CreateSendMessage(userId, new MessageBody.TextBody(content));
        }

        /**
         * \~chinese
         * 创建一条文件发送消息。
         *
         * @param userId            消息接收方。
         *                          - 单聊：对端用户 ID；
         *                          - 群组聊天：群组 ID；
         *                          - 聊天室：聊天室 ID；
         *                          - 子区：子区 ID。
         * @param localPath         文件的本地路径。
         * @param displayName       文件的显示名称。
         * @param fileSize          文件大小，单位为字节。
         * 
         * \~english
         * Creates a file message for sending.
         * @param userId            The message recipient:
         *                          - One-to-one chat: The user ID of the peer user.
         *                          - Group chat: Group ID.
         *                          - Chat room: Chat room ID.
         *                          - Thread: Thread ID.
         * @param localPath         The local path of the file.
         * @param displayName       The display name of the file.
         * @param fileSize          The file size in bytes.
         */
        static public Message CreateFileSendMessage(string userId, string localPath, string displayName = "", long fileSize = 0)
        {
            return CreateSendMessage(userId, new MessageBody.FileBody(localPath, displayName, fileSize: fileSize));
        }

        /**
         * \~chinese
         * 创建一条图片发送消息。
         *
         * @param userId                消息接收方。
         *                              - 单聊：对端用户 ID；
         *                              - 群组聊天：群组 ID；
         *                              - 聊天室：聊天室 ID；
         *                              - 子区：子区 ID。
         * @param localPath             图片的本地路径。
         * @param displayName           图片的显示名称。
         * @param fileSize              图片大小，单位为字节。
         * @param original              是否发送原图。
         *                              - `true`: 发送原图。
         *                              - (默认） `false`: 发送缩略图。若图片超过 100 KB，SDK 会先压缩图片，然后发送缩略图。
         * @param width                 图片宽度，单位为像素。
         * @param heigh                 图片高度，单位为像素。
         * 
         * \~english
         * Creates an image message for sending.
         *
         * @param userId                The message recipient:
         *                              - One-to-one chat: The user ID of the peer user.
         *                              - Group chat: Group ID.
         *                              - Chat room: Chat room ID.
         *                              - Thread: Thread ID.
         * @param localPath             The local path of the image.
         * @param displayName           The display name of the image.
         * @param fileSize              The image size in bytes.
         * @param original              Whether to send the original image.
         *                              - `true`: Yes. 
         *                              - (Default) `false`: No. The thumbnail is sent. For an image greater than 100 KB, the SDK will compress it before sending its thumbnail.
         * @param width                 The image width in pixels.
         * @param heigh                 The image height in pixels.
         * 
         *
         */
        static public Message CreateImageSendMessage(string userId, string localPath, string displayName = "", long fileSize = 0, bool original = false, double width = 0, double height = 0)
        {
            return CreateSendMessage(userId, new MessageBody.ImageBody(localPath, displayName: displayName, fileSize: fileSize, original: original, width: width, height: height));
        }

        /**
         * \~chinese
         * 创建一条视频发送消息。
         *
         * @param userId                消息接收方。
         *                              - 单聊：对端用户 ID；
         *                              - 群组聊天：群组 ID；
         *                              - 聊天室：聊天室 ID；
         *                              - 子区：子区 ID。
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
         * @param userId                The message recipient:
         *                              - One-to-one chat: The user ID of the peer user.
         *                              - Group chat: Group ID.
         *                              - Chat room: Chat room ID.
         *                              - Thread: Thread ID.
         * @param localPath             The URI of the video file.
         * @param displayName           The display name of the video file.
         * @param thumbnailLocalPath    The local path of the thumbnail of the video file.
         * @param fileSize              The size of the video file, in bytes. 
         * @param duration              The video duration in seconds.
         * @param width                 The video width in pixels.
         * @param heigh                 The video height in pixels.
         * 
         */
        static public Message CreateVideoSendMessage(string userId, string localPath, string displayName = "", string thumbnailLocalPath = "", long fileSize = 0, int duration = 0, double width = 0, double height = 0)
        {
            return CreateSendMessage(userId, new MessageBody.VideoBody(localPath, displayName: displayName, thumbnailLocalPath: thumbnailLocalPath, fileSize: fileSize, duration: duration, width: width, height: height));
        }

        /**
         * \~chinese
         * 创建一条语音发送消息。
         *
         * @param userId        消息接收方。
         *                      - 单聊：对端用户 ID；
         *                      - 群组聊天：群组 ID；
         *                      - 聊天室：聊天室 ID；
         *                      - 子区：子区 ID。
         * @param localPath     语音文件的本地路径。
         * @param displayName   语音文件的显示名称。
         * @param fileSize      语音文件的大小，单位为字节。
         * @param duration      语音时间长度，单位为秒。
         * 
         * 
         * \~english
         * Creates a voice message for sending.
         *
         * @param userId        The message recipient:
         *                      - One-to-one chat: The user ID of the peer user.
         *                      - Group chat: Group ID.
         *                      - Chat room: Chat room ID.
         *                      - Thread: Thread ID.
         * @param localPath     The local path of the voice file.
         * @param displayName   The display name of f the voice file.
         * @param fileSize      The size of the voice file, in bytes.
         * @param duration      The voice duration in seconds.
         *
         */
        static public Message CreateVoiceSendMessage(string userId, string localPath, string displayName = "", long fileSize = 0, int duration = 0)
        {
            return CreateSendMessage(userId, new MessageBody.VoiceBody(localPath, displayName: displayName, fileSize: fileSize, duration: duration));
        }

        /**
         * \~chinese
         * 创建一条位置发送消息。
         *
         * @param userId        消息接收方。
         *                      - 单聊：对端用户 ID；
         *                      - 群组聊天：群组 ID；
         *                      - 聊天室：聊天室 ID；
         *                      - 子区：子区 ID。
         * @param latitude      纬度。
         * @param longitude     经度。
         * @param address       位置详情。
         * @param buildingName  建筑物名称。
         *  
         * 
         * \~english
         * Creates a location message for sending.
         *
         * @param userId        The message recipient:
         *                      - One-to-one chat: The user ID of the peer user.
         *                      - Group chat: Group ID.
         *                      - Chat room: Chat room ID.
         *                      - Thread: Thread ID.
         * @param latitude      The latitude.
         * @param longitude     The longitude.
         * @param address       The location details.
         * @param buildingName  The building name.
         * 
         */
        static public Message CreateLocationSendMessage(string userId, double latitude, double longitude, string address = "", string buildingName = "")
        {
            return CreateSendMessage(userId, new MessageBody.LocationBody(latitude: latitude, longitude: longitude, address: address, buildName: buildingName));
        }

        /**
         * \~chinese
         * 创建一条命令发送消息。
         *
         * @param userId                消息接收方。
         *                              - 单聊：对端用户 ID；
         *                              - 群组聊天：群组 ID；
         *                              - 聊天室：聊天室 ID；
         *                              - 子区：子区 ID。
         * @param action                命令内容。
         *                              - `true`：只投在线用户。
         *                              - （默认） `false`：不管用户是否在线均投递。
         *
         * 
         * \~english
         * Creates a command message for sending.
         *
         * @param userId                The message recipient:
         *                              - One-to-one chat: The user ID of the peer user.
         *                              - Group chat: Group ID.
         *                              - Chat room: Chat room ID.
         *                              - Thread: Thread ID.
         * @param action                The command action.
         * @param deliverOnlineOnly     Whether this command message is delivered only to the online users.
         *                              - `true`: Yes.
         *                              - (Default) `false`: No. The command message is delivered to users, regardless of their online or offline status.
         * 
         */
        static public Message CreateCmdSendMessage(string userId, string action, bool deliverOnlineOnly = false)
        {
            return CreateSendMessage(userId, new MessageBody.CmdBody(action, deliverOnlineOnly: deliverOnlineOnly));
        }

        /**
         * \~chinese
         * 创建一条自定义发送消息。
         *
         * @param userId            消息接收方。
         *                          - 单聊：对端用户 ID；
         *                          - 群组聊天：群组 ID；
         *                          - 聊天室：聊天室 ID；
         *                          - 子区：子区 ID。
         * @param customEvent       自定义事件。
         * @param customParams      自定义参数字典。
         * 
         * 
         * \~english
         * Creates a custom message for sending.
         *
         * @param userId            The message recipient:
         *                          - One-to-one chat: The user ID of the peer user.
         *                          - Group chat: Group ID.
         *                          - Chat room: Chat room ID.
         *                          - Thread: Thread ID.
         * @param customEvent       The custom event.
         * @param customParams      The dictionary of custom parameters.
         * 
         */
        static public Message CreateCustomSendMessage(string userId, string customEvent, Dictionary<string, string> customParams = null)
        {
            return CreateSendMessage(userId, new MessageBody.CustomBody(customEvent, customParams: customParams));
        }

        /**
        * \~chinese
        * 创建一条合并消息的发送消息。
        *
        * @param userId                 消息接收方。
         *                              - 单聊：对端用户 ID；
         *                              - 群组聊天：群组 ID；
         *                              - 聊天室：聊天室 ID；
         *                              - 子区：子区 ID。
        * @param title                  合并消息的标题。该字段可以设置为 `null` 或者空字符串。
        * @param summary                合并消息的概要。该字段可以设置为 `null` 或者空字符串。
        * @param compatibleText         合并消息的兼容信息。该字段可以设置为 `null` 或者空字符串。该字段用于需要兼容不支持合并转发消息的版本。
        * @param messageList            合并消息的消息 ID 列表。列表不可为`null`或者空，最多可包含 300 个消息 ID。
        *
        * \~english
        * Creates a combined message for sending.
        *
        * @param userId                 The message recipient:
         *                              - One-to-one chat: The user ID of the peer user.
         *                              - Group chat: Group ID.
         *                              - Chat room: Chat room ID.
         *                              - Thread: Thread ID.
        * @param title                  The title of the combined message. It can be `null` or an empty string ("").
        * @param summary                The summary of the combined message. It can be `null` or an empty string ("").
        * @param compatibleText         The compatible text of the combined message. It can be `null` or an empty string ("").
        * @param messageList            The ID list of messages included in the combined message. The list cannot be `null` or empty. It can contain a maximum of 300 message IDs.
        *
        *
        */
        static public Message CreateCombineSendMessage(string userId, string title, string summary, string compatibleText, List<string> messageList)
        {
            return CreateSendMessage(userId, new MessageBody.CombineBody(title: title, summary: summary, compatibleText: compatibleText, messageList: messageList));
        }

        /**
         * \~chinese
         * 获取扩展属性的类型。
         *
         * @param value          扩展属性实例。
         * 
         * \~english
         * Gets the type of the message extension attribute.
         *
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
            if (null == arriMap)
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
            if (null == value)
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
            }
            else
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

        [Preserve]
        internal Message() { }

        [Preserve]
        internal Message(string jsonString) : base(jsonString) { }

        [Preserve]
        internal Message(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jn)
        {
            if (null != jn)
            {
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    From = jo["from"].Value;
                    To = jo["to"].Value;
                    HasReadAck = jo["hasReadAck"].AsBool;
                    HasDeliverAck = jo["hasDeliverAck"].AsBool;
                    LocalTime = (long)jo["localTime"].AsDouble;
                    ServerTime = (long)jo["serverTime"].AsDouble;
                    ConversationId = jo["convId"].Value;
                    MsgId = jo["msgId"].Value;
                    Status = jo["status"].AsInt.ToMessageStatus();
                    DeliverOnlineOnly = jo["deliverOnlineOnly"].AsBool;
                    MessageType = jo["chatType"].AsInt.ToMessageType();
                    Direction = jo["direction"].AsInt.ToMesssageDirection();
                    Attributes = AttributeValue.DictFromJsonObject(jo["attr"].AsObject);
                    // body:{type:iType, "body":{object}}
                    Body = ModelHelper.CreateBodyWithJsonObject(jo["body"]);
                    IsNeedGroupAck = jo["isNeedGroupAck"].AsBool;
                    IsRead = jo["isRead"].AsBool;
                    MessageOnlineState = jo["messageOnlineState"].AsBool;
                    IsThread = jo["isThread"].AsBool;
                    Broadcast = jo["broadcast"].AsBool;
                    IsContentReplaced = jo["isContentReplaced"].AsBool;
                }
            }
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("from", From);
            jo.AddWithoutNull("to", To);
            jo.AddWithoutNull("hasReadAck", HasReadAck);
            jo.AddWithoutNull("hasDeliverAck", HasDeliverAck);
            jo.AddWithoutNull("localTime", LocalTime);
            jo.AddWithoutNull("serverTime", ServerTime);
            jo.AddWithoutNull("convId", ConversationId);
            jo.AddWithoutNull("msgId", MsgId);
            jo.AddWithoutNull("priority", Priority.ToInt());
            jo.AddWithoutNull("deliverOnlineOnly", DeliverOnlineOnly);
            jo.AddWithoutNull("status", Status.ToInt());
            jo.AddWithoutNull("chatType", MessageType.ToInt());
            jo.AddWithoutNull("direction", Direction.ToInt());
            JSONNode jn = JsonObject.JsonObjectFromAttributes(Attributes);
            if (jn != null)
            {
                jo.AddWithoutNull("attr", jn);
            }
            jo.AddWithoutNull("body", Body.ToJsonObject());
            jo.AddWithoutNull("isNeedGroupAck", IsNeedGroupAck);
            jo.AddWithoutNull("isRead", IsRead);
            jo.AddWithoutNull("messageOnlineState", MessageOnlineState);
            jo.AddWithoutNull("isThread", IsThread);
            jo.AddWithoutNull("broadcast", Broadcast);
            jo.AddWithoutNull("isContentReplaced", IsContentReplaced);

            if (null != _receiverList && _receiverList.Count > 0)
            {
                jn = JsonObject.JsonArrayFromStringList(_receiverList);
                jo.AddWithoutNull("receiverList", jn);
            }

            return jo;
        }

    }
}